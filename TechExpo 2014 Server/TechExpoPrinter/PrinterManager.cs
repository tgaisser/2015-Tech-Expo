#region

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Printing;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using com.bluewatertech.common.logging;
using TechExpo.Data;
using TechExpo.Data.Models;

#endregion

namespace TechExpoPrinter
{
    public class PrinterManager
    {
        #region Member Variables

        private readonly LinkedList<PrinterQueue> _linkedPrintQueue = new LinkedList<PrinterQueue>();
        private readonly Object _lock = new Object();
        private readonly List<PrinterQueue> _printQueues = new List<PrinterQueue>();
        private readonly Queue<Registrant> _registrants = new Queue<Registrant>();
        private readonly Dispatcher _uiDispatcher;
        private LinkedListNode<PrinterQueue> _lastPrinter;
        private bool _monitor;
        private bool _refreshPrinters = true;
        private readonly DataContext _db;
        private double _scale;
        private Size _printSize;
        private Rect _layoutRect;

        #endregion

        public PrinterManager(Dispatcher uiDispatcher, DataContext db)
        {
            _db = db;
            _uiDispatcher = uiDispatcher;
            RefreshPrinterList();
        }

        private void GetPrinters()
        {
            var printFilter = ConfigurationManager.AppSettings["UsePrintersStartingWith"];
            lock (_lock)
            {
                _printQueues.Clear();
                _linkedPrintQueue.Clear();
                var printServer = new PrintServer();
                var printQueuesOnLocalServer = printServer.GetPrintQueues(new[]
                {
                    EnumeratedPrintQueueTypes.Local,
                    EnumeratedPrintQueueTypes.Connections
                });

                foreach (var newPrintQueue in
                    printQueuesOnLocalServer.Where(p => p.Name.StartsWith(printFilter))
                                            .Select(printQueue => new PrinterQueue(printQueue)))
                {
                    _printQueues.Add(newPrintQueue);
                    newPrintQueue.Refresh();
                    newPrintQueue.PrinterStatusUpdate += newPrintQueue_PrinterStatusUpdate;
                }
                _linkedPrintQueue.AddRange(_printQueues);
            }

            OnPrintersStatusChanged(new PrintersStatusChangedEventArgs(GetPrinterStatusUpdates()));
        }

        public void RefreshPrinterList()
        {
            _refreshPrinters = true;
        }

        private void newPrintQueue_PrinterStatusUpdate(object sender, EventArgs args)
        {
            OnPrintersStatusChanged(new PrintersStatusChangedEventArgs(GetPrinterStatusUpdates()));
        }

        public void Start()
        {
            _monitor = true;
            Monitor();
        }

        public void Stop()
        {
            _monitor = false;
        }

        public void QueueRegistrant(Registrant registrant)
        {
            lock (_registrants)
            {
                if (!_registrants.Contains(registrant))
                {
                    _registrants.Enqueue(registrant);
                }
            }
        }

        private void ProbePrinters()
        {
            var foundADamnPrinter = false;
            Logger.Instance.LogInfo("Probing Printers... (That's what she said...)");

            if (!_linkedPrintQueue.Any())
            {
                Logger.Instance.LogError("No Printers Found, Connect Printers And Restart Monitor Program!");
                return;

            }

            var window = new PassWindow(null);

            window.Show();

            while (!foundADamnPrinter)
            {
                var printQueue = GetNextPrinterQueue();

                try
                {
                   

                    var dialog = new PrintDialog();
                    var ticket = new PrintTicket
                    {
                        CopyCount = 1,
                        PageOrientation = PageOrientation.Portrait
                    };
                    dialog.PrintTicket = ticket;
                    dialog.PrintQueue = printQueue.PrintQueue;

                    printQueue.PrintQueue.UserPrintTicket = ticket;

                    var capabilities = printQueue.PrintQueue.GetPrintCapabilities(ticket);


                    //get scale of the print wrt to screen of WPF visual

                    _scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / window._gridPass.ActualWidth,
                                         capabilities.PageImageableArea.ExtentHeight / window._gridPass.ActualHeight);


                    //get the size of the printer page

                    _printSize = new Size(dialog.PrintableAreaWidth, dialog.PrintableAreaHeight);

                    _layoutRect =
                        new Rect(
                            new Point(capabilities.PageImageableArea.OriginWidth,
                                      capabilities.PageImageableArea.OriginHeight),
                            _printSize);

                    Logger.Instance.LogSuccess("Yippie! Found A Printer That Talks English!");

                    foundADamnPrinter = true;
                }
                catch (Exception)
                {
                    Logger.Instance.LogFail("Printer is not reporting capibilities (Stupid Printer)");
                    
                }
               
                window.Close();
            }

        }

        private void Monitor()
        {
            new Thread(new ThreadStart(delegate
            {
                Thread.CurrentThread.IsBackground = true;

                while (_monitor)
                {
                    _uiDispatcher.Invoke(delegate
                    {
                        if (_refreshPrinters || !_printQueues.Any())
                        {
                            GetPrinters();
                            ProbePrinters();
                            _refreshPrinters = false;
                            if (_printQueues.Any())
                            {
                                _lastPrinter = _linkedPrintQueue.First;
                            }
                        }

                        foreach (var printerQueue in _printQueues)
                        {
                            printerQueue.Refresh();
                        }
                        while (_registrants.Any() && _linkedPrintQueue.Any())
                        {
                            var printerQueue = GetNextPrinterQueue();

                            if (printerQueue == null)
                            {
                                break;
                            }
                            var registrant = _registrants.Dequeue();
                            PrintBadge(registrant, printerQueue);
                        }
                    });
                    Thread.Sleep(1000);
                }
            })).Start();
        }

        private PrinterQueue GetNextPrinterQueue()
        {
            if (!_linkedPrintQueue.Any())
            {
                return null;
            }

            if (_lastPrinter != null)
            {
                _lastPrinter = _lastPrinter.Next;
            }

            if (_lastPrinter == null)
            {
                _lastPrinter = _linkedPrintQueue.First;
            }

            return _lastPrinter.Value;
        }


        public void PrintBadge(Registrant registrant, PrinterQueue printerQueue)
        {
            //return;
            Logger.Instance.LogDebug("Printing Badge For: {0} {1}", registrant.FirstName, registrant.LastName);

            Logger.Instance.LogDebug("Printing On: {0}", printerQueue.PrintQueue.Name);

            var window = new PassWindow(registrant);

            window.Show();

            try
            {

            var dialog = new PrintDialog();
            var ticket = new PrintTicket
            {
                CopyCount = 1,
                PageOrientation = PageOrientation.Portrait
            };
            dialog.PrintTicket = ticket;
            dialog.PrintQueue = printerQueue.PrintQueue;

            printerQueue.PrintQueue.UserPrintTicket = ticket;

           
            
            //Transform the Visual to scale
            window._gridPass.LayoutTransform = new ScaleTransform(_scale, _scale);

            //update the layout of the visual to the printer page size.

            window._gridPass.Measure(_printSize);

            window._gridPass.Arrange(_layoutRect);

            dialog.PrintVisual(window._gridPass, string.Format("{0} {1}", registrant.FirstName, registrant.LastName));

            

            registrant.PrintPassStatus = "PRINTED";

            }
            catch (Exception ex)
            {
                registrant.PrintPassStatus = "NEEDS PRINTING";
                
                Logger.Instance.LogFail("Failed To Print Pass: {0}", ex.Message);
            }

            _db.SaveChanges();

            window.Close();

        }

        public List<PrinterStatusUpdate> GetPrinterStatusUpdates()
        {
            var results = new List<PrinterStatusUpdate>();
            lock (_lock)
            {
                results.AddRange(_printQueues.Select(printerQueue => new PrinterStatusUpdate
                {
                    PrinterName = printerQueue.PrintQueue.Name,
                    PrinterJobCount = printerQueue.NumberOfPrintJobs,
                    PrinterStatus = printerQueue.PrinterStatus
                }));
            }
            return results;
        }

        #region 'PrintersStatusChanged' event definition code

        // Private delegate linked list (explicitly defined)
        private EventHandler<PrintersStatusChangedEventArgs> PrintersStatusChangedEventHandlerDelegate;

        public event EventHandler<PrintersStatusChangedEventArgs> PrintersStatusChanged
        {
            // Explicit event definition with accessor methods
            add
            {
                PrintersStatusChangedEventHandlerDelegate =
                    (EventHandler<PrintersStatusChangedEventArgs>)
                    Delegate.Combine(PrintersStatusChangedEventHandlerDelegate, value);
            }
            remove
            {
                PrintersStatusChangedEventHandlerDelegate =
                    (EventHandler<PrintersStatusChangedEventArgs>)
                    Delegate.Remove(PrintersStatusChangedEventHandlerDelegate, value);
            }
        }

        /// <summary>
        ///     This is the method that is responsible for notifying
        ///     receivers that the event occurred
        /// </summary>
        protected virtual void OnPrintersStatusChanged(PrintersStatusChangedEventArgs e)
        {
            if (PrintersStatusChangedEventHandlerDelegate != null)
            {
                PrintersStatusChangedEventHandlerDelegate(this, e);
            }
        }

        /// <summary>
        ///     EventArgs derived type which holds the custom event fields
        /// </summary>
        public class PrintersStatusChangedEventArgs : EventArgs
        {
            public readonly List<PrinterStatusUpdate> Statuses;

            /// <summary>
            ///     Use this constructor to initialize the event arguments
            ///     object with the custom event fields
            /// </summary>
            public PrintersStatusChangedEventArgs(List<PrinterStatusUpdate> statuses)
            {
                Statuses = statuses;
            }
        }

        #endregion //('PrintersStatusChanged' event definition code)
    }
}