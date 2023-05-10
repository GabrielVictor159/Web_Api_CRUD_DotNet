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
using Web_Api_CRUD.Infraestructure;
using Xunit.Frameworks.Autofac;
using Bogus;
using Web_Api_CRUD.Model.Enums;

namespace TEST.Api
{
    [UseAutofacTestFramework]
    public class ClienteControllerTest
    {
        private readonly IMapper _mapper;
        private readonly IClienteService _IClienteService;
        private readonly ApplicationDbContext _context;
        public ClienteControllerTest(IClienteService iClienteService,ApplicationDbContextMock applicationDbContextMock,IMapper mapper)
        {
        _mapper = mapper;
        _IClienteService = iClienteService;
        _context = applicationDbContextMock;
        }

        [Fact]
        public async void LoginTest()
        {
            var faker = new Faker("pt_BR");
            string usuario = faker.Name.FullName();
            string senha = faker.Random.String2(10);
            Cliente cliente = new Cliente() { Id = Guid.NewGuid(), Nome = usuario, Role = Policies.ADMIN.ToString(), Senha = senha };
            _context.clientes.Add(cliente);
            _context.SaveChanges();
            ClienteController clienteController = new ClienteController(_IClienteService,_mapper);
            var result =  await clienteController.Login(new LoginDTO() { Nome=usuario, Senha=senha});
            result.Result.Should().BeOfType<OkObjectResult>();
            var result2 = await clienteController.Login(new LoginDTO() { Nome=usuario, Senha=senha+"5"});
            result2.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void AddClienteTest()
        {
            var faker = new Faker("pt_BR");
            LoginDTO loginDTO = new LoginDTO()
            {
                Nome = faker.Name.FullName(),
                Senha = faker.Random.String2(10)
            };
            ClienteController clienteController = new ClienteController(_IClienteService,_mapper);
            var result =  await clienteController.AddCliente(loginDTO);
            result.Result.Should().BeOfType<OkObjectResult>();
            var result2 =  await clienteController.AddCliente(loginDTO);
            result2.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void AddAdminTest()
        {
            var faker = new Faker("pt_BR");
            LoginDTO loginDTO = new LoginDTO()
            {
                Nome = faker.Name.FullName(),
                Senha = faker.Random.String2(10)
            };
            ClienteController clienteController = new ClienteController(_IClienteService,_mapper);
            var result =  await clienteController.AddAdmin(loginDTO);
            result.Result.Should().BeOfType<OkObjectResult>();
            var result2 =  await clienteController.AddCliente(loginDTO);
            result2.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void GetAllPageTest()
        {
            var faker = new Faker("pt_BR"); 
            List<Cliente> listCliente = new ();
            for(int i=0;i<20; i++)
            {
                listCliente.Add(new Cliente(){Id = Guid.NewGuid(), Nome = faker.Name.FullName(), Senha = faker.Random.String2(10), Role = Policies.ADMIN.ToString()});
            }
            _context.clientes.AddRange(listCliente);
            _context.SaveChanges();
            ClientePaginationDTO itensRequest = new ClientePaginationDTO();
            ClienteController clienteController = new ClienteController(_IClienteService,_mapper);
            var result =  await clienteController.getAllPage(itensRequest);
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void GetOneTest()
        {
            var faker = new Faker("pt_BR"); 
            Cliente cliente = new Cliente()
             {
                Id = Guid.NewGuid(),
                Nome = faker.Name.FullName(),
                Senha = faker.Random.String2(10),
                Role = Policies.ADMIN.ToString()
             };
             _context.clientes.Add(cliente);
             _context.SaveChanges();
            ClienteController clienteController = new ClienteController(_IClienteService,_mapper);
            var result =  await clienteController.getOne(cliente.Id);
             result.Result.Should().BeOfType<OkObjectResult>();
             var result2 =  await clienteController.getOne(Guid.NewGuid());
            result2.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void UpdateByAdminTeste()
        {
           var faker = new Faker("pt_BR"); 
            List<Cliente> clientes = new ();
             for(int i=0; i<2; i++)
             {
              clientes.Add(  
                new Cliente()
                {
                Id = Guid.NewGuid(),
                Nome = faker.Name.FullName(),
                Senha = faker.Random.String2(10),
                Role = Policies.ADMIN.ToString()
                }
              );
             }
             _context.clientes.AddRange(clientes);
             _context.SaveChanges();
              ClienteUpdateDTO dto = new ClienteUpdateDTO()
              {
                Id = clientes[0].Id,
                Nome = faker.Name.FullName(),
                Senha = faker.Random.String2(10),
                Role = Policies.USER.ToString()
              };
              ClienteController clienteController = new ClienteController(_IClienteService,_mapper);
               var result =  await clienteController.updateByAdmin(dto);
                result.Result.Should().BeOfType<OkObjectResult>();
                dto.Nome = faker.Random.String2(5);
                 var result2 =  await clienteController.updateByAdmin(dto);
                result2.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void DeleteByUserTeste()
        {
            var faker = new Faker("pt_BR"); 
            Cliente cliente = new Cliente()
             {
                Id = Guid.NewGuid(),
                Nome = faker.Name.FullName(),
                Senha = faker.Random.String2(10),
                Role = Policies.ADMIN.ToString()
             };
             _context.clientes.Add(cliente);
             _context.SaveChanges();
             ClienteController clienteController = new ClienteController(_IClienteService,_mapper);
              var result =  await clienteController.deleteByUser(cliente.Id);
                result.Result.Should().BeOfType<OkObjectResult>();
                 var result2 =  await clienteController.deleteByUser(Guid.NewGuid());
                result2.Result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
