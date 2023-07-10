using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using gcsb.ecommerce.infrastructure.Modules;
using gcsb.ecommerce.webapi.Modules;

namespace gcsb.ecommerce.webapi.DependencyInjection
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder AddAutofacRegistration(this ContainerBuilder builder)
        {
            builder.RegisterModule(new WebapiModule());
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new ApplicationModule());             
            return builder;
        }
    }
}