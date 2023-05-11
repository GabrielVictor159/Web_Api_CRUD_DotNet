using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Web_Api_CRUD.Infraestructure;

namespace TEST.Infraestructure
{
    public class AutofacInfraestructure : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
          builder.RegisterType<ApplicationDbContextMock>().As<ApplicationDbContext>().InstancePerLifetimeScope();
        }
    
        
    }
}