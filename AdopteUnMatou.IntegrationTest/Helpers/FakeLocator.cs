using AdopteUnMatou.API.DataProvider;
using AdopteUnMatou.API.DataProvider.Interfaces;
using AdopteUnMatou.API.Services;
using AdopteUnMatou.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdopteUnMatou.IntegrationTest.Helpers
{
    public static class FakeLocator
    {
        public static void InitLocator(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IDPUser, DPUser>();
        }
    }
}
