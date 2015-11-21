using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Pihalve.Player.Bootstrapping;
using Pihalve.Player.Library;
using Pihalve.Player.Library.Model;
using Pihalve.Player.Persistence;
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
        private readonly IAppDataPersister<Library.Model.Library> _libraryPersister;
        private readonly IMediaPlayer _mediaPlayer;
        private string _filesPath;
        private Library.Model.Library _library;

        public MainWindow(ITrackFactory trackFactory, IAppDataPersister<Library.Model.Library> libraryPersister)
        {
            _trackFactory = trackFactory;
            _libraryPersister = libraryPersister;

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
            _library = _libraryPersister.Load(LibraryFileName) ?? new Library.Model.Library();
            BindLibrary();

            _mediaPlayer = new WindowsMediaPlayer();
            _mediaPlayer.Volume = .5d;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            Settings.Default.MainWindowLeft = Left;
            Settings.Default.MainWindowTop = Top;
            Settings.Default.MainWindowWidth = Width;
            Settings.Default.MainWindowHeight = Height;

            SaveLibrary();
        }

        private void SmartPlaylistNew_OnClick(object sender, RoutedEventArgs e)
        {
            var smartPlaylistEditor = BootLoader.Resolve<SmartPlaylistEditorWindow>();
            smartPlaylistEditor.Left = Left + Width / 2 - smartPlaylistEditor.Width / 2;
            smartPlaylistEditor.Top = Top + Height / 2 - smartPlaylistEditor.Height / 2;

            bool? result = smartPlaylistEditor.ShowDialog();
            if (result.HasValue && result.Value)
            {
                _library.SmartPlaylists.Add(smartPlaylistEditor.SmartPlaylist);
                _library.IsDirty = true;
            }

        }

        private void LibraryNew_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.SelectedPath = _filesPath;

            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var processor = new Func<BackgroundWorker, Library.Model.Library>(bw => CreateNewLibrary(bw, dialog.SelectedPath));
                var processingWindow = BootLoader.Resolve<ProcessingWindow>("processor", processor);
                processingWindow.Left = Left + Width / 2 - processingWindow.Width / 2;
                processingWindow.Top = Top + Height / 2 - processingWindow.Height / 2;

                processingWindow.ShowDialog();
                _library = (Library.Model.Library)processingWindow.Result;

                SaveLibrary();

                BindLibrary();
            }
        }

        private Library.Model.Library CreateNewLibrary(BackgroundWorker bw, string libraryFolderPath)
        {
            var libraryBuilder = new NewLibraryBuilder(new Uri(libraryFolderPath), _trackFactory, bw);
            LibraryDirector.Construct(libraryBuilder);
            return libraryBuilder.Library;
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

        private void BindLibrary()
        {
            ContentList.ItemsSource = _library.Tracks;
            ContentList.Items.MoveCurrentTo(null);
            ////ContentList.IsSynchronizedWithCurrentItem = true;
        }

        private void SaveLibrary()
        {
            if (_library.IsDirty)
            {
                _libraryPersister.Save(_library, LibraryFileName);
            }
        }
    }
}
