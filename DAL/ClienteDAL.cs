using Model;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DAL
{
    public class ClienteDAL
    {
        private const string clienteFilePath = "clientes.json";

        /// <summary>
        /// Lê e retorna todos os clientes armazenados no arquivo.
        /// </summary>
        /// <returns>Lista de clientes.</returns>
        public List<Cliente> LerClientes()
        {
            if (File.Exists(clienteFilePath))
            {
                var json = File.ReadAllText(clienteFilePath);

                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                };

                // Desserializa a lista de clientes, preservando o tipo específico de cada cliente.
                var clientesComTipo = JsonConvert.DeserializeObject<List<JObject>>(json, settings);
                var listaClientes = clientesComTipo.Select(item =>
                {
                    var tipoCliente = item["Tipo"].ToObject<string>();
                    switch (tipoCliente)
                    {
                        case "Particular":
                            return item["Cliente"].ToObject<ClienteParticular>();
                        case "Empresarial":
                            return item["Cliente"].ToObject<ClienteEmpresarial>();
                        default:
                            return item["Cliente"].ToObject<Cliente>();
                    }
                }).ToList();

                return listaClientes;
            }
            return new List<Cliente>();
        }

        /// <summary>
        /// Grava a lista atualizada de clientes no arquivo.
        /// </summary>
        /// <param name="clientes">Lista de clientes a serem gravados.</param>
        public void GravarClientes(List<Cliente> clientes)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };

            // Prepara a lista de clientes com informações de tipo para serialização.
            var clientesComTipo = clientes.Select(cliente =>
            {
                var tipoCliente = cliente is ClienteParticular ? "Particular" : cliente is ClienteEmpresarial ? "Empresarial" : "Comum";
                return new JObject
                {
                    ["Tipo"] = tipoCliente,
                    ["Cliente"] = JToken.FromObject(cliente, JsonSerializer.Create(settings))
                };
            }).ToList();

            var json = JsonConvert.SerializeObject(clientesComTipo, Formatting.Indented);
            File.WriteAllText(clienteFilePath, json);

            Console.WriteLine("Cliente gravado com sucesso!");
        }

        /// <summary>
        /// Obtém um cliente pelo seu ID.
        /// </summary>
        /// <param name="clienteId">ID do cliente.</param>
        /// <returns>O cliente correspondente ao ID, ou null se não encontrado.</returns>
        public Cliente ObterClientePorId(int clienteId)
        {
            var clientes = LerClientes();
            return clientes.FirstOrDefault(c => c.ClienteID == clienteId);
        }

        /// <summary>
        /// Atualiza as informações de um cliente existente.
        /// </summary>
        /// <param name="clienteEditado">Cliente com as informações atualizadas.</param>
        public void EditarCliente(Cliente clienteEditado)
        {
            var clientes = LerClientes();
            var clienteExistente = ObterClientePorId(clienteEditado.ClienteID);

            if (clienteExistente != null)
            {
                var index = clientes.IndexOf(clienteExistente);
                clientes[index] = clienteEditado;
                GravarClientes(clientes);
            }
        }

        /// <summary>
        /// Remove um cliente pelo seu ID.
        /// </summary>
        /// <param name="clienteId">ID do cliente a ser removido.</param>
        public void RemoverCliente(int clienteId)
        {
            var clientes = LerClientes();
            var clienteParaRemover = ObterClientePorId(clienteId);

            if (clienteParaRemover != null)
            {
                clientes.Remove(clienteParaRemover);
                GravarClientes(clientes);
            }
        }
    }
}