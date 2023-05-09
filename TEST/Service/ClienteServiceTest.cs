using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Web_Api_CRUD.Services;
using Web_Api_CRUD.Repository;
using Moq;
using Web_Api_CRUD.Model;
using FluentAssertions;
using Web_Api_CRUD.Model.DTO;
using Web_Api_CRUD.Model.Enums;
using Bogus;
using Web_Api_CRUD.Infraestructure;
using Microsoft.EntityFrameworkCore.InMemory;
using AutoMapper;

namespace TEST.Service
{

    public class ClienteServiceTest
    {
        private readonly ApplicationDbContextMock _context;
          private readonly IMapper _mapper;
         public ClienteServiceTest()
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
             var faker = new Faker("pt_BR"); 
            List<Cliente> listCliente = new ();
            for(int i=0;i<20; i++)
            {
                listCliente.Add(new Cliente(){Id = Guid.NewGuid(), Nome = faker.Name.FullName(), Senha = faker.Random.String2(10), Role = Policies.ADMIN.ToString()});
            }
        _context = new ApplicationDbContextMock();
        _context.clientes.AddRange(listCliente);
        _context.SaveChanges();
         }
        [Fact]
        public async void LoginTest()
        {
            Moq.Mock<IClienteRepository> mock = new Moq.Mock<IClienteRepository>();
            mock.Setup(e => e.Login(It.IsAny<string>(),It.IsAny<string>())).ReturnsAsync(new Cliente() { Id = Guid.NewGuid(), Nome = "jubileu", Role="Admin", Senha="Testando"});
            ClienteService clienteService = new ClienteService(mock.Object);
            Cliente cliente = await clienteService.Login("jubileu","Testando");
            cliente.Should().NotBeNull();
        }

        [Fact]
        public async void CreateTest()
        {
            Moq.Mock<IClienteRepository> mock = new Moq.Mock<IClienteRepository>();
            mock.Setup(e => e.CreateAsync(It.IsAny<ClienteDTO>())).ReturnsAsync(new Cliente());
            ClienteService clienteService = new ClienteService(mock.Object);
            Cliente cliente = await clienteService.Create(new ClienteDTO(){Nome = "testando", Senha = "testando", Role = Policies.ADMIN.ToString()});
            cliente.Should().BeOfType<Cliente>();
        }

        [Fact]
        public async void getAllPageTest()
        {
            ClienteRepository clienteRepository = new ClienteRepository(_context,_mapper);
            ClienteService clienteService = new ClienteService(clienteRepository);
            List<ClienteResponseDTO> result1 = await clienteService.getAllPage();
            result1.Should().HaveCount(10);

        }
        

        
    }
}