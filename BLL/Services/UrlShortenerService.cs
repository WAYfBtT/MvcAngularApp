using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UrlShortenerService : IUrlShortenerService
    {
        public string ShortenUrl(string url)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(url);
            var hash = sha256.ComputeHash(bytes);
            var base64 = Convert.ToBase64String(hash);
            return base64.Substring(0, 12);
        }
    }
}
