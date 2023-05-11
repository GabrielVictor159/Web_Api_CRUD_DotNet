using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Web_Api_CRUD.Model.DTO;
using Web_Api_CRUD.Model.Enums;
using Web_Api_CRUD.Repository;
using Xunit;
using Xunit.Frameworks.Autofac;
using FluentAssertions;
using Web_Api_CRUD.Exceptions;
using Web_Api_CRUD.Model;
using Web_Api_CRUD.Infraestructure;

namespace TEST.Repository
{
    [UseAutofacTestFramework]
    public class ClienteRepositoryTest
    {
         private readonly IClienteRepository _service;
        private readonly ApplicationDbContext _context;
         public ClienteRepositoryTest(IClienteRepository service, ApplicationDbContext context)
         {
          _service = service;
          _context = context;
         }

         [Fact]
         public async void CreateAsyncTest()
         {
             var faker = new Faker("pt_BR");
            ClienteDTO clienteDto = new ClienteDTO()
            {
               Nome= faker.Name.FullName(),
               Senha = faker.Random.String2(10),
               Role = Policies.ADMIN.ToString()
            };
            var result1 = await _service.CreateAsync(clienteDto);
            result1.Should().NotBeNull();
             ClienteDTO clienteDto2 = new ClienteDTO()
            {
               Nome= clienteDto.Nome,
               Senha = faker.Random.String2(10),
               Role = Policies.ADMIN.ToString()
            };
            Func<Task> result2 = async ()=>{ await _service.CreateAsync(clienteDto2);};
            await result2.Should().ThrowAsync<ClienteRegisterException>();
         }

         [Fact]
         public async void LoginTest()
         {
            var faker = new Faker("pt_BR");
            String senha = faker.Random.String2(10);
             Cliente cliente = new Cliente()
             {
                Id = Guid.NewGuid(),
                Nome = faker.Name.FullName(),
                Senha = senha,
                Role = Policies.ADMIN.ToString()
             };
             _context.clientes.Add(cliente);
             _context.SaveChanges();

             var result = await _service.Login(cliente.Nome,senha);
             result.Should().NotBeNull();
         }

          [Fact]
        public async void getAllPageTest()
        {
            var faker = new Faker("pt_BR"); 
            List<Cliente> listCliente = new ();
            for(int i=0;i<20; i++)
            {
                listCliente.Add(new Cliente(){Id = Guid.NewGuid(), Nome = faker.Name.FullName(), Senha = faker.Random.String2(10), Role = Policies.ADMIN.ToString()});
            }
            _context.clientes.AddRange(listCliente);
            _context.SaveChanges();
            List<ClienteResponseDTO> result1 = await _service.GetAllPageAsync();
            result1.Should().HaveCountGreaterThan(9);
             List<ClienteResponseDTO> result2 = await _service.GetAllPageAsync(1,15);
             result2.Should().HaveCountGreaterThan(14);
             List<ClienteResponseDTO> result3 = await _service.GetAllPageAsync(1,10,listCliente[0].Nome);
             result3.Should().HaveCountGreaterThan(0); 
             List<ClienteResponseDTO> result4 = await _service.GetAllPageAsync(1,10,"",Policies.ADMIN.ToString());
             result4.Should().HaveCountGreaterThan(9);
              List<ClienteResponseDTO> result5 = await _service.GetAllPageAsync(1,10,"","",listCliente[0].Id.ToString());
              result5.Should().HaveCountGreaterThan(0); 
        }

         [Fact]
        public async void GetClienteByIdTest()
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
             ClienteResponseDTO result = await _service.GetClienteByIdAsync(cliente.Id);
             result.Should().NotBeNull();
             Func<Task> result2 = async ()=>{ await _service.GetClienteByIdAsync(Guid.NewGuid());};
            await result2.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async void UpdateTest()
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
               ClienteDTO clienteDto = new ClienteDTO()
            {
               Nome= faker.Name.FullName(),
               Senha = faker.Random.String2(10),
               Role = Policies.ADMIN.ToString()
            };
            var result1 = await _service.UpdateAsync(clientes[0].Id,clienteDto);
            result1.Should().NotBeNull();
            clienteDto.Nome = clientes[1].Nome;
             Func<Task> result2 = async ()=>{ await _service.UpdateAsync(clientes[0].Id,clienteDto);};
             await result2.Should().ThrowAsync<ClienteUpdateException>();
            clienteDto.Nome = faker.Name.FullName();
            Func<Task> result3 = async ()=>{ await _service.UpdateAsync(Guid.NewGuid(),clienteDto);};
            await result3.Should().ThrowAsync<ClienteUpdateException>();
        }

        [Fact]
        public async void DeleteTest()
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
            Func<Task> result1 = async ()=>{ await _service.DeleteAsync(cliente.Id);};
            await result1.Should().NotThrowAsync();
             Func<Task> result2 = async ()=>{ await _service.DeleteAsync(Guid.NewGuid());};
            await result2.Should().ThrowAsync<ClienteDeleteException>();
        }
    }
}