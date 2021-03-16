using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Polly;
using FXFinder.API.ExtensionClasses;
using FXFinder.Core.DataAccess;
using FXFinder.Core.DBModels;
using FXFinder.Core.Managers;
using FXFinder.Core.Managers.Interfaces;
using FXFinder.Core.Util.Models;
using FXFinder.Core.Util;

namespace FXFinder.API
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
            var jwtSecret = Configuration["JwtSettings:Secret"];
            var connstr = Configuration["ConnectionString:FXFinder.ConnectionString"];
            var baseurl = Configuration["BaseFixerUrl"];
            var pwd = Configuration["JwtSettings:Password"];
            // for docker db
            //var connectionstr = $@"Server=db,1433;Initial Catalog=walletsystem;User ID=aeon;Password={pwd};";

            //  Repo Service
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailUtil, EmailUtil>();

            services.AddDbContext<WalletDbContext>(options => options.UseSqlServer(connstr, c => c.MigrationsAssembly("FXFinder.Core")));

            #region Managers

            services.AddScoped<IAuthManager, AuthManager>();
            services.AddScoped<IAdminManager, AdminManager>();
            services.AddScoped<IWalletManager, WalletManager>();
            services.AddScoped<ICurrencyManager, CurrencyManager>();


            #endregion

            #region Auth/Auth Setup

            services.AddAppAuthentication(jwtSecret);
            services.AddAuthorization(opt =>
            {
                //Just the admin
                opt.AddPolicy(AuthorizedUserTypes.Admin, policy =>

                policy.RequireRole(UserRoles.Admin));

                // Just the user
                opt.AddPolicy(AuthorizedUserTypes.Users, policy =>

                policy.RequireRole(UserRoles.User));
                // user and admin
                opt.AddPolicy(AuthorizedUserTypes.UserAndAdmin, policy =>

                policy.RequireRole(UserRoles.User, UserRoles.Admin));


            });

            #endregion
            services.AddHttpContextAccessor();
            // Adding CORS allowing all origin for development purposes
            
            services.AddHttpClient("FixerApi", client =>
            {
                client.BaseAddress = new Uri(baseurl);
            }).AddTransientHttpErrorPolicy(x =>
                x.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(300)));

            services.AddControllers(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            }).AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );


            services.AddSwaggerGen(c =>
            {
                // configure SwaggerDoc and others
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                // add JWT Authentication to swagger
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "FXFinder.API v1",
                    Description = "Enter JWT Bearer token: ",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });


            });
        }
            // Method to get swagger xml path


            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //var context = services.GetService<WalletDbContext>();
            //context.Database.Migrate();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FXFinder.API v1"));
            }
            
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
