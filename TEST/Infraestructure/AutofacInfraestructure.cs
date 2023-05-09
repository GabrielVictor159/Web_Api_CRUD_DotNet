using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace TEST.Infraestructure
{
    public class AutofacInfraestructure : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContextMock>().AsSelf().InstancePerLifetimeScope();
        }
    
        
    }
}