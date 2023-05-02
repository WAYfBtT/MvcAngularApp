using BLL.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<ClaimsPrincipal> SignInAsync(SignInModel model);
        public Task<bool> SignUp(SignUpModel model);
    }
}
