using DAL;
using Model;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class ClienteBLL
    {
        private ClienteDAL clienteDAL = new ClienteDAL();

        /// <summary>
        /// Obtém todos os clientes registados.
        /// </summary>
        /// <returns>Uma lista de clientes.</returns>
        public List<Cliente> ObterClientes()
        {
            return clienteDAL.LerClientes();
        }

        /// <summary>
        /// Obtém um cliente específico pelo seu ID.
        /// </summary>
        /// <param name="clienteId">ID do cliente.</param>
        /// <returns>O cliente correspondente ao ID.</returns>
        public Cliente ObterClientePorId(int clienteId)
        {
            return clienteDAL.ObterClientePorId(clienteId);
        }

        /// <summary>
        /// Apaga todos os clientes registados.
        /// </summary>
        public void ApagarTodosClientes()
        {
            var listaClientes = clienteDAL.LerClientes();

            if (listaClientes.Count > 0)
            {
                listaClientes.Clear();
                clienteDAL.GravarClientes(listaClientes);
                Console.WriteLine("Todos os clientes foram apagados com sucesso!");
            }
            else
            {
                Console.WriteLine("A lista de clientes já está vazia.");
            }
        }

        /// <summary>
        /// Adiciona um novo cliente particular.
        /// </summary>
        /// <param name="nome">Nome do cliente.</param>
        /// <param name="email">Email do cliente.</param>
        /// <param name="palavraPasse">Palavra-passe do cliente.</param>
        /// <param name="contacto">Contacto telefónico do cliente.</param>
        /// <param name="morada">Morada do cliente.</param>
        /// <param name="nif">Número de Identificação Fiscal do cliente.</param>
        /// <param name="cartaoCredito">Número do cartão de crédito do cliente.</param>
        public void AdicionarClienteParticular(string nome, string email, string palavraPasse, string contacto, string morada, string nif, string cartaoCredito)
        {
            var listaClientes = clienteDAL.LerClientes();

            int novoClienteID = listaClientes.Count > 0 ? listaClientes.Max(c => c.ClienteID) + 1 : 1;

            ClienteParticular novoClienteParticular = new ClienteParticular(nome, email, palavraPasse, contacto, morada, nif, cartaoCredito)
            {
                ClienteID = novoClienteID
            };

            listaClientes.Add(novoClienteParticular);
            clienteDAL.GravarClientes(listaClientes);
        }

        /// <summary>
        /// Adiciona um novo cliente empresarial.
        /// </summary>
        /// <param name="nome">Nome da empresa.</param>
        /// <param name="email">Email da empresa.</param>
        /// <param name="palavraPasse">Palavra-passe da empresa.</param>
        /// <param name="contacto">Contacto telefónico da empresa.</param>
        /// <param name="morada">Morada da empresa.</param>
        /// <param name="nif">Número de Identificação Fiscal da empresa.</param>
        /// <param name="nomeResponsavel">Nome do responsável pela empresa.</param>
        public void AdicionarClienteEmpresarial(string nome, string email, string palavraPasse, string contacto, string morada, string nif, string nomeResponsavel)
        {
            var listaClientes = clienteDAL.LerClientes();

            int novoClienteID = listaClientes.Count > 0 ? listaClientes.Max(c => c.ClienteID) + 1 : 1;

            ClienteEmpresarial novoClienteEmpresarial = new ClienteEmpresarial(nome, email, palavraPasse, contacto, morada, nif, nomeResponsavel)
            {
                ClienteID = novoClienteID
            };

            listaClientes.Add(novoClienteEmpresarial);
            clienteDAL.GravarClientes(listaClientes);
        }

        /// <summary>
        /// Edita as informações de um cliente existente.
        /// </summary>
        /// <param name="cliente">Cliente com as informações atualizadas.</param>
        public void EditarCliente(Cliente cliente)
        {
            clienteDAL.EditarCliente(cliente);
        }

        /// <summary>
        /// Remove um cliente pelo seu ID.
        /// </summary>
        /// <param name="clienteId">ID do cliente a ser removido.</param>
        public void RemoverCliente(int clienteId)
        {
            clienteDAL.RemoverCliente(clienteId);
        }
    }
}