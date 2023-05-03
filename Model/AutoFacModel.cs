using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using Web_Api_CRUD.Model.DTO;

namespace Web_Api_CRUD.Model
{
    public class AutoFacModel : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClienteDTO, Cliente>().ReverseMap();
                cfg.CreateMap<LoginDTO, ClienteDTO>().ReverseMap();
                cfg.CreateMap<PedidoDTO, Pedido>().ReverseMap();
                cfg.CreateMap<PedidoProdutoDTO, PedidoProduto>().ReverseMap();
                cfg.CreateMap<ProdutoDTO, Produto>().ReverseMap();
                cfg.CreateMap<ClienteUpdateDTO, ClienteDTO>().ReverseMap();
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();
        }
    }
}