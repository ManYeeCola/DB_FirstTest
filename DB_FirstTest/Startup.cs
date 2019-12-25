using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObjects.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BusinessObjects.Util;
using BusinessObjects.Dao;
using BusinessObjects.Services;
using DB_FirstTest.Models;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Reflection;

namespace DB_FirstTest
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
            AppDbContext.connectStr = Configuration.GetConnectionString("University");
            services.AddControllersWithViews().AddControllersAsServices();
            services.AddControllersWithViews();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //模块化注入
            //builder.RegisterModule<AutoFacModule>();
            //注入entity层的repository类builder.RegisterType(typeof(TUserRepository)).As(typeof(IUserRepository)).InstancePerDependency();
            builder.RegisterType(typeof(AppDbContext)).As(typeof(AppDbContext)).InstancePerDependency();
            //批量注入Repository的类
            builder.RegisterAssemblyTypes(typeof(CourseDao).Assembly)
                   .Where(t => t.Name.EndsWith("Dao"))
                   .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(CourseServices).Assembly)
                   .Where(t => t.Name.EndsWith("Services"))
                   .AsImplementedInterfaces();
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
