using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pihalve.Player
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IMediaPlayer _mediaPlayer;
        private string _filesPath;

        public MainWindow()
        {
            InitializeComponent();

            // Defaults
            MainGrid.ColumnDefinitions.First(c => c.Name == "NavigationColumn").Width = new GridLength(30, GridUnitType.Star);
            MainGrid.ColumnDefinitions.First(c => c.Name == "ContentColumn").Width = new GridLength(70, GridUnitType.Star);

            _filesPath = @"M:\2000's";
            var files = new DirectoryInfo(_filesPath).GetFiles("*.mp3").OrderBy(x => x.Name);
            ContentList.DisplayMemberPath = "Name";
            //ContentList.IsSynchronizedWithCurrentItem = true;
            ContentList.ItemsSource = files;
            ContentList.Items.MoveCurrentTo(null);

            _mediaPlayer = new WindowsMediaPlayer();
            _mediaPlayer.Volume = .5d;
        }

        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PlayCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void PlayCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (ContentList.SelectedItem == null)
            {
                return;
            }

            ContentList.Items.MoveCurrentTo(ContentList.SelectedItem);
            Play((FileInfo)ContentList.SelectedItem);
        }

        private void PauseCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void PauseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _mediaPlayer.Pause();
        }

        private void StopCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void StopCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Stop();
            ContentList.Items.MoveCurrentTo(null);
        }

        private void PreviousCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void PreviousCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (ContentList.SelectedItem == null)
            {
                return;
            }

            if (ContentList.Items.MoveCurrentToPrevious())
            {
                Stop();
                ContentList.SelectedItem = ContentList.Items.CurrentItem;
                Play((FileInfo)ContentList.SelectedItem);
            }
        }

        private void NextCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NextCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (ContentList.SelectedItem == null)
            {
                return;
            }

            if (ContentList.Items.MoveCurrentToNext())
            {
                Stop();
                ContentList.SelectedItem = ContentList.Items.CurrentItem;
                Play((FileInfo)ContentList.SelectedItem);
            }
        }

        private void Play(FileInfo source)
        {
            var fileToPlay = new Uri(source.FullName);
            if (_mediaPlayer.Source == null || !_mediaPlayer.Source.Equals(fileToPlay))
            {
                _mediaPlayer.Open(fileToPlay);
                //Playing.Text = source.Name;
            }
            _mediaPlayer.Play();
        }

        private void Stop()
        {
            _mediaPlayer.Stop();
            _mediaPlayer.Close();
        }
    }
}
