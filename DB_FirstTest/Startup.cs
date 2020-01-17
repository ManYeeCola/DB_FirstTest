using Autofac;
using BusinessObjects.Aspect;
using BusinessObjects.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

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
            services.AddDbContext<AppDbContext>(
                options =>
                    options.UseSqlServer(Configuration.GetConnectionString("University")),
                ServiceLifetime.Scoped
            ) ;
            services.AddControllersWithViews().AddControllersAsServices();
            services.AddControllersWithViews();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //builder.Register(l => new Logger(System.Console.Out))//注册拦截器 命名注册
            //    .Named<IInterceptor>("_logger");
            builder.Register(l => new Logger(System.Console.Out));//注册拦截器 类型注册

            //Type basetype = typeof(IDependency);  //无效 属于手动使用Autofac获取
            //builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly())
            //    .Where(t => basetype.IsAssignableFrom(t) && t.IsClass)
            //    .AsImplementedInterfaces()
            //    .InstancePerLifetimeScope();

            //Assembly[] assemblies = GetYourAssemblies(); //暂不使用
            //
            //builder.RegisterAssemblyTypes(assemblies)
            //    .AsClosedTypesOf(typeof(IHandler<>));

            Type baseType = typeof(IDependency);
            /* 3、获取所有程序集
             * 当CLR COM服务器初始化时，它会创建一个AppDomain。 AppDomain是一组程序集的逻辑容器
             */
            //var assemblies = AppDomain.CurrentDomain.GetAssemblies();//这种写法AppDomain重新启动后会丢失程序集
            //var assemblies = System.Web.Compilation.BuildManager.GetReferencedAssemblies().Cast<System.Reflection.Assembly>().ToArray();
            //var assemblies = System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromAssemblyPath("")

            /* 4、自动注册接口
             * 筛选出对应条件的接口
             * IsAssignableFrom：返回true的条件：是否直接或间接实现了IDependency
             * IsAbstract：是否为抽象类
             * AsImplementedInterfaces：以接口的形式注册
             * InstancePerLifetimeScope：同一个Lifetime生成的对象是同一个实例
             */
            //builder.RegisterAssemblyTypes(assemblies)
            //    .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract).AsImplementedInterfaces().InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(typeof(StudentDao).Assembly)
            //       .Where(t => t.Name.EndsWith("Dao"))
            //       .AsImplementedInterfaces()
            //       .InstancePerLifetimeScope()
            //       .EnableInterfaceInterceptors();
            //builder.RegisterAssemblyTypes(typeof(StudentServices).Assembly)
            //       .Where(t => t.Name.EndsWith("Services"))
            //       .AsImplementedInterfaces()
            //       .InstancePerLifetimeScope()
            //       .EnableInterfaceInterceptors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)//01-15 add ", ILoggerFactory loggerFactory"
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
            loggerFactory.AddProvider(new BOLoggerProvider());//01-15 add
        }
    }
}
