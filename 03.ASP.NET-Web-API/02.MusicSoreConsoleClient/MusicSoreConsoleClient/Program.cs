namespace MusicSoreConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;

    class Program
    {
        private static readonly HttpClient client = new HttpClient { BaseAddress = new Uri("http://localhost:62988/") };

        static void Main(string[] args)
        {
            Controls controls = new Controls(client);

            Artist artistToAdd = new Artist
            {
                Name = "Peshkata",
                DateOfBirth = DateTime.Now,
                Country = "Germany"
            };

            Artist artistToUpdate = new Artist
            {
                Name = "UpdatedName",
                Country = "Africa",
                DateOfBirth = DateTime.Now
            };

            // Example services usage!!!

            //controls.All<Song>("songs");
            //controls.ById<Artist>(1, "artists");
            //controls.CreateAsJson<Artist>(artistToAdd, "artists");
            //controls.Update<Artist>(13, artistToUpdate, "artists");
            //controls.Delete<Artist>(11, "artists");
            //controls.CreateAsXml<Artist>(artistToAdd, "artists");
            //controls.AddSong(3, 3, "artists");
            //controls.CreateAsXml<Artist>(artistToAdd, "artists");
        }
    }
}
