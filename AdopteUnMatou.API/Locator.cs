using AdopteUnMatou.API.DataProvider;
using AdopteUnMatou.API.DataProvider.Interfaces;
using AdopteUnMatou.API.Services;
using AdopteUnMatou.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopteUnMatou.API
{
    public static class Locator
    {
        public static void InitLocator(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ICatsService, CatsService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IFilesService, FilesService>();
            services.AddScoped<IApplicationsService, ApplicationsService>();

            services.AddScoped<IDPUser, DPUser>();
            services.AddScoped<IDPCats, DPCats>();
            services.AddScoped<IDPApplication, DPApplication>();
        }
    }
}
