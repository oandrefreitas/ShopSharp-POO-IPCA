using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Representa uma venda realizada na loja, incluindo informações sobre o cliente, os produtos comprados, a data da venda, a campanha aplicada, se houver, e o valor total da venda.
    /// </summary>
    public class Venda
    {
        private static int proximoId = 1;

        /// <summary>
        /// Construtor da classe Venda, que inicializa uma nova venda com os dados fornecidos.
        /// </summary>
        /// <param name="cliente">Cliente que realizou a compra.</param>
        /// <param name="produtos">Lista de produtos comprados.</param>
        /// <param name="dataVenda">Data em que a venda foi realizada.</param>
        /// <param name="campanhaAplicada">Campanha promocional aplicada à venda, se houver.</param>
        public Venda(Cliente cliente, List<Produto> produtos, DateTime dataVenda, Campanha campanhaAplicada = null)
        {
            VendaId = proximoId++;
            Cliente = cliente;
            Produtos = produtos ?? new List<Produto>();
            DataVenda = dataVenda;
            CampanhaAplicada = campanhaAplicada;
            // O valor total da venda será calculado e definido posteriormente.
        }

        // Propriedades implementadas para armazenar as informações da venda.
        public int VendaId { get; private set; }
        public Cliente Cliente { get; set; }
        public List<Produto> Produtos { get; private set; }
        public DateTime DataVenda { get; set; }
        public Campanha CampanhaAplicada { get; set; }
        public decimal Total { get; set; }

        // Método para atualizar o total da venda
        public void AtualizarTotal(decimal novoTotal)
        {
            Total = novoTotal;
        }
    }
}