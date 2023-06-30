using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using gcsb.ecommerce.webapi.UseCases.Client.CreateClient;
using gcsb.ecommerce.webapi.UseCases.Client.GetClients;
using gcsb.ecommerce.webapi.UseCases.Client.LoginClient;
using gcsb.ecommerce.webapi.UseCases.Client.UpdateClient;
using gcsb.ecommerce.webapi.UseCases.Order.CreateOrder;
using gcsb.ecommerce.webapi.UseCases.Order.GetOrder;
using gcsb.ecommerce.webapi.UseCases.Order.RemoveOrder;
using gcsb.ecommerce.webapi.UseCases.Product.CreateProduct;
using gcsb.ecommerce.webapi.UseCases.Product.DeleteProducts;
using gcsb.ecommerce.webapi.UseCases.Product.GetProducts;
using gcsb.ecommerce.webapi.UseCases.Product.UpdateProducts;

namespace gcsb.ecommerce.webapi.Modules
{
    public class WebapiModule : Module
    {
         protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CreateOrderPresenter>()
             .AsImplementedInterfaces()
             .InstancePerLifetimeScope().AsSelf();
             builder.RegisterType<GetOrderPresenter>()
             .AsImplementedInterfaces()
             .InstancePerLifetimeScope().AsSelf();
             builder.RegisterType<RemoveOrderPresenter>()
             .AsImplementedInterfaces()
             .InstancePerLifetimeScope().AsSelf();
             builder.RegisterType<CreateClientPresenter>()
             .AsImplementedInterfaces()
             .InstancePerLifetimeScope().AsSelf();
             builder.RegisterType<GetClientsPresenter>()
             .AsImplementedInterfaces()
             .InstancePerLifetimeScope().AsSelf();
             builder.RegisterType<LoginClientPresenter>()
             .AsImplementedInterfaces()
             .InstancePerLifetimeScope().AsSelf();
             builder.RegisterType<UpdateClientPresenter>()
             .AsImplementedInterfaces()
             .InstancePerLifetimeScope().AsSelf();
             builder.RegisterType<CreateProductPresenter>()
             .AsImplementedInterfaces()
             .InstancePerLifetimeScope().AsSelf();
             builder.RegisterType<DeleteProductPresenter>()
             .AsImplementedInterfaces()
             .InstancePerLifetimeScope().AsSelf();
             builder.RegisterType<UpdateProductPresenter>()
             .AsImplementedInterfaces()
             .InstancePerLifetimeScope().AsSelf();
             builder.RegisterType<GetProductsPresenter>()
             .AsImplementedInterfaces()
             .InstancePerLifetimeScope().AsSelf();
             builder.RegisterType<CreateOrderPresenter>()
             .AsImplementedInterfaces()
             .InstancePerLifetimeScope().AsSelf();
        }
    }
}