using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using TEST.Infraestructure;
using TEST.Model;
using TEST.Repository.Mock;
using TEST.Service.Mock;
using Xunit;
using Xunit.Abstractions;
using Xunit.Frameworks.Autofac;

[assembly: TestFramework("TEST.ConfigureTestFramework", "TEST")]
namespace TEST
{
    
    public class ConfigureTestFramework : AutofacTestFramework
    {
         public ConfigureTestFramework(IMessageSink diagnosticMessageSink)
            : base(diagnosticMessageSink)
        {
        }
          protected override void ConfigureContainer(ContainerBuilder builder)
        {
           builder.RegisterModule(new AutofacInfraestructure());
           builder.RegisterModule(new ImapperAutofac());
           builder.RegisterModule(new AutofacRepositoryMock());
           builder.RegisterModule(new AutofacServicesMock());
        }
    }
}