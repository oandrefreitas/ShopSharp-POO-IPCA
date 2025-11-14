using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class VendaBLL
    {
        private VendaDAL vendaDAL = new VendaDAL();

        /// <summary>
        /// Obtém uma lista de todas as vendas realizadas.
        /// </summary>
        /// <returns>Lista de vendas.</returns>
        public List<Venda> ObterVendas()
        {
            return vendaDAL.LerVendas();
        }

        /// <summary>
        /// Obtém uma lista de vendas realizadas por um cliente específico.
        /// </summary>
        /// <param name="clienteId">ID do cliente.</param>
        /// <returns>Lista de vendas do cliente.</returns>
        public List<Venda> ObterVendasPorIdCliente(int clienteId)
        {
            var todasVendas = vendaDAL.LerVendas();
            return todasVendas.Where(v => v.Cliente.ClienteID == clienteId).ToList();
        }

        /// <summary>
        /// Regista uma nova venda no sistema.
        /// </summary>
        /// <param name="venda">A venda a ser adicionada.</param>
        public void AdicionarVenda(Venda venda)
        {
            vendaDAL.AdicionarVenda(venda);
        }

        /// <summary>
        /// Calcula o valor total de uma venda, aplicando descontos de campanhas quando aplicável.
        /// </summary>
        /// <param name="venda">A venda cujo total será calculado.</param>
        /// <returns>O valor total da venda.</returns>
        public decimal CalcularTotalVenda(Venda venda)
        {
            decimal total = 0m;

            foreach (var item in venda.Produtos)
            {
                decimal precoProduto = item.Preco;

                // Aplica desconto se o produto estiver associado a uma campanha ativa
                if (venda.CampanhaAplicada != null &&
                    venda.CampanhaAplicada.EstaAtiva() &&
                    venda.CampanhaAplicada.ProdutosAssociados.Contains(item.ProdutoId))
                {
                    precoProduto -= precoProduto * (venda.CampanhaAplicada.Desconto / 100m);
                }

                total += precoProduto;
            }

            return total;
        }
    }
}