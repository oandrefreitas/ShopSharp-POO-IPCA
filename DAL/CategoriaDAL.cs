using Model;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DAL
{
    public class CategoriaDAL
    {
        private const string categoriaFilePath = "categorias.json";

        /// <summary>
        /// Lê e retorna todas as categorias armazenadas no arquivo.
        /// </summary>
        /// <returns>Lista de categorias.</returns>
        public List<Categoria> LerCategorias()
        {
            if (File.Exists(categoriaFilePath))
            {
                var json = File.ReadAllText(categoriaFilePath);
                return JsonConvert.DeserializeObject<List<Categoria>>(json);
            }
            return new List<Categoria>();
        }

        /// <summary>
        /// Grava a lista de categorias no arquivo de armazenamento.
        /// </summary>
        /// <param name="categorias">Lista de categorias a serem gravadas.</param>
        public void GravarCategorias(List<Categoria> categorias)
        {
            var json = JsonConvert.SerializeObject(categorias, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(categoriaFilePath, json);
            Console.WriteLine("Categoria gravada com sucesso!");
        }

        /// <summary>
        /// Obtém uma categoria pelo seu ID.
        /// </summary>
        /// <param name="categoriaId">ID da categoria.</param>
        /// <returns>A categoria correspondente ao ID, ou null se não encontrada.</returns>
        public Categoria ObterCategoriaPorId(int categoriaId)
        {
            var categorias = LerCategorias();
            return categorias.FirstOrDefault(c => c.CategoriaID == categoriaId);
        }

        /// <summary>
        /// Atualiza uma categoria existente.
        /// </summary>
        /// <param name="categoriaEditada">Categoria com as informações atualizadas.</param>
        public void EditarCategoria(Categoria categoriaEditada)
        {
            var categorias = LerCategorias();
            var categoriaExistente = categorias.FirstOrDefault(c => c.CategoriaID == categoriaEditada.CategoriaID);

            if (categoriaExistente != null)
            {
                int index = categorias.FindIndex(c => c.CategoriaID == categoriaEditada.CategoriaID);
                if (index != -1)
                {
                    categorias[index] = categoriaEditada;
                    GravarCategorias(categorias);
                }
                else
                {
                    Console.WriteLine("Índice da categoria não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("Categoria não encontrada.");
            }
        }

        /// <summary>
        /// Remove uma categoria pelo seu ID.
        /// </summary>
        /// <param name="categoriaId">ID da categoria a ser removida.</param>
        public void RemoverCategoria(int categoriaId)
        {
            var categorias = LerCategorias();
            var categoriaParaRemover = categorias.FirstOrDefault(c => c.CategoriaID == categoriaId);

            if (categoriaParaRemover != null)
            {
                categorias.Remove(categoriaParaRemover);
                GravarCategorias(categorias);
            }
            else
            {
                Console.WriteLine("Categoria não encontrada.");
            }
        }
    }
}