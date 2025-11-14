using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MarcaBLL
    {
        private MarcaDAL marcaDAL = new MarcaDAL();

        /// <summary>
        /// Obtém uma lista de todas as marcas registadas.
        /// </summary>
        /// <returns>Lista de marcas.</returns>
        public List<Marca> ObterMarcas()
        {
            return marcaDAL.LerMarcas();
        }

        /// <summary>
        /// Obtém uma marca específica pelo seu ID.
        /// </summary>
        /// <param name="marcaId">ID da marca.</param>
        /// <returns>A marca correspondente ao ID.</returns>
        public Marca ObterMarcaPorId(int marcaId)
        {
            return marcaDAL.ObterMarcaPorId(marcaId);
        }

        /// <summary>
        /// Apaga todas as marcas registadas.
        /// </summary>
        public void ApagarTodasMarcas()
        {
            var listaMarcas = marcaDAL.LerMarcas();

            if (listaMarcas.Count > 0)
            {
                listaMarcas.Clear();
                marcaDAL.GravarMarcas(listaMarcas);
                Console.WriteLine("Todos as marcas foram apagados com sucesso!");
            }
            else
            {
                Console.WriteLine("A lista de marcas já está vazia.");
            }
        }

        /// <summary>
        /// Adiciona uma nova marca.
        /// </summary>
        /// <param name="nome">Nome da nova marca.</param>
        public void AdicionarMarca(string nome)
        {
            var marcas = marcaDAL.LerMarcas();

            int novaMarcaID = marcas.Count > 0 ? marcas.Max(m => m.MarcaID) + 1 : 1;

            Marca novaMarca = new Marca(nome)
            {
                MarcaID = novaMarcaID
            };

            marcas.Add(novaMarca);
            marcaDAL.GravarMarcas(marcas);
        }

        /// <summary>
        /// Edita as informações de uma marca existente.
        /// </summary>
        /// <param name="marca">Marca com as informações atualizadas.</param>
        public void EditarMarca(Marca marca)
        {
            marcaDAL.EditarMarca(marca);
        }

        /// <summary>
        /// Remove uma marca pelo seu ID.
        /// </summary>
        /// <param name="marcaId">ID da marca a ser removida.</param>
        public void RemoverMarca(int marcaId)
        {
            marcaDAL.RemoverMarca(marcaId);
        }
    }
}