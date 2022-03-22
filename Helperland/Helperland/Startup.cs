using Helperland.Implementations;
using Helperland.IRepositories;
using Helperland.Repository;
using Helperland.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland
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
            services.AddDataProtection();
            services.Configure<EmailConfig>(Configuration.GetSection("SMTPConfig"));
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("HelperlandDatabse")));
            services.AddControllersWithViews();
            services.AddScoped<IContactRepository, ContactImplementation>();
            services.AddScoped<IUserRepository, UserImplementation>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICipherService, CipherService>();
            services.AddScoped<ILoginRepository, LoginImplementation>();
            services.AddScoped<IServiceRequestRepository, ServiceRequestImplementation>();
            services.AddScoped<IAdminRepository, AdminImplementation>();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.LoginPath = "/Home/Login";
                options.Cookie.Name = "AuthenticationCookie";

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
