using Common;
using DataStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Users.Services;
using Accounts.Services;
using AccountsStatements.Services;

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

            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();

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
