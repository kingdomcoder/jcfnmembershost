using System;
using System.Text;
using JCFNMembers.Domain.Entities;
using JCFNMembers.Domain.Repositories.Abstract;
using JCFNMembers.Domain.Repositories.Concrete;
using JCFNMembers.Domain.Services.Abstract;
using JCFNMembers.Domain.Services.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace JCFNMembers.WebAPIHost {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();

            /*********** My Addition *************/
            // 1. EntityFramework support for Sqlserver
            services.AddEntityFrameworkSqlServer();

            // 2. Db Connection
            services.AddDbContext<MyDbContext>(myOption =>
            myOption.UseSqlServer(Configuration.GetConnectionString("myConnection")));

            services.AddDbContext<MyUserDbContext>(myOption =>
            myOption.UseSqlServer(Configuration.GetConnectionString("myConnection")));


            // 3. ASP.NET Identity support
            services.AddIdentity<ApplicationUser, IdentityRole>(
                opts => {
                    opts.Password.RequireDigit = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequiredLength = 7;
                })
                .AddEntityFrameworkStores<MyUserDbContext>()
                .AddDefaultTokenProviders(); //Need this for IUserTokenProvider need to be registered for GeneratePasswordResetTokenAsync


            // 4.Dependency Injection
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, EFUserRepository>();

            // 5. CORS
            services.AddCors(options => {
                options.AddPolicy("AllowAllOrigins",
                    builder => {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });


            //6 . Add Authentication with JWT token
            services.AddAuthentication(opts => {
                opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Add Jwt token support
            .AddJwtBearer(cfg => {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters() {
                    // standard configuration
                    ValidIssuer = Configuration["Auth:Jwt:Issuer"],  //  .....appsettings.json
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:Key"])),
                    ValidAudience = Configuration["Auth:Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero,

                    // security switches
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true
                };
                cfg.IncludeErrorDetails = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); // my addition
            app.UseAuthentication();    // my addition


            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            //Added by Oz 12/17/20. This enable to bust cache at local so that new angular update alwasy reflect on UI
            app.UseStaticFiles(new StaticFileOptions() {
                OnPrepareResponse = context => {
                    context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                    context.Context.Response.Headers.Add("Expires", "-1");
                }
            });
        }
    }
}
