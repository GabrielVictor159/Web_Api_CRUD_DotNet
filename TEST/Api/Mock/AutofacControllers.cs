using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Web_Api_CRUD.Controllers;

namespace TEST.Api.Mock
{
    public class AutofacControllers : Module
    {
         protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ClienteController>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ProdutoController>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<PedidoController>().AsSelf().InstancePerLifetimeScope();
        }
    }
}