using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
namespace Web_Api_CRUD.Repository
{
    public class AutoFacRepositories : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ClienteRepository>().As<IClienteRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PedidoRepository>().As<IPedidoRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProdutoRepository>().As<IProdutoRepository>().InstancePerLifetimeScope();
        }
    }
}