using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Infraestructure;
using Autofac;

namespace Web_Api_CRUD.Infraestructure
{
    public class AutoFacInfra : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<MessagingQeue>().As<IMessagingQeue>().InstancePerLifetimeScope();
        }
    }
}