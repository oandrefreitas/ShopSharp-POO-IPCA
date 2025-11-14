using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    namespace Model
    {
        /// <summary>
        /// Representa a garantia de um produto. Esta classe é utilizada para gerir diferentes tipos
        /// de garantias e suas durações associadas aos produtos.
        /// </summary>
        public class Garantia
        {
            private static int proximoId = 1;

            /// <summary>
            /// Construtor para criar uma nova garantia. O ID da garantia é gerado automaticamente.
            /// </summary>
            /// <param name="tipo">Tipo da garantia (ex: "Standard", "Estendida").</param>
            /// <param name="duracaoMeses">Duração da garantia em meses.</param>
            public Garantia(string tipo, int duracaoMeses)
            {
                GarantiaId = proximoId++;
                Tipo = tipo;
                DuracaoMeses = duracaoMeses;
            }

            // Propriedades implementadas para o ID, tipo e duração da garantia.
            public int GarantiaId { get; set; }
            public string Tipo { get; set; }
            public int DuracaoMeses { get; set; }
        }
    }
}