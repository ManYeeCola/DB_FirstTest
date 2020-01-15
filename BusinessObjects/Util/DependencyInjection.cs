using BusinessObjects.Services;
using BusinessObjects.Util;
using System;
using System.Collections.Generic;
using System.Text;

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
