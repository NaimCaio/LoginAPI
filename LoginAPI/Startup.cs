using LoginAPI.Application;
using LoginAPI.EF.Context;
using LoginAPI.Infra.Repositories;
using LoginAPI.Model;
using LoginAPI.Model.Entity;
using LoginAPI.Model.Helper;
using LoginAPI.Model.Interfaces;
using LoginAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginAPI
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
            services.AddControllersWithViews();
            //Swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Primeira API",
                        Version = "v1",
                        Description = "API REST",
                        Contact = new OpenApiContact
                        {
                            Name = "Caio Naim"
                        }

                    }
                    );
            });
            services.AddLogging(configure => configure.AddConsole());
            services.AddControllers();
            // in ConfigureServices()

            // config shown  for reference values
            var Issuer = Configuration.GetSection("Jwt:Issuer").Value;
            var Audience = Configuration.GetSection("Jwt:Audience").Value;
            var SigningKey = Configuration.GetSection("Jwt:SigningKey").Value;  //  some long id
            var TimeOut = double.Parse( Configuration.GetSection("Jwt:TimeOut").Value);

            var jwtConfig = new JwtConfig(Issuer, Audience, SigningKey, TimeOut);


            services.AddSingleton(jwtConfig);

            services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ILoginApplication, LoginApplication>();

            

            services.AddDbContext<BaseContext>();


            // Configure Authentication
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Issuer,
                    ValidateAudience = true,
                    ValidAudience = Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SigningKey))
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // in Startup.Configure()
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Primeira API - v1");
            });
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            // *** These are the important ones - note order matters ***
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStatusCodePages();
            //app.UseDefaultFiles(); // so index.html is not required
            //app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
