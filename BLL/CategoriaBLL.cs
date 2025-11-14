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
    /// Lógica de negócios para gerenciamento de categorias.
    /// </summary>
    public class CategoriaBLL
    {
        private CategoriaDAL categoriaDAL = new CategoriaDAL();

        /// <summary>
        /// Obtém uma lista de todas as categorias disponíveis.
        /// </summary>
        /// <returns>Uma lista de categorias.</returns>
        public List<Categoria> ObterCategorias()
        {
            return categoriaDAL.LerCategorias();
        }

        /// <summary>
        /// Obtém uma categoria específica pelo seu ID.
        /// </summary>
        /// <param name="categoriaId">O ID da categoria.</param>
        /// <returns>A categoria correspondente ao ID fornecido.</returns>
        public Categoria ObterCategoriaPorId(int categoriaId)
        {
            return categoriaDAL.ObterCategoriaPorId(categoriaId);
        }

        /// <summary>
        /// Apaga todas as categorias existentes.
        /// </summary>
        public void ApagarTodasCategorias()
        {
            var categorias = categoriaDAL.LerCategorias();
            categorias.Clear();
            categoriaDAL.GravarCategorias(categorias);
        }

        /// <summary>
        /// Adiciona uma nova categoria.
        /// </summary>
        /// <param name="nome">Nome da categoria.</param>
        /// <param name="tipo">Tipo da categoria (Hardware, Software, Gadgets).</param>
        /// <param name="detalhe">Detalhes específicos da categoria.</param>
        public void AdicionarCategoria(string nome, string tipo, string detalhe)
        {
            var listaCategorias = categoriaDAL.LerCategorias();

            Categoria novaCategoria;
            switch (tipo)
            {
                case "Hardware":
                    novaCategoria = new CategoriaHardware(nome, detalhe);
                    break;
                case "Software":
                    novaCategoria = new CategoriaSoftware(nome, detalhe);
                    break;
                case "Gadgets":
                    novaCategoria = new CategoriaGadgets(nome, detalhe);
                    break;
                default:
                    throw new ArgumentException("Tipo de categoria inválido");
            }

            listaCategorias.Add(novaCategoria);
            categoriaDAL.GravarCategorias(listaCategorias);
        }

        /// <summary>
        /// Edita os detalhes de uma categoria existente.
        /// </summary>
        /// <param name="categoria">A categoria com as alterações.</param>
        public void EditarCategoria(Categoria categoria)
        {
            categoriaDAL.EditarCategoria(categoria);
        }

        /// <summary>
        /// Remove uma categoria pelo seu ID.
        /// </summary>
        /// <param name="categoriaId">O ID da categoria a ser removida.</param>
        public void RemoverCategoria(int categoriaId)
        {
            categoriaDAL.RemoverCategoria(categoriaId);
        }
    }
}