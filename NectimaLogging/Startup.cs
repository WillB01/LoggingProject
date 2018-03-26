using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using System.Data.SqlClient;
using NLog.Targets;
using NectimaLogging.Data;
using Microsoft.EntityFrameworkCore;
using NectimaLogging.Controllers;
using NectimaLogging.Models;
using NectimaLogging.Repository;
using Microsoft.Extensions.FileProviders;
using System.IO;
using NectimaLogging.Services;
using ReflectionIT.Mvc.Paging;

namespace NectimaLogging
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ILogEntryRepository, EntryLogRepository>();
            services.AddTransient<IMyServices, MyServices>();
            


            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MyContext")));
           
            services.AddMvc();
            services.AddPaging();
        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            LogManager.Configuration.Variables["connectionString"] = Configuration.GetConnectionString("MyContext");

            //add NLog.Web

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            //var logger = LogManager.GetCurrentClassLogger();
            //logger.Info("Logged in");
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
         Path.Combine(Directory.GetCurrentDirectory(), @"node_modules")),
                RequestPath = new PathString("/vendor")
            });
            app.UseMvc(routes =>
                routes.MapRoute(
                    name: "default",
                    template: "{Controller=Home}/{action=Search}/{id?}"
            ));


        }
    }
}
