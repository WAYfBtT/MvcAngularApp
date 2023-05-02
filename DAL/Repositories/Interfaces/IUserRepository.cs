using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User?> GetByUserNameAsync(string userName);
        Task<User?> GetByIdWithDetailsAsync(int id);
    }
}
