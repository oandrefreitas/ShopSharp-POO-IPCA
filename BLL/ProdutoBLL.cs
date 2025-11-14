using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProdutoBLL
    {
        private ProdutoDAL produtoDAL = new ProdutoDAL();
        private MarcaBLL marcaBLL = new MarcaBLL();
        private MarcaDAL marcaDAL = new MarcaDAL();
        private CategoriaBLL categoriaBLL = new CategoriaBLL();
        private CategoriaDAL categoriaDAL = new CategoriaDAL();

        /// <summary>
        /// Obtém uma lista de todos os produtos disponíveis.
        /// </summary>
        /// <returns>Lista de produtos.</returns>
        public List<Produto> ObterProdutos()
        {
            return produtoDAL.LerProdutos();
        }

        /// <summary>
        /// Obtém um produto específico pelo seu ID.
        /// </summary>
        /// <param name="produtoId">ID do produto.</param>
        /// <returns>O produto correspondente ao ID.</returns>
        public Produto ObterProdutoPorId(int produtoId)
        {
            return produtoDAL.ObterProdutoPorId(produtoId);
        }

        /// <summary>
        /// Apaga todos os produtos registados.
        /// </summary>
        public void ApagarTodosProdutos()
        {
            var listaProdutos = produtoDAL.LerProdutos();

            if (listaProdutos.Count > 0)
            {
                listaProdutos.Clear();
                produtoDAL.GravarProdutos(listaProdutos);
                Console.WriteLine("Todos os produtos foram apagados com sucesso!");
            }
            else
            {
                Console.WriteLine("A lista de produtos já está vazia.");
            }
        }

        /// <summary>
        /// Adiciona um novo produto com os detalhes fornecidos.
        /// </summary>
        /// <param name="nome">Nome do produto.</param>
        /// <param name="preco">Preço do produto.</param>
        /// <param name="stock">Quantidade em stock.</param>
        /// <param name="nomeMarca">Nome da marca do produto.</param>
        /// <param name="garantiaMeses">Meses de garantia do produto.</param>
        /// <param name="nomeCategoria">Nome da categoria do produto.</param>
        /// <param name="tipoCategoria">Tipo da categoria do produto.</param>
        /// <param name="detalheCategoria">Detalhes adicionais da categoria.</param>
        public void AdicionarProduto(string nome, decimal preco, int stock, string nomeMarca, int garantiaMeses, string nomeCategoria, string tipoCategoria, string detalheCategoria)
        {
           // Obter a lista de produtos, marcas e categorias
        var listaProdutos = produtoDAL.LerProdutos();
            var listaMarcas = marcaBLL.ObterMarcas();
            var listaCategorias = categoriaBLL.ObterCategorias();

            // Obter o ID mais alto entre os produtos existentes
            int novoProdutoID = listaProdutos.Count > 0 ? listaProdutos.Max(p => p.ProdutoId) + 1 : 1;

            // Procurar a marca existente ou criar uma nova
            Marca marcaExistente = listaMarcas.FirstOrDefault(m => m.Nome == nomeMarca);
            if (marcaExistente == null)
            {
                marcaBLL.AdicionarMarca(nomeMarca); // Cria uma nova marca
                listaMarcas = marcaBLL.ObterMarcas(); // Atualiza a lista de marcas
                marcaExistente = listaMarcas.FirstOrDefault(m => m.Nome == nomeMarca);
            }

            // Procurar ou criar a categoria correspondente
            Categoria categoriaExistente = listaCategorias.FirstOrDefault(c => c.Nome == nomeCategoria);
            if (categoriaExistente == null)
            {
                switch (tipoCategoria)
                {
                    case "Hardware":
                        categoriaExistente = new CategoriaHardware(nomeCategoria, detalheCategoria);
                        break;
                    case "Software":
                        categoriaExistente = new CategoriaSoftware(nomeCategoria, detalheCategoria);
                        break;
                    case "Gadgets":
                        categoriaExistente = new CategoriaGadgets(nomeCategoria, detalheCategoria);
                        break;
                    default:
                        throw new ArgumentException("Tipo de categoria inválido");
                }
                listaCategorias.Add(categoriaExistente);
                categoriaDAL.GravarCategorias(listaCategorias);
            }

            // Cria e adiciona o novo produto com o ID incrementado
            Produto novoProduto = new Produto(nome, preco, stock, marcaExistente, garantiaMeses, categoriaExistente)
            {
                ProdutoId = novoProdutoID
            };
            listaProdutos.Add(novoProduto);

            // Atualizar as listas no arquivo
            produtoDAL.GravarProdutos(listaProdutos);
        }

        /// <summary>
        /// Edita as informações de um produto existente.
        /// </summary>
        /// <param name="produto">Produto com as informações atualizadas.</param>
        public void EditarProduto(Produto produto)
        {
            produtoDAL.EditarProduto(produto);
        }

        /// <summary>
        /// Remove um produto pelo seu ID.
        /// </summary>
        /// <param name="produtoId">ID do produto a ser removido.</param>
        public void RemoverProduto(int produtoId)
        {
            produtoDAL.RemoverProduto(produtoId);
        }
    }
}