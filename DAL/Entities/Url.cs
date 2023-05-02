using DAL.Entities.Abstract;
using System;

namespace DAL.Entities
{
    public class Url : BaseEntity, ICreatedAtUtcEntity
    {
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int CreatedBy { get; set; }
        public User User { get; set; }
    }
}
