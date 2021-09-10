using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyIssue.Infrastructure.Server;
using MyIssue.Web.Services;

namespace MyIssue.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StaticConfiguration = configuration;
        }
        public static IConfiguration StaticConfiguration { get; set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSession(opt =>
            {
                opt.Cookie.Name = "MyIssue.Session";
                opt.Cookie.SameSite = SameSiteMode.None;
            });
            Console.WriteLine(Configuration.GetValue<string>("ServerConnection:ServerIp"));
            Console.WriteLine(Configuration.GetValue<int>("ServerConnection:Port"));
           // var builder = new ConfigurationBuilder()
           //     .AddJsonFile("appsettings.json");
           // IConfiguration config = builder.Build();
            //services.AddScoped<IConfiguration>(_ => config);
            services.AddScoped<IServerConnector, ServerConnector>(_=> new ServerConnector(Configuration.GetValue<string>("ServerConnection:ServerIp"), Configuration.GetValue<int>("ServerConnection:Port")));
            services.AddScoped<ITaskService, TaskService>();
            
            //services.AddSingleton<ServerConnector>(p =>
                //;
            
            //services.AddIdentity<>()
                
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
