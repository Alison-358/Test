using Infrastructure.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data.Repositories;
using Domain.Services;
using Service.Interfaces;
using Service.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Principal;
using Service.Interfaces.Generics;
using Service.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Service.Utils.Helpers.LoginConfiguration;
using Microsoft.Extensions.Options;

namespace API
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
            //var connection = Configuration.GetConnectionString("ApiConnection");
            //services.AddDbContext<DatabaseContext>
            //(
            //    opt => opt.UseSqlServer(connection, op => op.EnableRetryOnFailure())
            //);

            //Athentication Login
            var signing = new Signing();
            services.AddSingleton(signing);

            var tokenConfigurations = new Token();

            new ConfigureFromConfigurationOptions<Token>(
                Configuration.GetSection("TokenConfigurations") //TokenConfigurations AppSettingsJson
            ).Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);//Configuring the three attributes in class getting the json settings

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signing.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Validates the signing of a received token
                paramsValidation.ValidateIssuerSigningKey = true;

                // Checks if a received token is still valid
                paramsValidation.ValidateLifetime = true;

                // Tolerance time for the expiration of a token (used in case
                // of time synchronization problems between different
                // computers involved in the communication process)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("user", policy => policy.RequireClaim("Store", "user"));
                auth.AddPolicy("admin", policy => policy.RequireClaim("Store", "admin"));
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser()
                    .Build());
                //filtros de autenticação
            });

            //Injeção de dependências
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            services.AddScoped(typeof(ISessionCurrent), typeof(SessionCurrent));

            services.AddScoped(typeof(IAuthenticationBusinessService), typeof(AuthenticationBusinessService));

            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IUserService), typeof(UserService));

            services.AddScoped(typeof(IRoleRepository), typeof(RoleRepository));
            services.AddScoped(typeof(IRoleService), typeof(RoleService));

            services.AddScoped(typeof(IAddressRepository), typeof(AddressRepository));
            services.AddScoped(typeof(IAddressBusinessService), typeof(AddressBusinessService));
            services.AddScoped(typeof(IAddressService), typeof(AddressService));

            services.AddCors();
            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>(
                provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);

            services.AddControllers()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
                .SetIsOriginAllowed(_ => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
