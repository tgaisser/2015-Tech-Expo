#region

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using com.bluewatertech.common.logging;
using TechExpo.Data;

#endregion

namespace TechExpoPrinter
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BackgroundWorker _dataMonitor = new BackgroundWorker();
        private readonly DataContext _db = new DataContext("TechExpo2014");
        private readonly PrinterManager _printerManager;
        private readonly List<PrinterStatusUpdate> _printerStatusUpdates = new List<PrinterStatusUpdate>();

        private void ResetLeadsThatNeedPrinting()
        {
            var stuckPrints = _db.Registrants.Where(r => r.PrintPassStatus.Equals("QUEUED"))
                                 .ToList();

            //Reset Missed Prints On Startup
            foreach (var registrant in stuckPrints)
            {
                registrant.PrintPassStatus = "NEEDS PRINTING";
            }

            _db.SaveChanges();
        }

        private void StartPrintManager()
        {
            _printerManager.PrintersStatusChanged += _printerManager_PrintersStatusChanged;
            _printerManager.Start();
            Logger.Instance.LogTrace("Printer Monitor Started");
        }

        public MainWindow()
        {
            InitializeComponent();

            _printerManager = new PrinterManager(Dispatcher, _db);
            Logger.Instance.LogMessage += Instance_LogMessage;
            Logger.Instance.LogTrace("Logging Started");

            ResetLeadsThatNeedPrinting();

           

            _dataMonitor.WorkerSupportsCancellation = true;
            _dataMonitor.DoWork += _dataMonitor_DoWork;
            
        }

        private void _dataMonitor_DoWork(object sender, DoWorkEventArgs e)
        {
            Logger.Instance.LogInfo("Starting Data Monitor");

            while (!e.Cancel)
            {
                var passes = _db.Registrants.Where(r => r.PrintPassStatus.Equals("NEEDS PRINTING"))
                                .OrderBy(r => r.OnsiteCheckInDateTime)
                                .ToList();

                foreach (var pass in passes)
                {
                    pass.PrintPassStatus = "QUEUED";
                    _db.SaveChanges();
                    _printerManager.QueueRegistrant(pass);
                }
                

                Thread.Sleep(3000);
            }
        
            Logger.Instance.LogWarning("Stopping Data Monitor");
        }

        private void Instance_LogMessage(object sender, Logger.LogMessageEventArgs e)
        {
            Dispatcher.Invoke(() => _lvLogView.AddLogMessage(e));
        }

        private void _printerManager_PrintersStatusChanged(object sender,
                                                           PrinterManager.PrintersStatusChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                _printerStatusUpdates.Clear();
                _printerStatusUpdates.AddRange(e.Statuses);
                _listBoxPrinters.ItemsSource = _printerStatusUpdates;
            }));
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            StartPrintManager();
            _dataMonitor.RunWorkerAsync();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            _dataMonitor.CancelAsync();
            _printerManager.Stop();

        }
    }
}