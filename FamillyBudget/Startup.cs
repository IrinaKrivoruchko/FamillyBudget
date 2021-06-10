using Common;
using DataStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Users.Services;
using Accounts.Services;
using AccountsStatements.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Authorization.Services;
using DataEntities;
using Microsoft.AspNetCore.Identity;

namespace FamilyBudget
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddDbContext<DatabaseContext>(options =>
            {
                string connectionStr = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionStr);
            });

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = AuthOptions.ISSUER,

                            ValidateAudience = true,
                            ValidAudience = AuthOptions.AUDIENCE,
                            ValidateLifetime = true,

                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                    });
            services.AddAuthorization();
            RegisterServicesMapper(services);
            RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            DbInitializer.Initialize(app.ApplicationServices);
        }

        public void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<AccountService>();
            services.AddScoped<TransferBetweenAccounts>();
        }

        public void RegisterServicesMapper(IServiceCollection services)
        {
            var configExpression = ServiceMapper.GetMapperConfiguration(new string[]
            {
                 "Users.Services",
                 "Accounts.Services",
                 "AccountsStatements.Services"
            });
            services.AddSingleton(configExpression);
            services.AddScoped<IServiceMapper, ServiceMapper>();
        }
    }
}
