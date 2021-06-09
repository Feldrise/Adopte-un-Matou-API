using AdopteUnMatou.API.Services;
using AdopteUnMatou.API.Services.Interfaces;
using AdopteUnMatou.API.Settings;
using AdopteUnMatou.API.Settings.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdopteUnMatou.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MongoSettings>(Configuration.GetSection(nameof(MongoSettings)));
            services.Configure<AdopteUnMatouSettings>(Configuration.GetSection(nameof(AdopteUnMatouSettings)));

            services.AddSingleton<IMongoSettings>(Span => Span.GetRequiredService<IOptions<MongoSettings>>().Value);
            services.AddSingleton<IAdopteUnMatouSettings>(Span => Span.GetRequiredService<IOptions<AdopteUnMatouSettings>>().Value);

            // JWT Authentication
            var adopteUnMatouSettings = Configuration.GetSection(nameof(AdopteUnMatouSettings)).Get<AdopteUnMatouSettings>();
            var key = Encoding.ASCII.GetBytes(adopteUnMatouSettings.ApiSecret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddCors(options =>
            {
                options.AddPolicy("developerPolicy", builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed((host) => true)
                        .AllowCredentials();
                });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
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
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdopteUnMatou.API v1");
                c.InjectJavascript("/swagger/themes/theme-material.css");
                c.InjectJavascript("/swagger/custom-script.js", "text/javascript");
                c.RoutePrefix = "documentation";
            });
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("developerPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
