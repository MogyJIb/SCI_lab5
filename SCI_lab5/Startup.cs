using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbDataLibrary.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SCI_lab5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SCI_lab5.Data;
using SCI_lab5.Middleware;
using SCI_lab5.Services;

namespace SCI_lab5
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
            services.AddTransient<ToursSqliteDbContext>();
            services.AddMemoryCache();
            services.AddResponseCaching();
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("SqliteUserConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            

            services.AddMvc(options =>
            {
                options.CacheProfiles.Add("Caching",
                   new CacheProfile()
                   {
                        Duration = 2 * 12 + 240,
                        Location = ResponseCacheLocation.Any
                   });
                options.CacheProfiles.Add("NoCaching",
                   new CacheProfile()
                   {
                        Location = ResponseCacheLocation.None,
                        NoStore = true
                   });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseStaticFiles();
            app.UseSession();
            app.UseResponseCaching();
            app.UseAuthentication();
            app.UseDbInitializer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
