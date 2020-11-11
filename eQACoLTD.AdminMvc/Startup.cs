using eQACoLTD.AdminMvc.Handlers;
using eQACoLTD.AdminMvc.Services;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using Microsoft.IdentityModel.Logging;

namespace eQACoLTD.AdminMvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<BearerTokenHandler>();
            services.AddAuthentication(config =>
            {
                config.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config=> {
                config.AccessDeniedPath = "/Auth/AccessDenied";
                config.LogoutPath = "/Home/Index";
            })
             .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, config =>
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
                 config.Scope.Add("offline_access");
                 config.ClaimActions.MapUniqueJsonKey("role", "role","role");
                 config.TokenValidationParameters = new TokenValidationParameters()
                 {
                     RoleClaimType = JwtClaimTypes.Role
                 };
               
             });    
            services.AddDistributedMemoryCache();
            services.AddSession(options=> {
                options.IdleTimeout = TimeSpan.FromHours(2);
            });
            services.AddHttpClient("APIClient",client=> {
                client.BaseAddress =new Uri(Configuration["APIServerHost"]);
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
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddTransient<IAccountAPIService, AccountAPIService>();
            services.AddTransient<IProductAPIService, ProductAPIService>();
            services.AddTransient<ICustomerAPIService, CustomerAPIService>();
            services.AddTransient<ICategoryAPIService, CategoryAPIService>();
            services.AddTransient<ISupplierAPIService, SupplierAPIService>();
            services.AddTransient<IOrderAPIService,OrderAPIService>();
            services.AddTransient<IReportService,ReportService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
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
