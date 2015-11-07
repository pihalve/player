namespace Pihalve.Player.Library
{
    public static class LibraryDirector
    {
        public static void Construct(ILibraryBuilder libraryBuilder)
        {
            libraryBuilder.BuildTrackList();
            libraryBuilder.BuildPlaylists();
        }
    }
}
