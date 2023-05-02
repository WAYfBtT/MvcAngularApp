using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IUrlRepository : IRepository<Url, int>
    {
        public Task<Url?> GetByShortUrlAsync(string shortUrl);

        public Task<IEnumerable<Url>> GetByUserIdAsync(int userId);
    }
}
