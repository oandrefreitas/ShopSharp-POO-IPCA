using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Representa um produto, contendo informações como nome, preço, quantidade em stock, marca, garantia e categoria.
    /// Esta classe é utilizada para gerir os dados dos produtos dentro da aplicação.
    /// </summary>
    public class Produto
    {
        private static int proximoId = 1;

        /// <summary>
        /// Construtor para criar um novo produto. Incrementa automaticamente o ID do produto.
        /// </summary>
        /// <param name="nome">Nome do produto.</param>
        /// <param name="preco">Preço do produto.</param>
        /// <param name="stock">Quantidade disponível em stock.</param>
        /// <param name="marca">Marca associada ao produto.</param>
        /// <param name="garantiaMeses">Garantia do produto em meses.</param>
        /// <param name="categoria">Categoria à qual o produto pertence.</param>
        public Produto(string nome, decimal preco, int stock, Marca marca, int garantiaMeses, Categoria categoria)
        {
            ProdutoId = proximoId++;
            Nome = nome;
            Preco = preco;
            Stock = stock;
            Marca = marca;
            GarantiaMeses = garantiaMeses;
            Categoria = categoria;
        }

        // Propriedades implementadas para o ID e nome da marca.
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Stock { get; set; }
        public Marca Marca { get; set; }
        public int GarantiaMeses { get; set; }
        public Categoria Categoria { get; set; }
    }
}