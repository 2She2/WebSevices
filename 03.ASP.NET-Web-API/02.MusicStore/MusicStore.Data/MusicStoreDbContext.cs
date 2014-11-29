namespace MusicStore.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using MusicStore.Models;
    using MusicStore.Data.Migrations;

    public class MusicStoreDbContext : DbContext, IMusicStoreDbContext
    {
        public MusicStoreDbContext()
            : base("MusicStoreConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MusicStoreDbContext, Configuration>());
        }

        public IDbSet<Artist> Artists { get; set; }

        public IDbSet<Album> Albums { get; set; }

        public IDbSet<Song> Songs { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
