using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Representa um carrinho de compras de um cliente.
    /// </summary>
    public class CarrinhoDeCompras
    {
        /// <summary>
        /// Identificador único do cliente associado ao carrinho.
        /// </summary>
        public int ClienteId { get; set; }

        /// <summary>
        /// Lista de itens que estão no carrinho de compras.
        /// </summary>
        public List<ItemCarrinho> Itens { get; set; } = new List<ItemCarrinho>();
    }

    /// <summary>
    /// Representa um item individual no carrinho de compras.
    /// </summary>
    public class ItemCarrinho
    {
        /// <summary>
        /// Identificador único do produto adicionado ao carrinho.
        /// </summary>
        public int ProdutoId { get; set; }

        /// <summary>
        /// Quantidade do produto selecionado no carrinho.
        /// </summary>
        public int Quantidade { get; set; }
    }

}