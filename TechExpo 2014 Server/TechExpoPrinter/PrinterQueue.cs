#region

using System;
using System.Linq;
using System.Printing;

#endregion

namespace TechExpoPrinter
{
    public class PrinterQueue
    {
        private int _numberOfPrintJobs;
        private string _printerStatus;
        private PrintQueueStatus _queueStatus;

        public PrinterQueue(PrintQueue printQueue)
        {
            PrintQueue = printQueue;
        }

        public PrintQueue PrintQueue
        {
            get;

            private set;
        }

        public int NumberOfPrintJobs
        {
            get
            {
                return _numberOfPrintJobs;
            }
            set
            {
                if (value == _numberOfPrintJobs)
                {
                    return;
                }
                _numberOfPrintJobs = value;
                OnPrinterStatusUpdate(new EventArgs());
            }
        }

        public string PrinterStatus
        {
            get
            {
                return _printerStatus;
            }
            set
            {
                if (value == _printerStatus)
                {
                    return;
                }
                _printerStatus = value;
                OnPrinterStatusUpdate(new EventArgs());
            }
        }

        private PrintQueueStatus QueueStatus
        {
            set
            {
                var printerStatus = "Available";

                switch (_queueStatus)
                {
                    case PrintQueueStatus.PaperProblem:
                        printerStatus = "Printer has a paper problem. ";
                        break;

                    case PrintQueueStatus.NoToner:
                        printerStatus = "Printer is out of toner. ";
                        break;

                    case PrintQueueStatus.DoorOpen:
                        printerStatus = "Printer has an open door. ";
                        break;

                    case PrintQueueStatus.Error:
                        printerStatus = "Printer is in an error state. ";
                        break;

                    case PrintQueueStatus.NotAvailable:
                        printerStatus = "Printer is not available. ";
                        break;

                    case PrintQueueStatus.Offline:
                        printerStatus = "Printer is offline. ";
                        break;

                    case PrintQueueStatus.OutOfMemory:
                        printerStatus = "Printer is out of memory. ";
                        break;

                    case PrintQueueStatus.PaperOut:
                        printerStatus = "Printer is out of paper. ";
                        break;

                    case PrintQueueStatus.OutputBinFull:
                        printerStatus = "Printer has a full output bin. ";
                        break;

                    case PrintQueueStatus.PaperJam:
                        printerStatus = "Printer has a paper jam. ";
                        break;

                    case PrintQueueStatus.Paused:
                        printerStatus = "Printer is paused. ";
                        break;

                    case PrintQueueStatus.TonerLow:
                        printerStatus = "Printer is low on toner. ";
                        break;

                    case PrintQueueStatus.UserIntervention:
                        printerStatus = "Printer needs user intervention. ";
                        break;
                }
                PrinterStatus = printerStatus;
                //if (value == _queueStatus)
                //{
                //    return;
                //}
                _queueStatus = value;
            }
        }

        public void Refresh()
        {
            var printServer = new PrintServer();
            var printQueuesOnLocalServer = printServer.GetPrintQueues(new[]
            {
                EnumeratedPrintQueueTypes.Local,
                EnumeratedPrintQueueTypes.Connections
            });
            PrintQueue = printQueuesOnLocalServer.FirstOrDefault(p => p.FullName == PrintQueue.FullName);
            //PrintQueue.Refresh();
            NumberOfPrintJobs = PrintQueue.NumberOfJobs;
            QueueStatus = PrintQueue.QueueStatus;
        }

        #region 'PrinterStatusUpdate' event definition code

        /// <summary>
        ///     This represents the delegate method prototype that
        ///     event receivers must implement
        /// </summary>
        public delegate void PrinterStatusUpdateEventHandler(object sender, EventArgs args);

        // Private delegate linked list (explicitly defined)
        private PrinterStatusUpdateEventHandler _printerStatusUpdateEventHandlerDelegate;

        public event PrinterStatusUpdateEventHandler PrinterStatusUpdate
        {
            // Explicit event definition with accessor methods
            add
            {
                _printerStatusUpdateEventHandlerDelegate =
                    (PrinterStatusUpdateEventHandler) Delegate.Combine(_printerStatusUpdateEventHandlerDelegate, value);
            }
            remove
            {
                _printerStatusUpdateEventHandlerDelegate =
                    (PrinterStatusUpdateEventHandler) Delegate.Remove(_printerStatusUpdateEventHandlerDelegate, value);
            }
        }

        /// <summary>
        ///     This is the method that is responsible for notifying
        ///     receivers that the event occurred
        /// </summary>
        protected virtual void OnPrinterStatusUpdate(EventArgs e)
        {
            if (_printerStatusUpdateEventHandlerDelegate != null)
            {
                _printerStatusUpdateEventHandlerDelegate(this, e);
            }
        }

        #endregion //('PrinterStatusUpdate' event definition code)
    }
}