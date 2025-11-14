using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class VendaDAL
    {
        private const string vendaFilePath = "vendas.json";

        /// <summary>
        /// Lê e retorna todas as vendas armazenadas no arquivo.
        /// </summary>
        /// <returns>Lista de vendas.</returns>
        public List<Venda> LerVendas()
        {
            if (File.Exists(vendaFilePath))
            {
                var json = File.ReadAllText(vendaFilePath);
                var vendas = JsonConvert.DeserializeObject<List<Venda>>(json);
                return vendas ?? new List<Venda>();
            }
            return new List<Venda>();
        }

        /// <summary>
        /// Grava a lista atualizada de vendas no arquivo.
        /// </summary>
        /// <param name="vendas">Lista de vendas a serem gravadas.</param>
        public void GravarVendas(List<Venda> vendas)
        {
            var json = JsonConvert.SerializeObject(vendas, Formatting.Indented);
            File.WriteAllText(vendaFilePath, json);
            Console.WriteLine("Vendas gravadas com sucesso!");
        }

        /// <summary>
        /// Adiciona uma nova venda à lista existente e atualiza o arquivo.
        /// </summary>
        /// <param name="venda">A venda a ser adicionada.</param>
        public void AdicionarVenda(Venda venda)
        {
            var vendas = LerVendas();
            vendas.Add(venda);
            GravarVendas(vendas);
        }
    }
}