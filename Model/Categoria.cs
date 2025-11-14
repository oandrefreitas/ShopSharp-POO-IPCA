using Newtonsoft.Json;
using System;

namespace Model
{
    /// <summary>
    /// Representa uma categoria geral de produtos.
    /// </summary>
    public class Categoria
    {
        private static int proximoId = 1; // Contador estático para gerar IDs sequenciais

        /// <summary>
        /// Construtor para criar uma nova categoria.
        /// </summary>
        public Categoria(string nome)
        {
            CategoriaID = proximoId++;
            Nome = nome;
        }

        // Propriedades implementadas para informações da categoria.
        public int CategoriaID { get; set; }
        public string Nome { get; set; }
    }

    /// <summary>
    /// Representa uma categoria específica - hardware.
    /// </summary>
    public class CategoriaHardware : Categoria
    {
        /// <summary>
        /// Construtor para criar uma categoria de hardware com um tipo específico.
        /// </summary>
        public CategoriaHardware(string nome, string tipoHardware)
            : base(nome)
        {
            TipoHardware = tipoHardware;
        }

        // Propriedade adicional para especificar o tipo de hardware.
        public string TipoHardware { get; set; }
    }

    /// <summary>
    /// Representa uma categoria específica - software.
    /// </summary>
    public class CategoriaSoftware : Categoria
    {
        /// <summary>
        /// Construtor para criar uma categoria de software com uma versão específica.
        /// </summary>
        public CategoriaSoftware(string nome, string versaoSoftware)
            : base(nome)
        {
            VersaoSoftware = versaoSoftware;
        }

        // Propriedade adicional para especificar a versão do software.
        public string VersaoSoftware { get; set; }
    }

    /// <summary>
    /// Representa uma categoria específica - gadgets.
    /// </summary>
    public class CategoriaGadgets : Categoria
    {
        /// <summary>
        /// Construtor para criar uma categoria de gadgets com uma função específica.
        /// </summary>
        public CategoriaGadgets(string nome, string funcaoGadget)
            : base(nome)
        {
            FuncaoGadget = funcaoGadget;
        }

        // Propriedade adicional para especificar a função do gadget.
        public string FuncaoGadget { get; set; }
    }
}