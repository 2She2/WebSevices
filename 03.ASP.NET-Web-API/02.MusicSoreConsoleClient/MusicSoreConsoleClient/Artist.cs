namespace MusicSoreConsoleClient
{
    using System;

    public class Artist
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public override string ToString()
        {
            return string.Format("{0,4} {1,-15} {2, -25} {3}",
                    Id, Name, DateOfBirth, Country);
        }
    }
}
