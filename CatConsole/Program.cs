using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq;
using System.Net.NetworkInformation;

namespace CatConsole
{
    public class Cat
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Cat(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    class Program
    {
        static HttpClient client = new HttpClient();

        async static Task Main()
        {
            client.BaseAddress = new Uri("http://localhost:65470/");

            var media = new MediaTypeWithQualityHeaderValue("application/json");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(media);

            try
            {
                var cat = new Cat(6, "Charles");
                
                var message = AddCat(cat);

                Console.WriteLine($"Create: {message}");

                var listOfCats = await ListCats();
              
                foreach (var cats in listOfCats)
                {
                    Console.WriteLine($"Here are your cats: {cats.Name}");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            Console.ReadLine();
        }

        private static string AddCat(Cat cat)
        {
            var action = "/cats";
            var request = client.PostAsJsonAsync(action, cat);

            var response = request.Result.Content.ReadAsStringAsync();

            return response.Result;
        }

        private static async Task<List<Cat>> ListCats()
        {
            var action = "/cats";
            var response = await client.GetAsync(action);

            var cats = await response.Content.ReadAsAsync<List<Cat>>();

            return cats;
        }

    }
}

