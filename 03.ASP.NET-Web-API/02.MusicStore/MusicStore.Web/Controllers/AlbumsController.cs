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

    public class AlbumsController : ApiController
    {
        private IMusicStoreData data;

        public AlbumsController()
            : this(new MusicStoreData())
        {
        }

        public AlbumsController(IMusicStoreData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var albums = this.data
                .Albums
                .All()
                .Select(AlbumModel.FromAlbum);

            return Ok(albums);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var albums = this.data
                .Albums
                .All()
                .Where(alb => alb.Id == id)
                .Select(AlbumModel.FromAlbum)
                .FirstOrDefault();

            if (albums == null)
            {
                return BadRequest("Album with id: " + id + " do not exists!");
            }

            return Ok(albums);
        }

        [HttpPost]
        public IHttpActionResult Create(AlbumModel album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newAlbum = new Album
            {
                Title = album.Title,
                Year = album.Year,
                Producer = album.Producer

            };

            this.data.Albums.Add(newAlbum);
            this.data.SaveChanges();

            album.Id = newAlbum.Id;
            return Ok(album);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, AlbumModel album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingAlbum = this.data
                .Albums
                .All()
                .FirstOrDefault(alb => alb.Id == id);

            if (existingAlbum == null)
            {
                return BadRequest("Album with id: " + id + " do not exists!");
            }

            existingAlbum.Title = album.Title;
            existingAlbum.Year = album.Year;
            existingAlbum.Producer = album.Producer;

            this.data.SaveChanges();

            album.Id = existingAlbum.Id;

            return Ok(album);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingAlbum = this.data
                .Albums
                .All()
                .FirstOrDefault(alb => alb.Id == id);

            if (existingAlbum == null)
            {
                return BadRequest("Album with id: " + id + " do not exists!");
            }

            this.data.Albums.Delete(existingAlbum);
            this.data.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AddSong(int id, int songId)
        {
            var existingAlbum = this.data
                .Albums
                .All()
                .FirstOrDefault(alb => alb.Id == id);

            if (existingAlbum == null)
            {
                return BadRequest("Album with id: " + id + " do not exists!");
            }

            var existingSong = this.data
                .Songs
                .All()
                .FirstOrDefault(s => s.Id == songId);

            if (existingSong == null)
            {
                return BadRequest("Song with id: " + songId + " do not exists!");
            }

            existingAlbum.Songs.Add(existingSong);
            this.data.SaveChanges();

            return Ok();
        }

        public IHttpActionResult AddArtist(int id, int artistId)
        {
            var existingAlbum = this.data
                .Albums
                .All()
                .FirstOrDefault(alb => alb.Id == id);

            if (existingAlbum == null)
            {
                return BadRequest("Album with id: " + id + " do not exists!");
            }

            var existingArtist = this.data
                .Artists
                .All()
                .FirstOrDefault(art => art.Id == artistId);

            if (existingArtist == null)
            {
                return BadRequest("Artist with id: " + artistId + " do not exists!");
            }

            existingAlbum.Artists.Add(existingArtist);
            this.data.SaveChanges();

            return Ok();
        }
    }
}
