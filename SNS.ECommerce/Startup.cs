using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SNS.ECommerce.Data;
using SNS.ECommerce.Infrastructure.Interfaces;
using SNS.ECommerce.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SNS.ECommerce
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Get environment name
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{envName}.json", optional: false)
                .Build();
            services.AddControllersWithViews();
            services.AddScoped<IProductServices, ProductService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddDbContext<ApplicationContext>(options =>
                                     options.UseSqlServer(configuration.GetConnectionString("SNSConnection")));
            services.AddDirectoryBrowser();
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.PageViewLocationFormats.Add("~/Views/Shared/_Layout.cshtml" + RazorViewEngine.ViewExtension);
            });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            ServiceLocator.Register(httpContextAccessor);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), @"Scripts")),
                RequestPath = "/Scripts"
            });

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Images")),
                RequestPath = new PathString("/Images")
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Index}/{id?}");
            });
        }
    }
}
