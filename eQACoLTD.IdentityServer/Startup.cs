using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using EmailService;
using eQACoLTD.Data.DBContext;
using eQACoLTD.Data.Entities;
using eQACoLTD.IdentityServer.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;

namespace eQACoLTD.IdentityServer
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
            var emailConfig = Configuration.GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "IdentityServer.Cookie";
                config.LoginPath = "/Account/Login";
                config.LogoutPath = "/Account/Logout";
            });

            services.AddIdentity<AppUser, AppRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.User.RequireUniqueEmail = true;
                config.SignIn.RequireConfirmedEmail = true; 
            }).AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddIdentityServer()
                .AddConfigurationStore(opt =>
                {
                    opt.ConfigureDbContext = c => c.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    sql => sql.MigrationsAssembly(migrationAssembly));
                })
                .AddOperationalStore(opt =>
                {
                    opt.ConfigureDbContext = o => o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    sql => sql.MigrationsAssembly(migrationAssembly));
                })
                .AddAspNetIdentity<AppUser>()
                .AddProfileService<ProfileService>()
                .AddDeveloperSigningCredential();

        
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.Configure<DataProtectionTokenProviderOptions>(opt => 
                opt.TokenLifespan = TimeSpan.FromHours(2));
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "1006379685493-ujqe2ae3k3jbsn2k8pckphb561cfh5in.apps.googleusercontent.com";
                options.ClientSecret = "mnPUnxRHKwrTQ0_j43fgANza";
            }).AddFacebook(options=> {
                options.ClientId = "368626310922622";
                options.ClientSecret = "9d646344a5b894c6e81fa7f1387275f3";
            });
            services.AddTransient<AppIdentityDbContext, AppIdentityDbContext>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
