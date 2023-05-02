
using DAL.Entities.Abstract;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string UsernameNormalized { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<Url> Urls { get; set; }
    }
}
