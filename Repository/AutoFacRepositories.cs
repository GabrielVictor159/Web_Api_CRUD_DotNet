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
            builder.RegisterType<ClienteRepository>().As<IClienteRepository>().InstancePerRequest();
            builder.RegisterType<PedidoRepository>().As<IPedidoRepository>().InstancePerRequest();
            builder.RegisterType<ProdutoRepository>().As<IProdutoRepository>().InstancePerRequest();
            builder.RegisterType<PedidoProdutoRepository>().As<IPedidoProdutoRepository>().InstancePerRequest();
        }
    }
}