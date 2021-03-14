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
using Wallets.Services;

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
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(options => 
            //    {
            //        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
            //    });
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
            services.AddScoped<WalletService>();
        }

        public void RegisterServicesMapper(IServiceCollection services)
        {
            var configExpression = ServiceMapper.GetMapperConfiguration(new string[]
            {
                 "Users.Services",
            });
            services.AddSingleton(configExpression);
            services.AddScoped<IServiceMapper, ServiceMapper>();
        }
    }
}
