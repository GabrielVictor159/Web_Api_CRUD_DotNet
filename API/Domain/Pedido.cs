using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using API.Domain.Enums;

namespace Web_Api_CRUD.Domain
{
    public class Pedido
    {
        public Pedido(Guid IdCliente, List<PedidoProduto> lista, String? nomeCupom = null)
        {
            this.idCliente = IdCliente;
            this.Lista = lista;
            decimal valorTotal = 0;
            foreach (PedidoProduto pedidoProduto in lista)
            {
                valorTotal += pedidoProduto.ValorTotalLinha;
            }
            if (nomeCupom != null)
            {
                Type tipoEnum = typeof(Cupons);
                if (Enum.IsDefined(tipoEnum, nomeCupom))
                {
                    Cupons cupom = (Cupons)Enum.Parse(tipoEnum, nomeCupom);
                    int valor = (int)cupom;
                    decimal valorDescontado = valorTotal * (valor / 100);
                    valorTotal = valorTotal - valorDescontado;
                    this.cupons = nomeCupom;
                }
            }
            this.ValorTotal = valorTotal;
        }
        public Pedido()
        { }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }
        [JsonIgnore]
        public Cliente cliente { get; private set; }
        [Required]
        public Guid idCliente { get; private set; }
        [Required]
        public decimal ValorTotal { get; private set; }
        public String cupons { get; private set; } = "";
        public List<PedidoProduto> Lista { get; set; } = new();

        public void AtualizarLista(List<PedidoProduto> list)
        {
            this.Lista = list;
            decimal valorTotal = 0;
            foreach (PedidoProduto pedidoProduto in list)
            {
                valorTotal += pedidoProduto.ValorTotalLinha;
            }
            this.ValorTotal = valorTotal;
        }
        public void AtualizarCupom(string nomeCupom)
        {
            Type tipoEnum = typeof(Cupons);
            if (Enum.IsDefined(tipoEnum, nomeCupom))
            {
                Cupons cupom = (Cupons)Enum.Parse(tipoEnum, nomeCupom);
                int valor = (int)cupom;
                decimal valorDescontado = ValorTotal * (valor / 100);
                this.ValorTotal = ValorTotal - valorDescontado;
                this.cupons = nomeCupom;
            }
        }
    }
}