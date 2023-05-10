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
using Xunit.Frameworks.Autofac;
using Web_Api_CRUD.Exceptions;

namespace TEST.Service
{
    [UseAutofacTestFramework]
    public class ClienteServiceTest
    {
        private readonly IClienteService _service;
        private readonly ApplicationDbContextMock _context;
         public ClienteServiceTest(IClienteService service, ApplicationDbContextMock context)
         {
          _service = service;
          _context = context;
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
            var faker = new Faker("pt_BR");
            ClienteDTO clienteDto = new ClienteDTO()
            {
               Nome= faker.Name.FullName(),
               Senha = faker.Random.String2(10),
               Role = Policies.ADMIN.ToString()
            };
            Func<Task> result = async ()=>{ await _service.Create(clienteDto);};
            result.Should().NotBeNull();
            clienteDto.Nome = faker.Random.String2(5);
            Func<Task> result2 = async ()=>{ await _service.Create(clienteDto);};
            await result2.Should().ThrowAsync<ClienteRegisterException>();
            clienteDto.Nome = faker.Random.String2(10);
            clienteDto.Senha = faker.Random.String2(5);
            Func<Task> result3 = async ()=>{ await _service.Create(clienteDto);};
            await result3.Should().ThrowAsync<ClienteRegisterException>();
             clienteDto.Senha = faker.Random.String2(10);
             clienteDto.Role = faker.Random.String2(10);
            Func<Task> result4 = async ()=>{ await _service.Create(clienteDto);};
            await result4.Should().ThrowAsync<ClienteRegisterException>();
        }
        
        [Fact]
        public async void Update()
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

             ClienteDTO newAtributes = new ClienteDTO()
             {
                Nome = faker.Name.FindName(),
                Senha = faker.Random.String2(10),
                Role = Policies.USER.ToString()
             };
             var Result1 = await _service.Update(cliente.Id,newAtributes);
             Result1.Nome.Should().BeSameAs(newAtributes.Nome);
             newAtributes.Nome = faker.Random.String2(5);
             Func<Task> result2 = async ()=>{ await _service.Update(cliente.Id,newAtributes);};
             await result2.Should().ThrowAsync<ClienteUpdateException>();
            newAtributes.Nome = faker.Random.String2(10);
            newAtributes.Senha = faker.Random.String2(6);
             Func<Task> result3 = async ()=>{ await _service.Update(cliente.Id,newAtributes);};
             await result3.Should().ThrowAsync<ClienteUpdateException>();
             newAtributes.Senha = faker.Random.String2(10);
             newAtributes.Role = faker.Random.String2(8);
             Func<Task> result4 = async ()=>{ await _service.Update(cliente.Id,newAtributes);};
             await result4.Should().ThrowAsync<ClienteUpdateException>();
        }

         [Fact]
        public async void UpdateByUser()
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

             ClienteDTO newAtributes = new ClienteDTO()
             {
                Nome = faker.Name.FindName(),
                Senha = faker.Random.String2(10),
                Role = Policies.ADMIN.ToString()
             };
             var Result1 = await _service.UpdateByUser(cliente.Id,newAtributes);
             Result1.Nome.Should().BeSameAs(newAtributes.Nome);
             newAtributes.Nome = faker.Random.String2(5);
             Func<Task> result2 = async ()=>{ await _service.UpdateByUser(cliente.Id,newAtributes);};
             await result2.Should().ThrowAsync<ClienteUpdateException>();
            newAtributes.Nome = faker.Random.String2(10);
            newAtributes.Senha = faker.Random.String2(6);
             Func<Task> result3 = async ()=>{ await _service.UpdateByUser(cliente.Id,newAtributes);};
             await result3.Should().ThrowAsync<ClienteUpdateException>();
             newAtributes.Senha = faker.Random.String2(10);
             newAtributes.Role = faker.Random.String2(8);
             Func<Task> result4 = async ()=>{ await _service.UpdateByUser(cliente.Id,newAtributes);};
             await result4.Should().ThrowAsync<ClienteUpdateException>();
        }
        
    }
}