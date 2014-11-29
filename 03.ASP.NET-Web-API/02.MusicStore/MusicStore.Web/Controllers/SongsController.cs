namespace MusicStore.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using MusicStore.Data;
    using MusicStore.Web.Models;
    using MusicStore.Models;

    public class SongsController : ApiController
    {
        private IMusicStoreData data;

        public SongsController()
            : this(new MusicStoreData())
        {
        }

        public SongsController(IMusicStoreData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var songs = this.data
                .Songs
                .All()
                .Select(SongModel.FromSong);

            return Ok(songs);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var song = this.data
                .Songs
                .All()
                .Where(s => s.Id == id)
                .Select(SongModel.FromSong)
                .FirstOrDefault();

            if (song == null)
            {
                return BadRequest("Song with id: " + id + " do not exists!");
            }

            return Ok(song);
        }

        [HttpPost]
        public IHttpActionResult Create(SongModel song)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newSong = new Song
            {
                Title = song.Title,
                Genre = song.Genre,
                ArtistId = song.ArtistId
            };

            this.data.Songs.Add(newSong);
            this.data.SaveChanges();

            song.Id = newSong.Id;
            return Ok(song);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, SongModel song)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingSong = this.data
                .Songs
                .All()
                .FirstOrDefault(s => s.Id == id);

            if (existingSong == null)
            {
                return BadRequest("Song with id: " + id + " do not exists!");
            }

            existingSong.Title = song.Title;
            existingSong.Genre = song.Genre;
            existingSong.ArtistId = song.ArtistId;

            this.data.SaveChanges();

            song.Id = existingSong.Id;

            return Ok(song);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingSong = this.data
                .Songs
                .All()
                .FirstOrDefault(s => s.Id == id);

            if (existingSong == null)
            {
                return BadRequest("Song with id: " + id + " do not exists!");
            }

            this.data.Songs.Delete(existingSong);
            this.data.SaveChanges();

            return Ok();
        }
    }
}
