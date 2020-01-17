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
            //builder.Register(l => new Logger(System.Console.Out))//ע�������� ����ע��
            //    .Named<IInterceptor>("_logger");
            builder.Register(l => new Logger(System.Console.Out));//ע�������� ����ע��

            //Type basetype = typeof(IDependency);  //��Ч �����ֶ�ʹ��Autofac��ȡ
            //builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly())
            //    .Where(t => basetype.IsAssignableFrom(t) && t.IsClass)
            //    .AsImplementedInterfaces()
            //    .InstancePerLifetimeScope();

            //Assembly[] assemblies = GetYourAssemblies(); //�ݲ�ʹ��
            //
            //builder.RegisterAssemblyTypes(assemblies)
            //    .AsClosedTypesOf(typeof(IHandler<>));

            Type baseType = typeof(IDependency);
            /* 3����ȡ���г���
             * ��CLR COM��������ʼ��ʱ�����ᴴ��һ��AppDomain�� AppDomain��һ����򼯵��߼�����
             */
            //var assemblies = AppDomain.CurrentDomain.GetAssemblies();//����д��AppDomain����������ᶪʧ����
            //var assemblies = System.Web.Compilation.BuildManager.GetReferencedAssemblies().Cast<System.Reflection.Assembly>().ToArray();
            //var assemblies = System.Runtime.Loader.AssemblyLoadContext.Default.LoadFromAssemblyPath("")

            /* 4���Զ�ע��ӿ�
             * ɸѡ����Ӧ�����Ľӿ�
             * IsAssignableFrom������true���������Ƿ�ֱ�ӻ���ʵ����IDependency
             * IsAbstract���Ƿ�Ϊ������
             * AsImplementedInterfaces���Խӿڵ���ʽע��
             * InstancePerLifetimeScope��ͬһ��Lifetime���ɵĶ�����ͬһ��ʵ��
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
