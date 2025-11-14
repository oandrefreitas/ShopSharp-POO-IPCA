using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Representa um cliente geral com informações comuns a todos os tipos de clientes.
    /// </summary>
    public class Cliente
    {
        private static int proximoId = 1; // Contador estático para gerar IDs sequenciais

        /// <summary>
        /// Construtor para criar um novo cliente.
        /// </summary>
        public Cliente(string nome, string email, string palavraPasse, string contacto, string morada, string nif)
        {
            ClienteID = proximoId++;
            Nome = nome;
            Email = email;
            PalavraPasse = palavraPasse;
            Contacto = contacto;
            Morada = morada;
            NIF = nif;
        }

        // Propriedades implementadas para informações do cliente.
        public int ClienteID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string PalavraPasse { get; set; }
        public string Contacto { get; set; }
        public string Morada { get; set; }
        public string NIF { get; set; }
    }

    /// <summary>
    /// Representa um cliente particular, que é um tipo específico de cliente.
    /// </summary>
    public class ClienteParticular : Cliente
    {
        /// <summary>
        /// Construtor para criar um cliente particular com informações adicionais específicas.
        /// </summary>
        public ClienteParticular(string nome, string email, string palavraPasse, string contacto, string morada, string nif, string cartaoCredito)
            : base(nome, email, palavraPasse, contacto, morada, nif)
        {
            CartaoCredito = cartaoCredito;
        }

        // Propriedade adicional para cliente particular.
        public string CartaoCredito { get; set; }
    }

    /// <summary>
    /// Representa um cliente empresarial, que é um tipo específico de cliente.
    /// </summary>
    public class ClienteEmpresarial : Cliente
    {
        /// <summary>
        /// Construtor para criar um cliente empresarial com informações adicionais específicas.
        /// </summary>
        public ClienteEmpresarial(string nome, string email, string palavraPasse, string contacto, string morada, string nif, string nomeResponsavel)
            : base(nome, email, palavraPasse, contacto, morada, nif)
        {
            NomeResponsavel = nomeResponsavel;
        }

        // Propriedade adicional para cliente empresarial.
        public string NomeResponsavel { get; set; }
    }
}