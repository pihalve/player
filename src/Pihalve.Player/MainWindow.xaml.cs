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
using Pihalve.Player.Library;
using Pihalve.Player.Library.Model;
using Pihalve.Player.Properties;

namespace Pihalve.Player
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string LibraryFileName = "pihalve-player-library.xml";

        private readonly ITrackFactory _trackFactory;
        private readonly ILibrarySerializer _librarySerializer;
        private readonly IMediaPlayer _mediaPlayer;
        private string _filesPath;

        public MainWindow(ITrackFactory trackFactory, ILibrarySerializer librarySerializer)
        {
            _trackFactory = trackFactory;
            _librarySerializer = librarySerializer;

            InitializeComponent();

            // Defaults
            Left = Settings.Default.MainWindowLeft;
            Top = Settings.Default.MainWindowTop;
            Width = Settings.Default.MainWindowWidth;
            Height = Settings.Default.MainWindowHeight;
            MainGrid.ColumnDefinitions.First(c => c.Name == "NavigationColumn").Width = new GridLength(20, GridUnitType.Star);
            MainGrid.ColumnDefinitions.First(c => c.Name == "ContentColumn").Width = new GridLength(80, GridUnitType.Star);

            Closing += MainWindow_Closing;

            _filesPath = @"M:\VA";
            //var tracks = new DirectoryInfo(_filesPath).GetFiles("*.mp3").OrderBy(x => x.Name);
            //ContentList.DisplayMemberPath = "Name";
            ////ContentList.IsSynchronizedWithCurrentItem = true;
            //ContentList.ItemsSource = tracks;
            //ContentList.Items.MoveCurrentTo(null);

            _mediaPlayer = new WindowsMediaPlayer();
            _mediaPlayer.Volume = .5d;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Settings.Default.MainWindowLeft = Left;
            Settings.Default.MainWindowTop = Top;
            Settings.Default.MainWindowWidth = Width;
            Settings.Default.MainWindowHeight = Height;
        }

        private void LibraryNew_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.SelectedPath = _filesPath;
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var libraryBuilder = new NewLibraryBuilder(new Uri(dialog.SelectedPath), _trackFactory);
                LibraryDirector.Construct(libraryBuilder);

                //ContentList.DisplayMemberPath = "Title";
                ContentList.ItemsSource = libraryBuilder.Library.Tracks;
                ContentList.Items.MoveCurrentTo(null);

                //_librarySerializer.Serialize(libraryBuilder.Library, LibraryFileName);
            }
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
            Play((Track)ContentList.SelectedItem);
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
                Play((Track)ContentList.SelectedItem);
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
                Play((Track)ContentList.SelectedItem);
            }
        }

        private void Play(Track source)
        {
            var fileToPlay = new Uri(source.Location);
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
