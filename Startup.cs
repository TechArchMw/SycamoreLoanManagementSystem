using Audit.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SycamoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechArchGeneral;
using TechArchGeneral.Security.JSONWebToken;

namespace SycamoreWebApp
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
            services.
               AddDbContext<SycamoreDBContext>
               (
                    options => options
                   .UseSqlServer(Configuration.GetConnectionString("SycamoreConnectionString")));
            services.AddRazorPages();
            var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();
            //services.AddTechArchIdentityModels();
            services.AddControllers();
            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));
            //services.AddTechArchServices();
            //services.AddTechArchAuth(jwtSettings);
            //services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddControllersWithViews();
            services.AddMvcCore(mvc =>
            {
                mvc.Filters.Add(new AuditIgnoreActionFilter());
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseTechArchAuth();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
                endpoints.MapRazorPages();
            });

            #region Audit manager
            app.UseAuditMiddleware(_ => _
                        .FilterByRequest(rq => !rq.Path.Value.EndsWith("favicon.ico"))
                        .WithEventType("{verb}:{url}")
                        .IncludeHeaders()
                        .IncludeResponseHeaders()
                        .IncludeRequestBody()
                        .IncludeResponseBody());

            Audit.Core.Configuration.Setup()
             .UseFileLogProvider(config => config
             .FilenamePrefix("SycamoreWebApp_")
             .Directory(@"logs\"));
            #endregion
        }
    }
}
