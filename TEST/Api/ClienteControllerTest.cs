using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_Api_CRUD.Controllers;
using Web_Api_CRUD.Domain;
using Web_Api_CRUD.Domain.DTO;
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
using Web_Api_CRUD.Domain.Enums;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace TEST.Api
{
    [UseAutofacTestFramework]
    public class ClienteControllerTest
    {
        private readonly ApplicationDbContext _context;
        private readonly ClienteController clienteController;
        private Faker faker = new Faker("pt_BR");
        public ClienteControllerTest(ClienteController controller, ApplicationDbContext applicationDbContext)
        {
            clienteController = controller;
            _context = applicationDbContext;
        }

        [Fact]
        public async void LoginTest()
        {
            string senha = faker.Random.String2(10);
            Cliente cliente = new Cliente() { Id = Guid.NewGuid(), Nome = faker.Name.FullName(), Role = Policies.ADMIN.ToString(), Senha = senha };
            _context.clientes.Add(cliente);
            _context.SaveChanges();
            var result = await clienteController.Login(new LoginDTO() { Nome = cliente.Nome, Senha = senha });
            result.Result.Should().BeOfType<OkObjectResult>();
            var result2 = await clienteController.Login(new LoginDTO() { Nome = cliente.Nome, Senha = senha + "5" });
            result2.Result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async void AddClienteTest()
        {
            LoginDTO loginDTO = new LoginDTO()
            {
                Nome = faker.Name.FullName(),
                Senha = faker.Random.String2(10)
            };
            var result = await clienteController.AddCliente(loginDTO);
            result.Result.Should().BeOfType<OkObjectResult>();
            var result2 = await clienteController.AddCliente(loginDTO);
            result2.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void AddAdminTest()
        {
            LoginDTO loginDTO = new LoginDTO()
            {
                Nome = faker.Name.FullName(),
                Senha = faker.Random.String2(10)
            };
            var result = await clienteController.AddAdmin(loginDTO);
            result.Result.Should().BeOfType<OkObjectResult>();
            var result2 = await clienteController.AddCliente(loginDTO);
            result2.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void GetAllPageTest()
        {
            List<Cliente> listCliente = new();
            for (int i = 0; i < 20; i++)
            {
                listCliente.Add(new Cliente() { Id = Guid.NewGuid(), Nome = faker.Name.FullName(), Senha = faker.Random.String2(10), Role = Policies.ADMIN.ToString() });
            };
            _context.clientes.AddRange(listCliente);
            _context.SaveChanges();
            ClientePaginationDTO itensRequest = new ClientePaginationDTO();
            var result = await clienteController.getAllPage(itensRequest);
            result.Result.Should().BeOfType<OkObjectResult>();
            itensRequest.Index = -1;
            var result2 = await clienteController.getAllPage(itensRequest);
            result2.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void GetOneTest()
        {
            Cliente cliente = new Cliente()
            {
                Id = Guid.NewGuid(),
                Nome = faker.Name.FullName(),
                Senha = faker.Random.String2(10),
                Role = Policies.ADMIN.ToString()
            };
            _context.clientes.Add(cliente);
            _context.SaveChanges();
            var result = await clienteController.getOne(cliente.Id);
            result.Result.Should().BeOfType<OkObjectResult>();
            var result2 = await clienteController.getOne(Guid.NewGuid());
            result2.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void UpdateByAdminTeste()
        {
            List<Cliente> clientes = new();
            for (int i = 0; i < 2; i++)
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
            var result = await clienteController.updateByAdmin(dto);
            result.Result.Should().BeOfType<OkObjectResult>();
            dto.Nome = faker.Random.String2(5);
            var result2 = await clienteController.updateByAdmin(dto);
            result2.Result.Should().BeOfType<BadRequestObjectResult>();
        }

         [Fact]
        public async void UpdateByUserTeste()
        {
            Cliente cliente = new Cliente()
            {
                      Id = Guid.NewGuid(),
                      Nome = faker.Name.FullName(),
                      Senha = faker.Random.String2(10),
                      Role = Policies.ADMIN.ToString()
            };
            _context.clientes.Add(cliente);
            await _context.SaveChangesAsync();
            ClienteDTO dto = new ClienteDTO()
            {
                Nome = faker.Name.FullName(),
                Senha = faker.Random.String2(10),
                Role = Policies.ADMIN.ToString()
            };
            SetHttpContextWithClaims(cliente.Id.ToString());
            var result = await clienteController.updateByUser(dto);
            result.Result.Should().BeOfType<OkObjectResult>();
            dto.Nome = faker.Random.String2(5);
            var result2 = await clienteController.updateByUser(dto);
            result2.Result.Should().BeOfType<BadRequestObjectResult>();
            dto.Nome = faker.Random.String2(10);
            dto.Role = Policies.USER.ToString();
            var result3 = await clienteController.updateByUser(dto);
            result3.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void DeleteByUserTeste()
        {
            Cliente cliente = new Cliente()
            {
                Id = Guid.NewGuid(),
                Nome = faker.Name.FullName(),
                Senha = faker.Random.String2(10),
                Role = Policies.ADMIN.ToString()
            };
            _context.clientes.Add(cliente);
            await _context.SaveChangesAsync();
            SetHttpContextWithClaims(cliente.Id.ToString());
            var result = await clienteController.deleteByUser();
            result.Result.Should().BeOfType<OkObjectResult>();
            SetHttpContextWithClaims(Guid.NewGuid().ToString());
            var result2 = await clienteController.deleteByUser();
            result2.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void DeleteByUserAdminTeste()
        {
            Cliente cliente = new Cliente()
            {
                Id = Guid.NewGuid(),
                Nome = faker.Name.FullName(),
                Senha = faker.Random.String2(10),
                Role = Policies.ADMIN.ToString()
            };
            _context.clientes.Add(cliente);
            _context.SaveChanges();
            var result = await clienteController.deleteByUserAdmin(cliente.Id);
            result.Result.Should().BeOfType<OkObjectResult>();
            var result2 = await clienteController.deleteByUserAdmin(Guid.NewGuid());
            result2.Result.Should().BeOfType<BadRequestObjectResult>();
        }
        

         private void SetHttpContextWithClaims(string id, Boolean admin = true)
        {
            var claims = new[]
            {
            new Claim("Id", id),
            new Claim(ClaimTypes.Role, admin?Policies.ADMIN.ToString():Policies.USER.ToString())
        };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext
            {
                User = principal
            };

            clienteController.ControllerContext.HttpContext = httpContext;
        }
    }
}
