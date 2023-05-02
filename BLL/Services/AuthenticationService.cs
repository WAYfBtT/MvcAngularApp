using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthenticationService(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<ClaimsPrincipal> SignInAsync(SignInModel model)
        {

            var user = await _userService.GetByUserNameAsync(model.Username);

            var claims = new List<Claim>();

            if (user == null || user.Password != model.Password)
                return new ClaimsPrincipal(new ClaimsIdentity(
                claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType));

            claims.AddRange(new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.UserData, user.Username),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "Customer")
            });

            var claimsIdentity = new ClaimsIdentity(
                claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return new ClaimsPrincipal(claimsIdentity);
        }

        public async Task<bool> SignUp(SignUpModel model)
        {
            var user = await _userService.GetByUserNameAsync(model.Username);
            if (user != null)
                return false;
            var userModel = _mapper.Map<UserModel>(model);
            await _userService.AddAsync(userModel);
            return true;
        }
    }
}
