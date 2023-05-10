using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Web_Api_CRUD.Repository;

namespace TEST.Repository.Mock
{
    public class AutofacRepositoryMock : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterType<ClienteRepositoryMock>().As<IClienteRepository>().InstancePerLifetimeScope();
        builder.RegisterType<PedidoRepositoryMock>().As<IPedidoRepository>().InstancePerLifetimeScope();
        builder.RegisterType<ProdutoRepositoryMock>().As<IProdutoRepository>().InstancePerLifetimeScope();
    }
        
    }
}