using FinCache.InMemory.Config;
using FinCache.InMemory.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCache.InMemory.Extensions
{
    public static class FinCacheExtensions
    {
        public static IServiceCollection AddFinCache(this IServiceCollection services, FinCacheConfig config)
        {                
            services.AddSingleton<IFinCache>(x => new FinCache(config));
            return services;
        }

        public static bool IsNull(this object T)
        {
            return T is null;
        }
    }
}
