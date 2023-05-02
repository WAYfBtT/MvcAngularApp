using BLL.Models;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService : ICrud<UserModel, int>
    {
        public Task<UserModel> GetByIdWithDetailsAsync(int id);
        public Task<UserModel> GetByUserNameAsync(string userName);
    }
}
