using BLL.Interfaces;
using BLL.Mapper;
using BLL.Services;
using DAL.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Configuration
{
    public static class Startup
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUrlService, UrlService>();
            services.AddSingleton<IUrlShortenerService, UrlShortenerService>();
        }
    }
}
