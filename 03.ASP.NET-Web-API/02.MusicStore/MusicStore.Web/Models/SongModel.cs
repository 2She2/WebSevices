namespace MusicStore.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using MusicStore.Models;

    public class SongModel
    {
        public static Expression<Func<Song, SongModel>> FromSong
        {
            get
            {
                return s => new SongModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    Genre = s.Genre,
                    ArtistId = s.ArtistId
                };
            }
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime? Year { get; set; }

        public string Genre { get; set; }

        public int? ArtistId { get; set; }
    }
}