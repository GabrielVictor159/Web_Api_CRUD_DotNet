using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Web_Api_CRUD.Services;

namespace TEST.Service.Mock
{
    public class AutofacServicesMock : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ClienteService>().As<IClienteService>().InstancePerLifetimeScope();
            builder.RegisterType<PedidoService>().As<IPedidoService>().InstancePerLifetimeScope();
            builder.RegisterType<ProdutoService>().As<IProdutoService>().InstancePerLifetimeScope();
        }
    
        
    }
}