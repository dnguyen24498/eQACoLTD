using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using eQACoLTD.AdminMvc.Services;
using eQACoLTD.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace eQACoLTD.AdminMvc
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
            services.AddAuthentication(config =>
            {
                config.DefaultScheme = "Cookie";
                config.DefaultChallengeScheme = "oidc";
            }).AddCookie("Cookie",config=> {
                config.ExpireTimeSpan=TimeSpan.FromHours(2);
                config.Cookie.Name = "CustomCookieAspNetCore";
            })
             .AddOpenIdConnect("oidc", config =>
             {
                 config.Authority = Configuration["IdentityServerHost"];
                 config.ClientId = Configuration["ClientId"];
                 config.ClientSecret = Configuration["ClientSecret"];
                 config.SaveTokens = true;
                 config.ResponseType = "code";
                 config.SignedOutCallbackPath = "/Home/Index";
                 config.MaxAge = TimeSpan.FromHours(2);
                 config.Scope.Add("backend_api");
                 config.UseTokenLifetime = true;
                 //config.Scope.Add("roles");

             });
            services.AddDistributedMemoryCache();
            services.AddSession(options=> {
                options.IdleTimeout = TimeSpan.FromHours(2);
            });
            services.AddHttpClient();
            services.AddControllersWithViews();
            services.AddTransient<IUserApiClient, UserApiClient>();
            services.AddTransient<IAccountsApiClient, AccountsApiClient>();
            services.AddTransient<IRoleApiClient, RoleApiClient>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddRazorPages().AddRazorRuntimeCompilation();
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

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
