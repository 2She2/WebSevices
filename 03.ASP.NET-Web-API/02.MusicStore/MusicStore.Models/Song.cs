namespace MusicStore.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Song
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime? Year { get; set; }

        public string Genre { get; set; }

        public int? ArtistId { get; set; }

        public virtual Artist Artist { get; set; }
    }
}
