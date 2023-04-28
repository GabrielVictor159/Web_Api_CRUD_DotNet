using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Web_Api_CRUD.Services.Token;

namespace Web_Api_CRUD.Services
{
    public class AutoFacServices : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TokenService>().As<ITokenService>();
            builder.RegisterType<ClienteService>().As<IClienteService>().InstancePerRequest();
            builder.RegisterType<PedidoService>().As<IPedidoService>().InstancePerRequest();
            builder.RegisterType<ProdutoService>().As<IProdutoService>().InstancePerRequest();
            builder.RegisterType<PedidoProdutoService>().As<IPedidoProdutoService>().InstancePerRequest();
        }
    }
}