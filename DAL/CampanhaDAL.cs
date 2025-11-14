using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CampanhaDAL
    {
        private const string campanhaFilePath = "campanhas.json";

        /// <summary>
        /// Lê e retorna todas as campanhas armazenadas no arquivo.
        /// </summary>
        /// <returns>Lista de campanhas.</returns>
        public List<Campanha> LerCampanhas()
        {
            if (File.Exists(campanhaFilePath))
            {
                var json = File.ReadAllText(campanhaFilePath);
                var campanhas = JsonConvert.DeserializeObject<List<Campanha>>(json);
                return campanhas ?? new List<Campanha>();
            }
            return new List<Campanha>();
        }

        /// <summary>
        /// Grava a lista de campanhas no arquivo de armazenamento.
        /// </summary>
        /// <param name="campanhas">Lista de campanhas a serem gravadas.</param>
        public void GravarCampanhas(List<Campanha> campanhas)
        {
            var json = JsonConvert.SerializeObject(campanhas, Formatting.Indented);
            File.WriteAllText(campanhaFilePath, json);
        }

        /// <summary>
        /// Obtém uma campanha pelo seu ID.
        /// </summary>
        /// <param name="campanhaId">ID da campanha.</param>
        /// <returns>A campanha correspondente ao ID, ou null se não encontrada.</returns>
        public Campanha ObterCampanhaPorId(int campanhaId)
        {
            var campanhas = LerCampanhas();
            return campanhas.FirstOrDefault(c => c.CampanhaId == campanhaId);
        }

        /// <summary>
        /// Atualiza uma campanha existente.
        /// </summary>
        /// <param name="campanhaEditada">Campanha com as informações atualizadas.</param>
        public void EditarCampanha(Campanha campanhaEditada)
        {
            var campanhas = LerCampanhas();
            var campanhaExistente = campanhas.FirstOrDefault(c => c.CampanhaId == campanhaEditada.CampanhaId);

            if (campanhaExistente != null)
            {
                int index = campanhas.FindIndex(c => c.CampanhaId == campanhaEditada.CampanhaId);
                if (index != -1)
                {
                    campanhas[index] = campanhaEditada;
                    GravarCampanhas(campanhas);
                }
                else
                {
                    Console.WriteLine("Índice da campanha não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("Campanha não encontrada.");
            }
        }

        /// <summary>
        /// Remove uma campanha pelo seu ID.
        /// </summary>
        /// <param name="campanhaId">ID da campanha a ser removida.</param>
        public void RemoverCampanha(int campanhaId)
        {
            var campanhas = LerCampanhas();
            var campanhaParaRemover = campanhas.FirstOrDefault(c => c.CampanhaId == campanhaId);

            if (campanhaParaRemover != null)
            {
                campanhas.Remove(campanhaParaRemover);
                GravarCampanhas(campanhas);
            }
            else
            {
                Console.WriteLine("Campanha não encontrada.");
            }
        }
    }
}