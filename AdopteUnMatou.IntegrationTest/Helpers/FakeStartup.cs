using AdopteUnMatou.API.Services;
using AdopteUnMatou.API.Services.Interfaces;
using AdopteUnMatou.API.Settings;
using AdopteUnMatou.API.Settings.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebMotions.Fake.Authentication.JwtBearer;

namespace AdopteUnMatou.IntegrationTest.Helpers
{
    public class FakeStartup
    {
        public FakeStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MongoSettings>(Configuration.GetSection(nameof(MongoSettings)));
            services.Configure<AdopteUnMatouSettings>(Configuration.GetSection(nameof(AdopteUnMatouSettings)));

            services.AddSingleton<IMongoSettings>(Span => Span.GetRequiredService<IOptions<MongoSettings>>().Value);
            services.AddSingleton<IAdopteUnMatouSettings>(Span => Span.GetRequiredService<IOptions<AdopteUnMatouSettings>>().Value);

            // JWT Authentication
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddAuthentication(FakeJwtBearerDefaults.AuthenticationScheme).AddFakeJwtBearer();
            services.AddControllers();

            services.AddMvc().AddApplicationPart(Assembly.Load(new AssemblyName("AdopteUnMatou.API")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "AdopteUnMatou.API",
                    Description = "l'API de Adopte un Matou",
                    Contact = new OpenApiContact
                    {
                        Name = "Victor (Feldrise) DENIS",
                        Email = "contact@feldrise.com",
                        Url = new Uri("https://feldrise.com")
                    }
                });

                var filepath = Path.Combine(System.AppContext.BaseDirectory, "AdopteUnMatou.API.xml");
                c.IncludeXmlComments(filepath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // First we need to clear and get a new database
            var path = Directory.GetCurrentDirectory() + "\\..\\..\\..\\database_filling_script_test.js";
            Process.Start("mongo", "localhost:27017/AdopteUnMatouTestDb " + path);

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdopteUnMatou.API v1");
                c.InjectJavascript("/swagger/themes/theme-material.css");
                c.InjectJavascript("/swagger/custom-script.js", "text/javascript");
                c.RoutePrefix = "documentation";
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
