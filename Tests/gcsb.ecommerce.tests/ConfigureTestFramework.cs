using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Bogus;
using gcsb.ecommerce.application.Interfaces.Services;
using gcsb.ecommerce.application.UseCases.Order.CreateOrder.Handlers;
using gcsb.ecommerce.infrastructure.Modules;
using gcsb.ecommerce.infrastructure.Service;
using gcsb.ecommerce.webapi.Modules;
using Xunit;
using Xunit.Abstractions;
using Xunit.Frameworks.Autofac;

[assembly: TestFramework("gcsb.ecommerce.tests.ConfigureTestFramework", "gcsb.ecommerce.tests")]
namespace gcsb.ecommerce.tests
{
    public class ConfigureTestFramework : AutofacTestFramework
    {
        public ConfigureTestFramework(IMessageSink diagnosticMessageSink)
           : base(diagnosticMessageSink)
        {
            Environment.SetEnvironmentVariable("USEINMEMORY", true.ToString());
        }
        protected override void ConfigureContainer(ContainerBuilder builder)
        {
          builder.RegisterInstance<Faker>(new Faker("pt_BR")).AsSelf().SingleInstance();
          builder.RegisterModule(new ApplicationModule());
          builder.RegisterModule(new InfrastructureModule());
          builder.RegisterModule(new WebapiModule());
        }
    }
}