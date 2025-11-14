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
    /// Lógica de negócios para gerenciamento de campanhas, incluindo operações como adicionar, editar e remover campanhas.
    /// </summary>
    public class CampanhaBLL
    {
        private CampanhaDAL campanhaDAL = new CampanhaDAL();

        // Lista de IDs de produtos associados às campanhas.
        public List<int> ProdutosAssociados { get; set; } = new List<int>();

        /// <summary>
        /// Obtém uma lista de todas as campanhas.
        /// </summary>
        /// <returns>Lista de campanhas.</returns>
        public List<Campanha> ObterCampanhas()
        {
            return campanhaDAL.LerCampanhas();
        }

        /// <summary>
        /// Obtém uma campanha específica pelo seu ID.
        /// </summary>
        /// <param name="campanhaId">ID da campanha a ser obtida.</param>
        /// <returns>Campanha correspondente ao ID fornecido.</returns>
        public Campanha ObterCampanhaPorId(int campanhaId)
        {
            return campanhaDAL.ObterCampanhaPorId(campanhaId);
        }

        /// <summary>
        /// Apaga todas as campanhas existentes.
        /// </summary>
        public void ApagarTodasCampanhas()
        {
            var listaCampanhas = campanhaDAL.LerCampanhas();

            if (listaCampanhas.Count > 0)
            {
                listaCampanhas.Clear();
                campanhaDAL.GravarCampanhas(listaCampanhas);
                Console.WriteLine("Todas as campanhas foram apagadas com sucesso!");
            }
            else
            {
                Console.WriteLine("A lista de campanhas já está vazia.");
            }
        }

        /// <summary>
        /// Adiciona uma nova campanha.
        /// </summary>
        /// <param name="nome">Nome da campanha.</param>
        /// <param name="dataInicio">Data de início da campanha.</param>
        /// <param name="dataFim">Data de fim da campanha.</param>
        /// <param name="desconto">Desconto oferecido pela campanha.</param>
        /// <param name="idsProdutos">Lista de IDs de produtos associados à campanha.</param>
        public void AdicionarCampanha(string nome, DateTime dataInicio, DateTime dataFim, decimal desconto, List<int> idsProdutos)
        {
            var listaCampanhas = campanhaDAL.LerCampanhas();
            int novoCampanhaId = listaCampanhas.Count > 0 ? listaCampanhas.Max(c => c.CampanhaId) + 1 : 1;

            Campanha novaCampanha = new Campanha(nome, dataInicio, dataFim, desconto)
            {
                CampanhaId = novoCampanhaId,
                ProdutosAssociados = idsProdutos
            };

            listaCampanhas.Add(novaCampanha);
            campanhaDAL.GravarCampanhas(listaCampanhas);
        }

        /// <summary>
        /// Edita uma campanha existente.
        /// </summary>
        /// <param name="campanha">Campanha com as alterações para serem guardadas.</param>
        public void EditarCampanha(Campanha campanha)
        {
            campanhaDAL.EditarCampanha(campanha);
        }

        /// <summary>
        /// Remove uma campanha específica.
        /// </summary>
        /// <param name="campanhaId">ID da campanha a ser removida.</param>
        public void RemoverCampanha(int campanhaId)
        {
            campanhaDAL.RemoverCampanha(campanhaId);
        }
    }

}