using BusinessObjects.Util;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UnitOfWorkServiceCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services) where TContext : AppDbContext
        {
            //services.AddScoped<IServices, UnitOfWork<TContext>>();


            return services;
        }
    }
}
