using DAL.DataContext;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UrlRepository : BaseRepository<Url, int>, IUrlRepository
    {
        public UrlRepository(UrlShortenerDataContext context) : base(context)
        {
        }

        public async Task<Url?> GetByShortUrlAsync(string shortUrl) =>
            await _dbSet.FirstOrDefaultAsync(x => x.ShortUrl == shortUrl);
        public async Task<IEnumerable<Url>> GetByUserIdAsync(int userId)
            => await _dbSet.Where(x => x.CreatedBy == userId).ToListAsync();
    }
}
