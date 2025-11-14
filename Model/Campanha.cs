using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Representa uma campanha promocional, incluindo detalhes como nome, período de validade e desconto aplicado.
    /// </summary>
    public class Campanha
    {
        private static int proximoId = 1;

        /// <summary>
        /// Construtor para criar uma nova campanha.
        /// Valida as datas de início e fim, e o valor do desconto.
        /// </summary>
        /// <param name="nome">Nome da campanha.</param>
        /// <param name="dataInicio">Data de início da campanha.</param>
        /// <param name="dataFim">Data de fim da campanha.</param>
        /// <param name="desconto">Percentagem de desconto oferecida pela campanha.</param>
        public Campanha(string nome, DateTime dataInicio, DateTime dataFim, decimal desconto)
        {
            if (dataInicio >= dataFim)
            {
                throw new ArgumentException("A data de início deve ser anterior à data de fim.");
            }
            if (desconto < 0 || desconto > 100)
            {
                throw new ArgumentException("O desconto deve ser entre 0 e 100.");
            }

            CampanhaId = proximoId++;
            Nome = nome;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Desconto = desconto;
        }

        public int CampanhaId { get; set; }
        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public decimal Desconto { get; set; }
        public List<int> ProdutosAssociados { get; set; } = new List<int>();

        /// <summary>
        /// Verifica se a campanha está ativa com base na data atual.
        /// </summary>
        /// <returns>Verdadeiro se a campanha estiver ativa, falso caso contrário.</returns>
        public bool EstaAtiva()
        {
            var agora = DateTime.Now;
            return agora >= DataInicio && agora <= DataFim;
        }
    }
}