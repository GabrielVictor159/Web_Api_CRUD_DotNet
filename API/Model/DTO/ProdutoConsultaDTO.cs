using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api_CRUD.Model.DTO
{
    public class ProdutoConsultaDTO
    {
        public int? index { get; set; } = 1;
        public int? size { get; set; } = 10;
        public string? nome { get; set; } = null;
        public decimal? valorMinimo { get; set; } = null;
        public decimal? valorMaximo { get; set; } = null;
        public string? id { get; set; } = null;
    }
}