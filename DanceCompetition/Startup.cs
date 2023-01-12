using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DanceCompetition.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using DanceCompetition.Models;

namespace DanceCompetition
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
            services.AddIdentity<DanceCompetitionUser, DanceCompetitionRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddDefaultUI()
                .AddEntityFrameworkStores<DanceCompetitionContext>();

            services.AddControllersWithViews();

            services.AddRazorPages();

            services.AddDbContext<DanceCompetitionContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DanceCompetitionContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            SetupAppDataAsync(app, env);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}"
                );

                endpoints.MapControllerRoute(
                    name: "grade1",
                    pattern: "dancepairs/grade1",
                    defaults: new { controller = "DancePairs", action = "MissingGrades", grade = 1 }
                );

                endpoints.MapControllerRoute(
                    name: "grade2",
                    pattern: "dancepairs/grade2",
                    defaults: new { controller = "DancePairs", action = "MissingGrades", grade = 2 }
                );

                endpoints.MapControllerRoute(
                    name: "grade3",
                    pattern: "dancepairs/grade3",
                    defaults: new { controller = "DancePairs", action = "MissingGrades", grade = 3 }
                );

                endpoints.MapControllerRoute(
                    name: "results",
                    pattern: "dancepairs/results",
                    defaults: new { controller = "DancePairs", action = "IndexResult" }
                );

                endpoints.MapRazorPages();
            }
            );
        }

        private async Task SetupAppDataAsync(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var userManager = serviceScope.ServiceProvider.GetService<UserManager<DanceCompetitionUser>>();
            using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<DanceCompetitionRole>>();
            using var context = serviceScope.ServiceProvider.GetService<DanceCompetitionContext>();
            if (context == null)
            {
                throw new ApplicationException("Problem in services. Can not initialize context");
            }
            while (true)
            {
                try
                {
                    context.Database.OpenConnection();
                    context.Database.CloseConnection();
                    context.Database.EnsureCreated();
                    break;
                }
                catch (SqlException e)
                {
                    if (e.Message.Contains("The login failed.")) { break; }
                    System.Threading.Thread.Sleep(1000);
                }
            }
            await SeedData.SeedIdentity(userManager, roleManager);
            context.SaveChanges();
        }

    }
}
