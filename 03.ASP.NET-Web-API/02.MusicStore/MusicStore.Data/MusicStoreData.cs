namespace MusicStore.Data
{
    using MusicStore.Data.Repositories;
    using MusicStore.Models;
    using System;
    using System.Collections.Generic;

    public class MusicStoreData : IMusicStoreData
    {
        private IMusicStoreDbContext context;
        private IDictionary<Type, object> repositories;

        public MusicStoreData()
            : this(new MusicStoreDbContext())
        {
        }

        public MusicStoreData(IMusicStoreDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Artist> Artists
        {
            get { return this.GetRepository<Artist>(); }
        }

        public IRepository<Album> Albums
        {
            get { return this.GetRepository<Album>(); }
        }

        public IRepository<Song> Songs
        {
            get { return this.GetRepository<Song>(); }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(Repository<T>);

                //if (typeOfModel.IsAssignableFrom(typeof(Student)))
                //{
                //    type = typeof(StudentsRepository);
                //}

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeOfModel];
        }
    }
}
