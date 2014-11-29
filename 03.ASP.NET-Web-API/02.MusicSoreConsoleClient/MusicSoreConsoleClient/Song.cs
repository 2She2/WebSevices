namespace MusicSoreConsoleClient
{
    using System;

    public class Song
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? Year { get; set; }

        public string Genre { get; set; }

        public int? ArtistId { get; set; }

        public override string ToString()
        {
            return string.Format("{0,4} {1,-15} {2, -25} {3} {4}",
                    Id, Title, Year, Genre, ArtistId);
        }
    }
}
