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
    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        public UserRepository(UrlShortenerDataContext context) : base(context)
        {
        }

        public async Task<User?> GetByIdWithDetailsAsync(int id)
            => await _dbSet.Include(x => x.Urls).FirstOrDefaultAsync(x => x.Id == id);
        public Task<User?> GetByUserNameAsync(string userName)
            => _dbSet.FirstOrDefaultAsync(x => x.UsernameNormalized == userName.ToUpper());
    }
}
