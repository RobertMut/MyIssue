using System;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyIssue.Main.API.Extensions;
using MyIssue.Main.API.Infrastructure;
using MyIssue.Main.API.Infrastructure.Swagger;
using MyIssue.Main.API.Services;

namespace MyIssue.Main.API
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
            services
                .AddDbContext<MyIssueContext>(o =>
                {
                    o.UseSqlServer(Configuration["ConnectionString"],
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30),
                                errorNumbersToAdd: null);
                        });
                });
            services.AddDbMigration<MyIssueContext>();

            //Console.WriteLine(Configuration.GetValue<string>("Token:Audience"));
            //Console.WriteLine(Configuration.GetValue<string>("Token:Issuer"));
            // services.AddAuthentication().AddIdentityServerAuthentication("Bearer", opt =>
            // {
            //     opt.ApiName = "API";
            //     opt.Authority = Configuration.GetValue<string>("API:Identity");
            //     opt.RequireHttpsMetadata = Configuration.GetValue<bool>("API:RequireHttps");
            //     
            // });
            services.AddAuthentication(options =>
            {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearer =>
            {
                bearer.Authority = "https://127.0.0.1:6001";
                bearer.RequireHttpsMetadata = false;
                bearer.Audience = "server_api";
                bearer.BackchannelHttpHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
            });


            services.AddScoped<IUserService, UserService>();
            services.AddHttpContextAccessor();
            services.AddSingleton<IUriService, UriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });
            services.AddControllers();
            services.AddSwagger(Configuration);
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }
            app.UseRouting();
            app.UseSwaggerWithAuthorization(); 
            app.UseAuthentication();
            
           // app.UseHttpsRedirection();



            //app.UseMiddleware<JwtMiddleware>();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
