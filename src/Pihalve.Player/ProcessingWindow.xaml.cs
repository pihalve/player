using System;
using System.ComponentModel;
using System.Windows;

namespace Pihalve.Player
{
    /// <summary>
    /// Interaction logic for ProcessingWindow.xaml
    /// </summary>
    public partial class ProcessingWindow : Window
    {
        private readonly Func<BackgroundWorker, object> _processor;
        private readonly BackgroundWorker _backgroundWorker;

        public object Result { get; private set; }

        public ProcessingWindow(Func<BackgroundWorker, object> processor)
        {
            _processor = processor;

            InitializeComponent();

            _backgroundWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true
            };
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;
            _backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            _backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }

        private void ProcessingWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = _processor((BackgroundWorker)sender);
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Progress.Value = e.ProgressPercentage;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Result = e.Result;
            Close();
        }
    }
}
