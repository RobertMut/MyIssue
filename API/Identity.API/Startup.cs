using System;
using System.Reflection;
using System.Text;
using IdentityServer.LdapExtension.Extensions;
using IdentityServer.LdapExtension.UserModel;
using IdentityServer.LdapExtension.UserStore;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyIssue.Identity.API.Auth.Configuration;
using MyIssue.Identity.API.Auth.GrantValidators;
using MyIssue.Identity.API.Extensions;
using MyIssue.Identity.API.Infrastructure;
using MyIssue.Identity.API.Model;
using MyIssue.Identity.API.Services;

namespace MyIssue.Identity.API
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
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<IdentityContext>(o =>
            {
                o.UseSqlServer(Configuration["ConnectionString"],
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(migrationAssembly);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    });
            });
            services.AddScoped<IAuthService, AuthService>(); 
            services.AddDbMigration<IdentityContext>();
            //services.AddIdentity<User, UserType>()
            //    .AddDefaultTokenProviders();
            var builder = services.AddIdentityServer(opt =>
                {
                    //opt.Events.RaiseErrorEvents = true;
                    //opt.Events.RaiseInformationEvents = true;
                    //opt.Events.RaiseFailureEvents = true;
                    //opt.Events.RaiseFailureEvents = true;
                    //opt.Events.RaiseSuccessEvents = true;
                    opt.UserInteraction.LoginUrl = "/Account/Login";
                    opt.UserInteraction.LogoutUrl = "/Account/Logout";
                    opt.UserInteraction.ErrorUrl = "/Home/error";
                }).AddInMemoryClients(Configuration.GetSection("IdentityServer:Clients"))
                .AddInMemoryApiScopes(Configuration.GetSection("IdentityServer:ApiScopes"))
                .AddInMemoryApiResources(Configuration.GetSection("IdentityServer:ApiResources"))
                .AddExtensionGrantValidator<MyIssueGrantValidator>()
                .AddDeveloperSigningCredential();
            if (Configuration.GetValue<bool>("LDAPEnabled") == true)
            {
                builder = builder.AddLdapUsers<OpenLdapAppUser>(Configuration.GetSection("LDAP"), UserStore.InMemory)
                    .AddProfileService<HostProfileService>();
            }
                
            builder.AddDeveloperSigningCredential();
            services.AddAuthentication().AddLocalApi();
            services.AddControllersWithViews();
            services.Configure<AuthenticationOptions>(Configuration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity.API v1"));
            }

            app.UseStaticFiles();
            app.UseCors(b =>
            {
                b.AllowAnyOrigin();
                b.AllowAnyMethod();
                b.AllowAnyHeader();
            });
            app.UseHttpsRedirection();
            
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
