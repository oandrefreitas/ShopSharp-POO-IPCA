using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// Lógica de negócios para gerenciamento do carrinho de compras.
    /// </summary>
    public class CarrinhoBLL
    {
        private ProdutoBLL produtoBLL = new ProdutoBLL();
        private ClienteBLL clienteBLL = new ClienteBLL();
        private VendaBLL vendaBLL = new VendaBLL();

        /// <summary>
        /// Adiciona um produto ao carrinho de compras.
        /// </summary>
        /// <param name="carrinho">O carrinho de compras.</param>
        /// <param name="produtoId">ID do produto.</param>
        /// <param name="quantidade">Quantidade do produto.</param>
        public void AdicionarAoCarrinho(CarrinhoDeCompras carrinho, int produtoId, int quantidade)
        {
            var produto = produtoBLL.ObterProdutoPorId(produtoId);

            // Verifica se o produto existe e se há stock suficiente
            if (produto != null && produto.Stock >= quantidade)
            {
                // Verifica se o item já existe no carrinho
                var itemExistente = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == produtoId);
                if (itemExistente != null)
                {
                    itemExistente.Quantidade += quantidade;
                }
                else
                {
                    carrinho.Itens.Add(new ItemCarrinho { ProdutoId = produtoId, Quantidade = quantidade });
                }
            }
            else
            {
                Console.WriteLine("Produto não disponível ou stock insuficiente.");
            }
        }

        /// <summary>
        /// Finaliza a compra, convertendo o carrinho em uma venda.
        /// </summary>
        /// <param name="carrinho">O carrinho de compras.</param>
        /// <param name="clienteId">ID do cliente.</param>
        /// <returns>A venda finalizada.</returns>
        public Venda FinalizarCompra(CarrinhoDeCompras carrinho, int clienteId)
        {
            // Obtém os produtos do carrinho e cria uma nova venda
            List<Produto> produtosComprados = carrinho.Itens.Select(i => produtoBLL.ObterProdutoPorId(i.ProdutoId)).ToList();
            Venda novaVenda = new Venda(
                clienteBLL.ObterClientePorId(clienteId),
                produtosComprados,
                DateTime.Now
            );

            // Calcula o total da venda e atualiza o stock dos produtos
            decimal totalVenda = vendaBLL.CalcularTotalVenda(novaVenda);
            novaVenda.AtualizarTotal(totalVenda);

            foreach (var item in carrinho.Itens)
            {
                var produto = produtoBLL.ObterProdutoPorId(item.ProdutoId);
                produto.Stock -= item.Quantidade;
                produtoBLL.EditarProduto(produto);
            }

            vendaBLL.AdicionarVenda(novaVenda);

            LimparCarrinho(carrinho);

            return novaVenda;
        }

        /// <summary>
        /// Remove um produto do carrinho de compras.
        /// </summary>
        /// <param name="carrinho">O carrinho de compras.</param>
        /// <param name="produtoId">ID do produto a remover.</param>
        public void RemoverProdutoCarrinho(CarrinhoDeCompras carrinho, int produtoId)
        {
            var itemParaRemover = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == produtoId);
            if (itemParaRemover != null)
            {
                carrinho.Itens.Remove(itemParaRemover);
            }
            else
            {
                Console.WriteLine("Produto não encontrado no carrinho.");
            }
        }

        /// <summary>
        /// Limpa todos os itens do carrinho de compras.
        /// </summary>
        /// <param name="carrinho">O carrinho de compras a ser limpo.</param>
        public void LimparCarrinho(CarrinhoDeCompras carrinho)
        {
            carrinho.Itens.Clear();
        }
    }
}