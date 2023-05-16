using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Web_Api_CRUD.Domain.DTO;
using Web_Api_CRUD.Domain.Enums;
using Web_Api_CRUD.Repository;
using Xunit;
using Xunit.Frameworks.Autofac;
using FluentAssertions;
using Web_Api_CRUD.Exceptions;
using Web_Api_CRUD.Domain;
using Web_Api_CRUD.Infraestructure;

namespace TEST.Repository
{
    [UseAutofacTestFramework]
    public class ClienteRepositoryTest
    {
        private readonly IClienteRepository _repository;
        private readonly ApplicationDbContext _context;
        private  Faker faker = new Faker("pt_BR");
        public ClienteRepositoryTest(IClienteRepository repository, ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        [Fact]
        public async void CreateAsyncTest()
        {
            Cliente cliente = new Cliente()
            {
                Nome = faker.Name.FullName(),
                Senha = faker.Random.String2(10),
                Role = Policies.ADMIN.ToString()
            };
            Cliente Result = await _repository.CreateAsync(cliente);
            Result.Id.Should().NotBeEmpty();     
        }

        [Fact]
        public async void GetAllByNameAsyncTest()
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
            var result = await _repository.GetAllByNameAsync(cliente.Nome);
            result.Should().NotBeNullOrEmpty(); 
            var result2 = await _repository.GetAllByNameAsync(faker.Name.FullName());
            result2.Should().BeNullOrEmpty(); 
        }
        [Fact]
        public async void LoginTest()
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
            var result = await _repository.Login(cliente.Nome, cliente.Senha);
            result.Should().NotBeNull();
            var result2 = await _repository.Login(cliente.Nome+"a", cliente.Senha);
            result2.Should().BeNull();
        }

        [Fact]
        public async void getAllPageAsyncTest()
        {
            List<Cliente> listCliente = new();
            for (int i = 0; i < 20; i++)
            {
                listCliente.Add(new Cliente() { Id = Guid.NewGuid(), Nome = faker.Name.FullName(), Senha = faker.Random.String2(10), Role = Policies.ADMIN.ToString() });
            }
            _context.clientes.AddRange(listCliente);
            await _context.SaveChangesAsync();
            List<ClienteResponseDTO> result1 = await _repository.GetAllPageAsync();
            result1.Should().HaveCountGreaterThan(9);
            List<ClienteResponseDTO> result2 = await _repository.GetAllPageAsync(1, 15);
            result2.Should().HaveCountGreaterThan(14);
            List<ClienteResponseDTO> result3 = await _repository.GetAllPageAsync(1, 10, listCliente[0].Nome);
            result3.Should().HaveCountGreaterThan(0);
            List<ClienteResponseDTO> result4 = await _repository.GetAllPageAsync(1, 10, "", Policies.ADMIN.ToString());
            result4.Should().HaveCountGreaterThan(9);
            List<ClienteResponseDTO> result5 = await _repository.GetAllPageAsync(1, 10, "", "", listCliente[0].Id.ToString());
            result5.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async void GetClienteByIdAsyncTest()
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
            var result = await _repository.GetClienteByIdAsync(cliente.Id);
            result.Should().NotBeNull();
            var result2 =  await _repository.GetClienteByIdAsync(Guid.NewGuid());
            result2.Should().BeNull();
        }

        [Fact]
        public async void UpdateAyncTest()
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
            ClienteDTO clienteDto = new ClienteDTO()
            {
                Nome = faker.Name.FullName(),
                Senha = faker.Random.String2(10),
                Role = Policies.ADMIN.ToString()
            };
            var result1 = await _repository.UpdateAsync(cliente.Id, clienteDto);
            result1.Should().NotBeNull();
            result1.Nome.Should().Be(clienteDto.Nome);   
            result1.Id.Should().Be(cliente.Id);
        }

        [Fact]
        public async void DeleteAsyncTest()
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
            var result1 =  await _repository.DeleteAsync(cliente.Id); ;
            result1.Should().Be(true);
            var result2 =  await _repository.DeleteAsync(Guid.NewGuid());
            result2.Should().Be(false);
        }

        [Fact]
        public async void GetPedidosTest()
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
            List<Pedido> listPedidos = new ();
            List<Pedido> pedidos = await _repository.GetPedidos(cliente.Id);
            pedidos.Should().BeNullOrEmpty();
        }
        
    }
}