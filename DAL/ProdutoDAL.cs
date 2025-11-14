using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;


namespace DAL
{
    public class ProdutoDAL
    {
        private const string produtoFilePath = "produtos.json";

        /// <summary>
        /// Lê e retorna todos os produtos armazenados no arquivo.
        /// </summary>
        /// <returns>Lista de produtos.</returns>
        public List<Produto> LerProdutos()
        {
            if (File.Exists(produtoFilePath))
            {
                var json = File.ReadAllText(produtoFilePath);
                return JsonConvert.DeserializeObject<List<Produto>>(json);
            }
            return new List<Produto>();
        }

        /// <summary>
        /// Grava a lista atualizada de produtos no arquivo.
        /// </summary>
        /// <param name="produtos">Lista de produtos a serem gravados.</param>
        public void GravarProdutos(List<Produto> produtos)
        {
            var json = JsonConvert.SerializeObject(produtos, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(produtoFilePath, json);
            Console.WriteLine("Produto gravado com sucesso!");
        }

        /// <summary>
        /// Obtém um produto pelo seu ID.
        /// </summary>
        /// <param name="produtoId">ID do produto.</param>
        /// <returns>O produto correspondente ao ID, ou null se não encontrado.</returns>
        public Produto ObterProdutoPorId(int produtoId)
        {
            var produtos = LerProdutos();
            return produtos.FirstOrDefault(p => p.ProdutoId == produtoId);
        }

        /// <summary>
        /// Atualiza as informações de um produto existente.
        /// </summary>
        /// <param name="produtoEditado">Produto com as informações atualizadas.</param>
        public void EditarProduto(Produto produtoEditado)
        {
            var produtos = LerProdutos();
            var produtoExistente = ObterProdutoPorId(produtoEditado.ProdutoId);
            if (produtoExistente != null)
            {
                int index = produtos.FindIndex(p => p.ProdutoId == produtoEditado.ProdutoId);
                if (index != -1)
                {
                    produtos[index] = produtoEditado;
                    GravarProdutos(produtos);
                }
                else
                {
                    Console.WriteLine("Índice do produto não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("Produto não encontrado.");
            }
        }

        /// <summary>
        /// Remove um produto pelo seu ID.
        /// </summary>
        /// <param name="produtoId">ID do produto a ser removido.</param>
        public void RemoverProduto(int produtoId)
        {
            var produtos = LerProdutos();
            var produtoParaRemover = ObterProdutoPorId(produtoId);
            if (produtoParaRemover != null)
            {
                produtos.Remove(produtoParaRemover);
                GravarProdutos(produtos);
            }
            else
            {
                Console.WriteLine("Produto não encontrado.");
            }
        }
    }
}