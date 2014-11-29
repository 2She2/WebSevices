namespace MusicSoreConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;

    public class Controls
    {
        private readonly HttpClient client;

        public Controls(HttpClient client)
        {
            this.client = client;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
        }
        public void All<T>(string controller)
        {
            HttpResponseMessage response = client.GetAsync("api/" + controller + "/all").Result;
            if (response.IsSuccessStatusCode)
            {
                var responseItems = response.Content.ReadAsAsync<IEnumerable<T>>().Result;

                PrintAll(responseItems);
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public void ById<T>(int id, string controller)
        {
            string headerString = "api/" + controller + "/byid/" + id;

            HttpResponseMessage response = client.GetAsync(headerString).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseItem = response.Content.ReadAsAsync<T>().Result;
                Console.WriteLine(responseItem.ToString());
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public void CreateAsJson<T>(T item, string controller)
        {
            string headerString = "api/" + controller + "/create";

            HttpResponseMessage response = client.PostAsJsonAsync<T>(headerString, item).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseItem = response.Content.ReadAsAsync<T>().Result;
                Console.WriteLine(responseItem.ToString());
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public void CreateAsXml<T>(T item, string controller)
        {
            string headerString = "api/" + controller + "/create";
            HttpResponseMessage response = client.PostAsXmlAsync<T>(headerString, item).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseItem = response.Content.ReadAsAsync<T>().Result;
                Console.WriteLine(responseItem.ToString());
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public void Update<T>(int id, T item, string controller)
        {
            string headerString = "api/" + controller + "/update/" + id;
            HttpResponseMessage response = client.PutAsJsonAsync<T>(headerString, item).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseItem = response.Content.ReadAsAsync<Artist>().Result;
                Console.WriteLine(responseItem.ToString());
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public void Delete<T>(int id, string controller)
        {
            string headerString = "api/" + controller + "/delete/" + id;
            HttpResponseMessage response = client.DeleteAsync(headerString).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseItem = response.Content.ReadAsAsync<T>().Result;
                if (responseItem != null)
                {
                    Console.Write("\nDeleted:");
                    Console.WriteLine(responseItem.ToString());
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public void AddSong(int itemId, int songId, string controller)
        {
            string headerString = string.Format("api/" + controller + "/AddSong/{0}?songId={1}", itemId, songId);
            HttpResponseMessage response = client.PostAsync(headerString, null).Result;

            if (response.IsSuccessStatusCode)
            {
                //var responseArtist = response.Content.ReadAsAsync<Song>().Result;
                Console.WriteLine("Song successfully added!");
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public void AddArtist(int itemId, int artistId, string controller)
        {
            string headerString = string.Format("api/" + controller + "/AddArtist/{0}?artistId={1}", itemId, artistId);
            HttpResponseMessage response = client.PostAsync(headerString, null).Result;

            if (response.IsSuccessStatusCode)
            {
                //var responseArtist = response.Content.ReadAsAsync<Artist>().Result;
                Console.WriteLine("Artist successfully added!");
            }
            else
            {
                Console.WriteLine("{0} ({1})",
                    (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        private static void PrintAll<T>(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
