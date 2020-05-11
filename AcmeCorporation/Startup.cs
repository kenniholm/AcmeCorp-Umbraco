using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using AcmeCorporation.Models;
using ageCalc;
using AcmeCorporation.Data;
using Microsoft.AspNetCore.Identity;
using AcmeCorporation.Areas.Identity.Data;

namespace AcmeCorporation
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

            services.AddDbContext<AcmeCorporationContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("AcmeCorporationContext")));

            services.AddDefaultIdentity<Admin>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<AcmeIdentityContext>();

            services.AddScoped<IAgeCalculation, AgeCalculator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider provider)
        {
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

            SetDefaultAdmin(provider).Wait();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        private async Task SetDefaultAdmin(IServiceProvider provider)
        {
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = provider.GetRequiredService<UserManager<Admin>>();
            string roleName = "Administrator";

            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                IdentityResult roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            Admin DefaultAdmin = await userManager.FindByEmailAsync("admin@admin.com");
            if (DefaultAdmin != null)
            {
                await userManager.AddToRoleAsync(DefaultAdmin, roleName);
            }
        }
    }
}
