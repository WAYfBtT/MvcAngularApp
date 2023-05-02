using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataContext
{
    public static class DataContextExtension
    {
        public static void SeedDataContext(this UrlShortenerDataContext context, Func<string, string> shortener)
        {
            if (context.Users.Any())
            {
                return;
            }

            var user = new User()
            {
                Username = "admin",
                UsernameNormalized = "ADMIN",
                Password = "admin",
                IsAdmin = true,
                Urls = new List<Url>
                {
                    new Url
                    {
                        LongUrl = "https://github.com/",
                        CreatedAtUtc = DateTime.Now,
                        ShortUrl = shortener("https://github.com/")
                    },
                    new Url
                    {
                        LongUrl = "https://stackoverflow.com/",
                        CreatedAtUtc = DateTime.Now,
                        ShortUrl = shortener("https://stackoverflow.com/")
                    },
                    new Url
                    {
                        LongUrl = "https://techcrunch.com/",
                        CreatedAtUtc = DateTime.Now,
                        ShortUrl = shortener("https://techcrunch.com/")
                    },
                    new Url
                    {
                        LongUrl = "https://news.ycombinator.com/",
                        CreatedAtUtc = DateTime.Now,
                        ShortUrl = shortener("https://news.ycombinator.com/")
                    },
                }
            };
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
