namespace MusicStore.Data
{
    using MusicStore.Data.Repositories;
    using MusicStore.Models;

    public interface IMusicStoreData
    {
        IRepository<Artist> Artists { get; }

        IRepository<Album> Albums { get; }

        IRepository<Song> Songs { get; }

        void SaveChanges();
    }
}
