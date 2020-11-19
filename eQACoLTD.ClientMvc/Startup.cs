using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eQACoLTD.ClientMvc.Handlers;
using eQACoLTD.ClientMvc.Services;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

namespace eQACoLTD.ClientMvc
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
            services.AddAuthentication(config =>
            {
                config.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
            {
                config.AccessDeniedPath = "/Auth/AccessDenied";
                config.LogoutPath = "/Home/Index";
            }).AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, config =>
            {
                config.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                config.Authority = Configuration["IdentityServerHost"];
                config.ClientId = Configuration["ClientId"];
                config.ClientSecret = Configuration["ClientSecret"];
                config.SaveTokens = true;
                config.ResponseType = OpenIdConnectResponseType.Code;
                config.Scope.Add("backend_api");
                config.GetClaimsFromUserInfoEndpoint = true;
                config.ClaimActions.DeleteClaim("sid");
                config.ClaimActions.DeleteClaim("idp");
                config.Scope.Add("roles");
                config.ClaimActions.MapUniqueJsonKey("role", "role");
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    RoleClaimType = JwtClaimTypes.Role
                };
                config.Scope.Add("offline_access");
            });
            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromHours(2);
            });
            services.AddTransient<BearerTokenHandler>();
            services.AddHttpClient("APIClient", client => {
                client.BaseAddress = new Uri(Configuration["APIServerHost"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            }).AddHttpMessageHandler<BearerTokenHandler>();
            services.AddHttpClient("IDPClient", client =>
            {
                client.BaseAddress = new Uri(Configuration["IdentityServerHost"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            });
            services.AddControllersWithViews();
            
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IHomeAPIService, HomeAPIService>();
            services.AddTransient<IProductAPIService,ProductAPIService>();
            services.AddTransient<IAccountAPIService,AccountAPIService>();
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
            app.UseStaticFiles();
            app.UseHttpsRedirection();

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
