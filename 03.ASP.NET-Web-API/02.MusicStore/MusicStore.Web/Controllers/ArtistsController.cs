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

    public class ArtistsController : ApiController
    {
        private IMusicStoreData data;

        public ArtistsController()
            : this(new MusicStoreData())
        {
        }

        public ArtistsController(IMusicStoreData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var artists = this.data
                .Artists
                .All()
                .Select(ArtistModel.FromArtist);

            return Ok(artists);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var artist = this.data
                .Artists
                .All()
                .Where(art => art.Id == id)
                .Select(ArtistModel.FromArtist)
                .FirstOrDefault();

            if (artist == null)
            {
                return BadRequest("Artist with id: " + id + " do not exists!");
            }

            return Ok(artist);
        }

        [HttpPost]
        public IHttpActionResult Create(ArtistModel artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newArtist = new Artist
            {
                Name = artist.Name,
                Country = artist.Country,
                DateOfBirth = artist.DateOfBirth
            };

            this.data.Artists.Add(newArtist);
            this.data.SaveChanges();

            artist.Id = newArtist.Id;
            return Ok(artist);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, ArtistModel artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingArtist = this.data
                .Artists
                .All()
                .FirstOrDefault(art => art.Id == id);

            if (existingArtist == null)
            {
                return BadRequest("Artist with id: " + id + " do not exists!");
            }

            existingArtist.Name = artist.Name;
            existingArtist.Country = artist.Country;
            existingArtist.DateOfBirth = artist.DateOfBirth;

            this.data.SaveChanges();

            artist.Id = existingArtist.Id;

            return Ok(artist);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingArtist = this.data
                .Artists
                .All()
                .FirstOrDefault(art => art.Id == id);

            if (existingArtist == null)
            {
                return BadRequest("Artist with id: " + id + " do not exists!");
            }

            this.data.Artists.Delete(existingArtist);
            this.data.SaveChanges();

            return Ok(existingArtist);
        }

        [HttpPost]
        public IHttpActionResult AddSong(int id, int songId)
        {
            var existingArtist = this.data
                .Artists
                .All()
                .FirstOrDefault(art => art.Id == id);

            if (existingArtist == null)
            {
                return BadRequest("Artist with id: " + id + " do not exists!");
            }

            var existingSong = this.data
                .Songs
                .All()
                .FirstOrDefault(s => s.Id == songId);

            if (existingSong == null)
            {
                return BadRequest("Song with id: " + songId + " do not exists!");
            }

            existingArtist.Songs.Add(existingSong);
            this.data.SaveChanges();

            return Ok();
        }
    }
}
