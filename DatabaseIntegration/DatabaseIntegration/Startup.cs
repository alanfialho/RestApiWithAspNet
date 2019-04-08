using DatabaseIntegration.Model.Context;
using DatabaseIntegration.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace DatabaseIntegration
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment, ILogger<Startup> logger)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration["MySqlConnection:ConnectionString"];
            services.AddDbContext<CursoAspNetCoreContext>(options => options.UseMySql(connectionString));
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IBookService, BookService>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddApiVersioning();
            ExecuteMigration(connectionString);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => 
            {
                var validationParameters = options.TokenValidationParameters;
                validationParameters.IssuerSigningKey = SignInCredentialsHandler.Credentials.Key;
                validationParameters.ValidAudience = Token.Audience;
                validationParameters.ValidIssuer = Token.Issuer;
                validationParameters.ValidateIssuerSigningKey = true;
                validationParameters.ValidateLifetime = true;
                validationParameters.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(options => 
            {
                options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Database Integration API", Version = "v1" });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        private void ExecuteMigration(string connectionString)
        {
            if (_hostingEnvironment.IsDevelopment())
            {
                try
                {
                    var mySqlConnection = new MySqlConnection(connectionString);

                    var evolve = new Evolve.Evolve(mySqlConnection, message => _logger.LogInformation(message))
                    {
                        Locations = new List<string> { "db/scripts", "db/dataset" },
                        IsEraseDisabled = true,
                    };

                    evolve.Migrate();
                }
                catch (Exception ex)
                {
                    _logger.LogCritical("Migration failed", ex);
                    throw;
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            SetUpExceptionHandler(app);
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Database Integration V1");
            });

            var rewriteOptions = new RewriteOptions();
            rewriteOptions.AddRedirect("^$", "swagger");

            app.UseRewriter(rewriteOptions);
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        public static void SetUpExceptionHandler(IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;

                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();
                });
            });
        }
    }
}
