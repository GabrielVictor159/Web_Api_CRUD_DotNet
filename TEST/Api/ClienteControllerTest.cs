using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_Api_CRUD.Controllers;
using Web_Api_CRUD.Model;
using Web_Api_CRUD.Model.DTO;
using Web_Api_CRUD.Repository;
using Web_Api_CRUD.Services;
using Xunit;
using FluentAssertions;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace TEST.Api
{
    public class ClienteControllerTest
    {
        private readonly IMapper _mapper;
        public ClienteControllerTest()
        {
         _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClienteDTO, Cliente>().ReverseMap();
                cfg.CreateMap<LoginDTO, ClienteDTO>().ReverseMap();
                cfg.CreateMap<PedidoDTO, Pedido>().ReverseMap();
                cfg.CreateMap<PedidoProdutoDTO, PedidoProduto>().ReverseMap();
                cfg.CreateMap<ProdutoDTO, Produto>().ReverseMap();
                cfg.CreateMap<ClienteUpdateDTO, ClienteDTO>().ReverseMap();
                cfg.CreateMap<ProdutoAtualizarDTO, ProdutoDTO>().ReverseMap();
            }).CreateMapper();
        }
        [Fact]
        public async void Login()
        {
            string usuario = "jubileu";
            string senha = "Testando";
            Moq.Mock<IClienteRepository> mock = new Moq.Mock<IClienteRepository>();
            mock.Setup(e => e.Login(usuario,senha)).ReturnsAsync(new Cliente() { Id = Guid.NewGuid(), Nome = usuario, Role = "Admin", Senha = senha });
            ClienteService clienteService = new ClienteService(mock.Object);
            ClienteController clienteController = new ClienteController(clienteService,_mapper);
            var result =  await clienteController.Login(new LoginDTO() { Nome=usuario, Senha=senha});
            result.Result.Should().BeOfType<OkObjectResult>();
            var result2 = await clienteController.Login(new LoginDTO() { Nome=usuario, Senha=senha+"5"});
            result2.Result.Should().BeOfType<NotFoundResult>();
        }
    }
}
