using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MarcaDAL
    {
        private const string marcaFilePath = "marcas.json";

        /// <summary>
        /// Lê e retorna todas as marcas armazenadas no arquivo.
        /// </summary>
        /// <returns>Lista de marcas.</returns>
        public List<Marca> LerMarcas()
        {
            if (File.Exists(marcaFilePath))
            {
                var json = File.ReadAllText(marcaFilePath);
                return JsonConvert.DeserializeObject<List<Marca>>(json);
            }
            return new List<Marca>();
        }

        /// <summary>
        /// Grava a lista atualizada de marcas no arquivo.
        /// </summary>
        /// <param name="marcas">Lista de marcas a serem gravadas.</param>
        public void GravarMarcas(List<Marca> marcas)
        {
            var json = JsonConvert.SerializeObject(marcas, Formatting.Indented);
            File.WriteAllText(marcaFilePath, json);
            Console.WriteLine("Marca Registada com sucesso!");
        }

        /// <summary>
        /// Obtém uma marca pelo seu ID.
        /// </summary>
        /// <param name="marcaId">ID da marca.</param>
        /// <returns>A marca correspondente ao ID, ou null se não encontrada.</returns>
        public Marca ObterMarcaPorId(int marcaId)
        {
            var marcas = LerMarcas();
            return marcas.FirstOrDefault(m => m.MarcaID == marcaId);
        }

        /// <summary>
        /// Atualiza as informações de uma marca existente.
        /// </summary>
        /// <param name="marcaEditada">Marca com as informações atualizadas.</param>
        public void EditarMarca(Marca marcaEditada)
        {
            var marcas = LerMarcas();
            var marcaExistente = ObterMarcaPorId(marcaEditada.MarcaID);
            if (marcaExistente != null)
            {
                int index = marcas.FindIndex(m => m.MarcaID == marcaEditada.MarcaID);
                if (index != -1)
                {
                    marcas[index] = marcaEditada;
                    GravarMarcas(marcas);
                }
                else
                {
                    Console.WriteLine("Índice da marca não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("Marca não encontrada.");
            }
        }

        /// <summary>
        /// Remove uma marca pelo seu ID.
        /// </summary>
        /// <param name="marcaId">ID da marca a ser removida.</param>
        public void RemoverMarca(int marcaId)
        {
            var marcas = LerMarcas();
            var marcaParaRemover = ObterMarcaPorId(marcaId);
            if (marcaParaRemover != null)
            {
                marcas.Remove(marcaParaRemover);
                GravarMarcas(marcas);
            }
            else
            {
                Console.WriteLine("Marca não encontrada.");
            }
        }
    }
}