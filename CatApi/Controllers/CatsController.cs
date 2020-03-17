using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CatApi.Controllers
{
    public class Cat
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class CatsController : ControllerBase
    {

        public static List<Cat> cats = new List<Cat>();

        static CatsController()
        {
            cats.Add(new Cat { Id = 1, Name = "Amiee" });
            cats.Add(new Cat { Id = 2, Name = "Ryan" });
            cats.Add(new Cat { Id = 3, Name = "Justin" });
            cats.Add(new Cat { Id = 4, Name = "David" });
            cats.Add(new Cat { Id = 5, Name = "Glenn" });
        }

        [HttpGet("{id}")]
        public Cat GetCat(int id)
        {
                return cats.FirstOrDefault(x => x.Id == id);
        }

        [HttpPost]
        public string AddCat(Cat cat)
        {
            cats.Add(cat);
            return $"Cats added! Now we have {cats.Count} cats!";
        }

        [HttpGet]
        public List<Cat> GetCats()
        {
            return cats;
        }

    }
}

