using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gcsb.ecommerce.webapi.Filters;

namespace gcsb.ecommerce.webapi.DependencyInjection
{
    public static class FiltersExtensions
    {
        public static IServiceCollection AddFilters(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(NotificationFilter));
            });
            return services;
        }
    }
}