using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Web_Api_CRUD.Services;
using Web_Api_CRUD.Repository;
using Moq;
using Web_Api_CRUD.Domain;
using FluentAssertions;
using Web_Api_CRUD.Domain.DTO;
using Web_Api_CRUD.Domain.Enums;
using Bogus;
using Web_Api_CRUD.Infraestructure;
using Microsoft.EntityFrameworkCore.InMemory;
using AutoMapper;
using Xunit.Frameworks.Autofac;
using Web_Api_CRUD.Exceptions;
using Web_Api_CRUD.Domain.Cryptography;

namespace TEST.Service
{
    [UseAutofacTestFramework]
    public class ClienteServiceTest
    {
        private readonly IClienteService _service;
        private readonly ApplicationDbContext _context;
        private Faker faker = new Faker("pt_BR");
        public ClienteServiceTest(IClienteService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }

        [Fact]
        public async void LoginTest()
        {
            String senha = faker.Random.String2(10);
            Cliente cliente = new Cliente()
            {
                Id = Guid.NewGuid(),
                Nome = faker.Name.FullName(),
                Senha = senha,
                Role = Policies.ADMIN.ToString()
            };
            _context.clientes.Add(cliente);
            await _context.SaveChangesAsync();
            var token = await _service.Login(cliente.Nome, senha);
            token.Should().NotBeNull();
            var token2 = await _service.Login(cliente.Nome + "a", senha);
            token2.Should().BeNull();
        }

        [Fact]
        public async void CreateTest()
        {
            ClienteDTO clienteDto = new ClienteDTO()
            {
                Nome = faker.Name.FullName(),
                Senha = faker.Random.String2(10),
                Role = Policies.ADMIN.ToString()
            };
            var result = await _service.Create(clienteDto);
            result.Should().BeOfType<Cliente>();
            var result2 = await _service.Create(clienteDto);
            result2.Should().BeOfType<string>();
        }
        
         [Fact]
        public async void getAllPageTest()
        {
            List<Cliente> listCliente = new();
            for (int i = 0; i < 20; i++)
            {
                listCliente.Add(new Cliente() { Id = Guid.NewGuid(), Nome = faker.Name.FullName(), Senha = faker.Random.String2(10), Role = Policies.ADMIN.ToString() });
            }
           
            _context.clientes.AddRange(listCliente);
            await _context.SaveChangesAsync();
            List<ClienteResponseDTO> result = await _service.getAllPage();
            result.Should().HaveCountGreaterThan(9);
        }

        [Fact]
        public async void getByIdTest()
        {
            Cliente cliente = new Cliente()
            {
                Id = Guid.NewGuid(), Nome = faker.Name.FullName(), Senha = faker.Random.String2(10), Role = Policies.ADMIN.ToString() 
            
            };
            _context.clientes.Add(cliente);
            await _context.SaveChangesAsync();
            var result = await _service.getById(cliente.Id);
            result.Should().BeOfType<ClienteResponseDTO>();
            var result2 = await _service.getById(Guid.NewGuid());
            result2.Should().BeOfType<string>();
        }

        [Fact]
        public async void UpdateTest()
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

            ClienteDTO newAtributes = new ClienteDTO()
            {
                Nome = faker.Name.FindName(),
                Senha = faker.Random.String2(10),
                Role = Policies.USER.ToString()
            };
            Object Result1 = await _service.Update(cliente.Id, newAtributes);
            Result1.Should().BeOfType<ClienteResponseDTO>();
            if(Result1 is ClienteResponseDTO dto)
            {
                dto.Nome.Should().Be(newAtributes.Nome);
            }
            Object Result2 = await _service.Update(Guid.NewGuid(), newAtributes);
            Result2.Should().BeOfType<string>();
        }

         [Fact]
        public async void UpdateByUserTest()
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

            ClienteDTO newAtributes = new ClienteDTO()
            {
                Nome = faker.Name.FindName(),
                Senha = faker.Random.String2(10),
                Role = Policies.USER.ToString()
            };
            Object Result1 = await _service.UpdateByUser(cliente.Id, newAtributes);
            Result1.Should().BeOfType<string>();
            newAtributes.Role = Policies.ADMIN.ToString();
            Object Result2 = await _service.UpdateByUser(cliente.Id, newAtributes);
            Result2.Should().BeOfType<ClienteResponseDTO>();
            if(Result2 is ClienteResponseDTO dto)
            {
                dto.Nome.Should().Be(newAtributes.Nome);
            }
        }

        [Fact]
        public async void DeleteTest()
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
            Boolean Result1 = await _service.Delete(cliente.Id);
            Result1.Should().Be(true);
            Boolean Result2 = await _service.Delete(Guid.NewGuid());
            Result2.Should().Be(false);
        }
      

    }
}