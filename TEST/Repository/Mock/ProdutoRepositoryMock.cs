using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Web_Api_CRUD.Infraestructure;
using Web_Api_CRUD.Repository;

namespace TEST.Repository.Mock
{
    public class ProdutoRepositoryMock : ProdutoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProdutoRepositoryMock(ApplicationDbContextMock context, IMapper mapper) : base(context, mapper)
        {
             _context = context;
            _mapper = mapper;
        }
    }
}