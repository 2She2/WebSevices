namespace MusicSoreConsoleClient
{
    using System;

    public class Album
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? Year { get; set; }

        public string Producer { get; set; }

        public override string ToString()
        {
            return string.Format("{0,4} {1,-15} {2, -25} {3}",
                    Id, Title, Year, Producer);
        }
    }
}
