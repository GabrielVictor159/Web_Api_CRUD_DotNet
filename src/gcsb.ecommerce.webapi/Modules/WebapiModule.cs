using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using gcsb.ecommerce.webapi.UseCases.Order.CreateOrder;
using gcsb.ecommerce.webapi.UseCases.Order.GetOrder;
using gcsb.ecommerce.webapi.UseCases.Order.RemoveOrder;

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
        }
    }
}