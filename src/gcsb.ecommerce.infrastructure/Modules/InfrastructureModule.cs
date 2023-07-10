using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;

namespace gcsb.ecommerce.infrastructure.Modules
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(InfrastructureException).Assembly)
            .AsImplementedInterfaces().AsSelf().InstancePerLifetimeScope();
            
            Mapper(builder);
            base.Load(builder);
        }
        private void Mapper(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(InfrastructureException).Assembly)
              .Where(t => (t.Namespace ?? string.Empty).Contains("Database") && typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic)
              .As<Profile>();
            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                cfg.AddProfile(profile);
                }
                cfg.AddExpressionMapping();
            })).AsSelf().SingleInstance();
            builder.Register(c => c.Resolve<MapperConfiguration>()
                .CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}