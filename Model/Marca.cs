using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Representa uma marca de produtos. Esta classe é utilizada para gerir as diferentes marcas
    /// associadas aos produtos dentro do sistema.
    /// </summary>
    public class Marca
    {
        private static int proximoId = 1;

        /// <summary>
        /// Construtor para criar uma nova marca. O ID da marca é gerado automaticamente.
        /// </summary>
        /// <param name="nome">Nome da marca.</param>
        public Marca(string nome)
        {
            MarcaID = proximoId++;
            Nome = nome;
        }

        // Propriedades implementadas para o ID e nome da marca.
        public int MarcaID { get; set; }
        public string Nome { get; set; }
    }
}