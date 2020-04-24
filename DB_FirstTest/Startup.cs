using Alipay.EasySDK.Factory;
using Alipay.EasySDK.Kernel;
using Autofac;
using Autofac.Extras.DynamicProxy;
using BusinessObjects.Aspect;
using BusinessObjects.Interceptor;
using BusinessObjects.Util;
using DB_FirstTest.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace DB_FirstTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Factory.SetOptions(GetAlipayConfig());
        }
        /// <summary>
        /// ����֧���ӿ�
        /// </summary>
        /// <returns></returns>
        static private Config GetAlipayConfig()
        {
            System.Diagnostics.Debug.WriteLine("Init Config Alipay");
            return new Config()
            {
                Protocol = "https",
                GatewayHost = "openapi.alipaydev.com/gateway.do",
                SignType = "RSA2",

                // �����Ϊ����AppId
                AppId = "2016100100642629",
                //// �����Ϊ����Ӧ�ù�Կ֤���ļ�·��
                //MerchantCertPath = "/home/foo/appCertPublicKey_2019051064521003.crt",
                //// �����Ϊ����֧������Կ֤���ļ�·��
                //AlipayCertPath = "/home/foo/alipayCertPublicKey_RSA2.crt",
                //// �����Ϊ����֧������֤���ļ�·��
                //AlipayRootCertPath = "/home/foo/alipayRootCert.crt",
                //// �����Ϊ����PKCS1��ʽ��Ӧ��˽Կ
                //MerchantPrivateKey = "MIIEvQIBADANB ... ...",

                // ������÷�֤��ģʽ�������踳ֵ���������֤��·������Ϊ��ֵ���µ�֧������Կ�ַ�������
                AlipayPublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAkiMg5QAvKJP09harp6C+FTX0eV4qYE2EFHA6j485mibOb+AomS08V/MMZWCjPRtiyU0QNuH0BB2A4y8D4DTQTrmiWWcBCzCVCCSttFBDz/bgx/uQoEohmp11He89BpV8fvpsouytRe53usmYZaocKHrgbgsyEFPbHi/iwA9R1CE6PpnU3QvhGUTQar07YcCNh9TfHsfTh0ZOtMn4TicJ7rtZacZ5ivMen1lTrgAirIMtfwCMxpfz8NwHesJ19wSNMi4OZL1wqUGmf/43XacAvpbfLjya2ru0jsqVqA72S6HN8kIdP3BnEZY8waJKV7LAI8FarLPdmWsE8w2JFpDXtQIDAQAB"
            };
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
            builder.RegisterType<TransactionInterceptor>();//ע�������� ����ע��

            builder.RegisterAssemblyTypes(typeof(BusinessObjects.Util.IDependency).Assembly)
                   .Where(type => typeof(IDependency).IsAssignableFrom(type) && !type.GetTypeInfo().IsAbstract)
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope()
                   .EnableInterfaceInterceptors();
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
