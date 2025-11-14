using BLL;
using DAL;
using Model;
using System.ComponentModel;

namespace ShopSharp
{
    class Program
    {
        #region Main
        static void Main(string[] args)
        {
            bool continuar = true;
            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("Bem-vindo à ShopSharp");
                Console.WriteLine("1. Área de Gestor");
                Console.WriteLine("2. Área de Cliente");
                Console.WriteLine("3. Registar Conta de Cliente");
                Console.WriteLine("4. Sair");

                Console.Write("Escolha uma opção: ");
                string escolha = Console.ReadLine();

                switch (escolha)
                {
                    case "1":
                        MenuGestor();
                        break;
                    case "2":
                        MenuCliente();
                        break;
                    case "3":
                        RegistarContaCliente();
                        break;
                    case "4":
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Por favor, tente novamente.");
                        break;
                }

                if (continuar)
                {
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
        #endregion Main

        #region Área de Gestor
        static bool RealizarLogin()
        {
            const string utilizador = "admin";
            const string palavraPasse = "admin123";

            Console.WriteLine("Por favor, insira as suas credenciais.");

            Console.Write("Nome de utilizador: ");
            string nomeUtilizador = Console.ReadLine();

            Console.Write("Palavra-passe: ");
            string palavraPasseUtilizador = Console.ReadLine();

            return nomeUtilizador == utilizador && palavraPasseUtilizador == palavraPasse;
        }
        static void MenuGestor()
        {
            if (!RealizarLogin())
            {
                Console.WriteLine("Credenciais inválidas!");
                return;
            }
            bool continuarGestor = true;
            while (continuarGestor)
            {
                Console.Clear();
                Console.WriteLine("Área de Gestor");
                Console.WriteLine("1. Gerir Campanhas");
                Console.WriteLine("2. Gerir Categorias");
                Console.WriteLine("3. Gerir Clientes");
                Console.WriteLine("4. Gerir Marcas");
                Console.WriteLine("5. Gerir Produtos");
                Console.WriteLine("6. Listar Vendas");
                Console.WriteLine("7. Voltar");

                Console.Write("Escolha uma opção: ");
                string escolhaGestor = Console.ReadLine();

                switch (escolhaGestor)
                {
                    case "1":
                        GerirCampanhas();
                        break;
                    case "2":
                        GerirCategorias();
                        break;
                    case "3":
                        GerirClientes();
                        break;
                    case "4":
                        GerirMarcas();
                        break;
                    case "5":
                        GerirProdutos();
                        break;
                    case "6":
                        ListarVendas();
                        break;
                    case "7":
                        continuarGestor = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Por favor, tente novamente.");
                        break;
                }

                if (continuarGestor)
                {
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void ListarVendas()
        {
            Console.Clear();
            Console.WriteLine("Vendas registadas na ShopSharp...");

            VendaBLL vendaBLL = new VendaBLL();
            List<Venda> vendas = vendaBLL.ObterVendas();

            if (vendas.Count == 0)
            {
                Console.WriteLine("Nenhuma venda registada.");
            }
            else
            {
                foreach (Venda venda in vendas)
                {
                    Console.WriteLine($"ID da Venda: {venda.VendaId}, Data: {venda.DataVenda}, Total: {venda.Total:C}");
                }
            }

            Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
            Console.ReadKey();
        }
        #endregion Área de Gestor

        #region Gestão de Campanhas
        static void GerirCampanhas()
        {
            CampanhaBLL campanhaBLL = new CampanhaBLL();
            ProdutoBLL produtoBLL = new ProdutoBLL(); // Adicionado para suporte à edição de campanhas

            bool continuarCampanha = true;
            while (continuarCampanha)
            {
                Console.Clear();
                Console.WriteLine("Gerir Campanhas");
                Console.WriteLine("1. Listar Campanhas");
                Console.WriteLine("2. Adicionar Campanha");
                Console.WriteLine("3. Editar Campanha");
                Console.WriteLine("4. Remover Campanha");
                Console.WriteLine("5. Apagar Todas as Campanhas");
                Console.WriteLine("6. Voltar");

                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        ListarCampanhas(campanhaBLL);
                        break;
                    case "2":
                        AdicionarCampanha(campanhaBLL, produtoBLL);
                        break;
                    case "3":
                        EditarCampanha(campanhaBLL, produtoBLL);
                        break;
                    case "4":
                        RemoverCampanha(campanhaBLL);
                        break;
                    case "5":
                        campanhaBLL.ApagarTodasCampanhas();
                        Console.WriteLine("Todas as campanhas foram apagadas.");
                        break;
                    case "6":
                        continuarCampanha = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                if (continuarCampanha)
                {
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void ListarCampanhas(CampanhaBLL campanhaBLL)
        {
            var campanhas = campanhaBLL.ObterCampanhas();
            if (campanhas.Count == 0)
            {
                Console.WriteLine("Não há campanhas registadas.");
                return;
            }

            foreach (var campanha in campanhas)
            {
                Console.WriteLine($"ID: {campanha.CampanhaId}, Nome: {campanha.Nome}, Início: {campanha.DataInicio.ToShortDateString()}, Fim: {campanha.DataFim.ToShortDateString()}, Desconto: {campanha.Desconto}%");
            }
        }

        static void AdicionarCampanha(CampanhaBLL campanhaBLL, ProdutoBLL produtoBLL)
        {
            Console.Write("Nome da Campanha: ");
            string nome = Console.ReadLine();

            Console.Write("Data de Início (dd/mm/aaaa): ");
            DateTime dataInicio = DateTime.Parse(Console.ReadLine());

            Console.Write("Data de Fim (dd/mm/aaaa): ");
            DateTime dataFim = DateTime.Parse(Console.ReadLine());

            Console.Write("Desconto (%): ");
            decimal desconto = decimal.Parse(Console.ReadLine());

            // Listar produtos para escolha
            ListarProdutos(produtoBLL);

            Console.WriteLine("Digite os IDs dos produtos a serem associados à campanha (separados por vírgula): ");
            var idsProdutosInput = Console.ReadLine();
            var idsProdutos = idsProdutosInput.Split(',').Select(int.Parse).ToList();

            campanhaBLL.AdicionarCampanha(nome, dataInicio, dataFim, desconto, idsProdutos);
            Console.WriteLine("Campanha adicionada com sucesso.");
        }

        static void EditarCampanha(CampanhaBLL campanhaBLL, ProdutoBLL produtoBLL)
        {
            Console.Write("Digite o ID da campanha que deseja editar: ");
            int id = int.Parse(Console.ReadLine());

            var campanha = campanhaBLL.ObterCampanhaPorId(id);
            if (campanha == null)
            {
                Console.WriteLine("Campanha não encontrada.");
                return;
            }

            Console.Write("Novo nome da Campanha (deixe em branco para não alterar): ");
            string nome = Console.ReadLine();
            if (!string.IsNullOrEmpty(nome))
            {
                campanha.Nome = nome;
            }

            Console.Write("Nova data de Início (dd/mm/aaaa) (deixe em branco para não alterar): ");
            string dataInicioInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(dataInicioInput))
            {
                campanha.DataInicio = DateTime.Parse(dataInicioInput);
            }

            Console.Write("Nova data de Fim (dd/mm/aaaa) (deixe em branco para não alterar): ");
            string dataFimInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(dataFimInput))
            {
                campanha.DataFim = DateTime.Parse(dataFimInput);
            }

            Console.Write("Novo desconto (%) (deixe em branco para não alterar): ");
            string descontoInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(descontoInput))
            {
                campanha.Desconto = decimal.Parse(descontoInput);
            }

            // Opção de alterar produtos associados
            Console.WriteLine("Deseja alterar os produtos associados? (s/n)");
            if (Console.ReadLine().ToLower() == "s")
            {
                ListarProdutos(produtoBLL);

                Console.WriteLine("Digite os novos IDs dos produtos a serem associados à campanha (separados por vírgula): ");
                var idsProdutosInput = Console.ReadLine();
                var idsProdutos = idsProdutosInput.Split(',').Select(int.Parse).ToList();

                campanha.ProdutosAssociados = idsProdutos;
            }

            campanhaBLL.EditarCampanha(campanha);
            Console.WriteLine("Campanha editada com sucesso.");
        }

        static void RemoverCampanha(CampanhaBLL campanhaBLL)
        {
            Console.Write("Informe o ID da campanha a remover: ");
            int id = int.Parse(Console.ReadLine());

            campanhaBLL.RemoverCampanha(id);
            Console.WriteLine("Campanha removida com sucesso.");
        }
        #endregion Gestão de Campanhas

        #region Gestão de Categorias
        static void GerirCategorias()
        {
            CategoriaBLL categoriaBLL = new CategoriaBLL();

            bool continuarCategoria = true;
            while (continuarCategoria)
            {
                Console.Clear();
                Console.WriteLine("Gerir Categorias");
                Console.WriteLine("1. Listar Categorias");
                Console.WriteLine("2. Adicionar Categoria");
                Console.WriteLine("3. Editar Categoria");
                Console.WriteLine("4. Remover Categoria");
                Console.WriteLine("5. Apagar Todas as Categorias");
                Console.WriteLine("6. Voltar");

                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        ListarCategorias(categoriaBLL);
                        break;
                    case "2":
                        AdicionarCategoria(categoriaBLL);
                        break;
                    case "3":
                        EditarCategoria(categoriaBLL);
                        break;
                    case "4":
                        RemoverCategoria(categoriaBLL);
                        break;
                    case "5":
                        categoriaBLL.ApagarTodasCategorias();
                        Console.WriteLine("Todas as categorias foram apagadas.");
                        break;
                    case "6":
                        continuarCategoria = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                if (continuarCategoria)
                {
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void ListarCategorias(CategoriaBLL categoriaBLL)
        {
            var categorias = categoriaBLL.ObterCategorias();
            if (categorias.Count == 0)
            {
                Console.WriteLine("Nenhuma categoria disponível.");
            }
            else
            {
                foreach (var categoria in categorias)
                {
                    Console.WriteLine($"ID: {categoria.CategoriaID}, Nome: {categoria.Nome}");
                    if (categoria is CategoriaHardware hardware)
                    {
                        Console.WriteLine($" - Tipo de Hardware: {hardware.TipoHardware}");
                    }
                    else if (categoria is CategoriaSoftware software)
                    {
                        Console.WriteLine($" - Versão do Software: {software.VersaoSoftware}");
                    }
                    else if (categoria is CategoriaGadgets gadgets)
                    {
                        Console.WriteLine($" - Função do Gadget: {gadgets.FuncaoGadget}");
                    }
                }
            }
        }

        static void AdicionarCategoria(CategoriaBLL categoriaBLL)
        {
            Console.WriteLine("Adicionar nova categoria");
            Console.Write("Nome da Categoria: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Tipo de Categoria (1-Hardware, 2-Software, 3-Gadgets): ");
            string tipo = Console.ReadLine();
            string detalhe = "";

            switch (tipo)
            {
                case "1":
                    Console.Write("Digite o tipo de hardware (ex: CPU, GPU): ");
                    detalhe = Console.ReadLine();
                    break;
                case "2":
                    Console.Write("Digite a versão do software (ex: 1.0, 2023.1): ");
                    detalhe = Console.ReadLine();
                    break;
                case "3":
                    Console.Write("Digite a função do gadget (ex: Smartwatch, Drone): ");
                    detalhe = Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Tipo de categoria inválido.");
                    return;
            }

            categoriaBLL.AdicionarCategoria(nome, tipo, detalhe);
            Console.WriteLine("Categoria adicionada com sucesso!");
        }

        static void EditarCategoria(CategoriaBLL categoriaBLL)
        {
            ListarCategorias(categoriaBLL);
            Console.Write("Digite o ID da categoria que deseja editar: ");
            if (!int.TryParse(Console.ReadLine(), out int categoriaId))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var categoria = categoriaBLL.ObterCategoriaPorId(categoriaId);
            if (categoria == null)
            {
                Console.WriteLine("Categoria não encontrada.");
                return;
            }

            Console.WriteLine("Digite os novos detalhes da categoria: ");
            Console.Write("Novo Nome: ");
            categoria.Nome = Console.ReadLine();

            // Atualizar detalhes específicos de cada subclasse
            if (categoria is CategoriaHardware hardware)
            {
                Console.Write("Novo Tipo de Hardware: ");
                hardware.TipoHardware = Console.ReadLine();
            }
            else if (categoria is CategoriaSoftware software)
            {
                Console.Write("Nova Versão do Software: ");
                software.VersaoSoftware = Console.ReadLine();
            }
            else if (categoria is CategoriaGadgets gadgets)
            {
                Console.Write("Nova Função do Gadget: ");
                gadgets.FuncaoGadget = Console.ReadLine();
            }

            categoriaBLL.EditarCategoria(categoria);
            Console.WriteLine("Categoria atualizada com sucesso!");
        }

        static void RemoverCategoria(CategoriaBLL categoriaBLL)
        {
            ListarCategorias(categoriaBLL);
            Console.Write("Digite o ID da categoria que deseja remover: ");
            if (!int.TryParse(Console.ReadLine(), out int categoriaId))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            categoriaBLL.RemoverCategoria(categoriaId);
            Console.WriteLine("Categoria removida com sucesso!");
        }


        #endregion Gestão de Categorias

        #region Gestão de Clientes
        static void GerirClientes()
        {
            ClienteBLL clienteBLL = new ClienteBLL();

            bool continuarCliente = true;
            while (continuarCliente)
            {
                Console.Clear();
                Console.WriteLine("Gerir Clientes");
                Console.WriteLine("1. Listar Clientes");
                Console.WriteLine("2. Apagar Todos os Clientes");
                Console.WriteLine("3. Voltar");

                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        ListarClientes(clienteBLL);
                        break;
                    case "2":
                        clienteBLL.ApagarTodosClientes();
                        Console.WriteLine("Todos os clientes foram apagados.");
                        break;
                    case "3":
                        continuarCliente = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }

       
        // Método para listar todos os clientes
        static void ListarClientes(ClienteBLL clienteBLL)
        {
            var clientes = clienteBLL.ObterClientes();
            if (clientes.Count == 0)
            {
                Console.WriteLine("Não há clientes registados.");
            }
            else
            {
                foreach (var cliente in clientes)
                {
                    Console.WriteLine($"ID: {cliente.ClienteID}, Nome: {cliente.Nome}, Email: {cliente.Email}, NIF: {cliente.NIF}");
                    // Adicione mais detalhes conforme necessário
                }
            }
        }
        #endregion Gestão de Clientes

        #region Gestão de Marcas
        static void GerirMarcas()
        {
            MarcaBLL marcaBLL = new MarcaBLL();

            bool continuarMarca = true;
            while (continuarMarca)
            {
                Console.Clear();
                Console.WriteLine("Gerir Marcas");
                Console.WriteLine("1. Listar Marcas");
                Console.WriteLine("2. Adicionar Marca");
                Console.WriteLine("3. Editar Marca");
                Console.WriteLine("4. Remover Marca");
                Console.WriteLine("5. Apagar Todas as Marcas");
                Console.WriteLine("6. Voltar");

                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        ListarMarcas(marcaBLL);
                        break;
                    case "2":
                        AdicionarMarca(marcaBLL);
                        break;
                    case "3":
                        EditarMarca(marcaBLL);
                        break;
                    case "4":
                        RemoverMarca(marcaBLL);
                        break;
                    case "5":
                        marcaBLL.ApagarTodasMarcas();
                        Console.WriteLine("Todas as marcas foram apagadas.");
                        break;
                    case "6":
                        continuarMarca = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                if (continuarMarca)
                {
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void ListarMarcas(MarcaBLL marcaBLL)
        {
            var marcas = marcaBLL.ObterMarcas();
            if (marcas.Count == 0)
            {
                Console.WriteLine("Não existem marcas registadas.");
            }
            else
            {
                foreach (var marca in marcas)
                {
                    Console.WriteLine($"ID: {marca.MarcaID}, Nome: {marca.Nome}");
                }
            }
        }

        static void AdicionarMarca(MarcaBLL marcaBLL)
        {
            Console.Write("Insira o nome da nova marca: ");
            string nomeMarca = Console.ReadLine();
            marcaBLL.AdicionarMarca(nomeMarca);
            Console.WriteLine("Marca adicionada com sucesso!");
        }

        static void EditarMarca(MarcaBLL marcaBLL)
        {
            Console.Write("Insira o ID da marca que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());
            var marca = marcaBLL.ObterMarcaPorId(id);

            if (marca == null)
            {
                Console.WriteLine("Marca não encontrada.");
                return;
            }

            Console.WriteLine($"Editar Marca: {marca.Nome}");
            Console.Write("Novo nome: ");
            marca.Nome = Console.ReadLine();

            marcaBLL.EditarMarca(marca);
            Console.WriteLine("Marca editada com sucesso!");
        }

        static void RemoverMarca(MarcaBLL marcaBLL)
        {
            Console.Write("Insira o ID da marca que deseja remover: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var marca = marcaBLL.ObterMarcaPorId(id);
            if (marca == null)
            {
                Console.WriteLine("Marca não encontrada.");
                return;
            }

            marcaBLL.RemoverMarca(id);
            Console.WriteLine("Marca removida com sucesso!");
        }

        #endregion Gestão de Marcas

        #region Gestão de Produtos
        static void GerirProdutos()
        {
            ProdutoBLL produtoBLL = new ProdutoBLL();
            CategoriaBLL categoriaBLL = new CategoriaBLL();
            MarcaBLL marcaBLL = new MarcaBLL();

            bool continuarProduto = true;
            while (continuarProduto)
            {
                Console.Clear();
                Console.WriteLine("Gerir Produtos");
                Console.WriteLine("1. Listar Produtos");
                Console.WriteLine("2. Adicionar Produto");
                Console.WriteLine("3. Editar Produto");
                Console.WriteLine("4. Remover Produto");
                Console.WriteLine("5. Apagar Todos os Produtos");
                Console.WriteLine("6. Voltar");

                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        ListarProdutos(produtoBLL);
                        break;
                    case "2":
                        AdicionarProduto(produtoBLL, categoriaBLL, marcaBLL);
                        break;
                    case "3":
                        EditarProduto(produtoBLL);
                        break;
                    case "4":
                        RemoverProduto(produtoBLL);
                        break;
                    case "5":
                        produtoBLL.ApagarTodosProdutos();
                        Console.WriteLine("Todos os produtos foram apagados com sucesso!");
                        break;
                    case "6":
                        continuarProduto = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                if (continuarProduto)
                {
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void ListarProdutos(ProdutoBLL produtoBLL)
        {
            var produtos = produtoBLL.ObterProdutos();
            if (produtos.Count == 0)
            {
                Console.WriteLine("Não existem produtos registados.");
            }
            else
            {
                foreach (var produto in produtos)
                {
                    Console.WriteLine($"ID: {produto.ProdutoId}, Nome: {produto.Nome}, Preço: {produto.Preco}, Stock: {produto.Stock}, Marca: {produto.Marca.Nome}, Categoria: {produto.Categoria.Nome}");
                }
            }
        }

        static void AdicionarProduto(ProdutoBLL produtoBLL, CategoriaBLL categoriaBLL, MarcaBLL marcaBLL)
        {
            Console.Write("Nome do produto: ");
            string nome = Console.ReadLine();

            Console.Write("Preço do produto: ");
            decimal preco = decimal.Parse(Console.ReadLine());

            Console.Write("Stock do produto: ");
            int stock = int.Parse(Console.ReadLine());

            Console.Write("Nome da marca: ");
            string nomeMarca = Console.ReadLine();

            Console.Write("Garantia (em meses): ");
            int garantiaMeses = int.Parse(Console.ReadLine());

            Console.Write("Nome da categoria: ");
            string nomeCategoria = Console.ReadLine();

            Console.Write("Tipo de categoria (Hardware, Software, Gadgets): ");
            string tipoCategoria = Console.ReadLine();

            Console.Write("Detalhe da categoria: ");
            string detalheCategoria = Console.ReadLine();

            produtoBLL.AdicionarProduto(nome, preco, stock, nomeMarca, garantiaMeses, nomeCategoria, tipoCategoria, detalheCategoria);
            Console.WriteLine("Produto adicionado com sucesso!");
        }

        static void EditarProduto(ProdutoBLL produtoBLL)
        {
            Console.Write("Insira o ID do produto que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());
            var produto = produtoBLL.ObterProdutoPorId(id);

            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado.");
                return;
            }

            Console.WriteLine($"Editar Produto: {produto.Nome}");
            Console.Write("Novo nome: ");
            produto.Nome = Console.ReadLine();

            Console.Write("Novo preço: ");
            produto.Preco = decimal.Parse(Console.ReadLine());

            Console.Write("Novo stock: ");
            produto.Stock = int.Parse(Console.ReadLine());

            produtoBLL.EditarProduto(produto);
            Console.WriteLine("Produto editado com sucesso!");
        }

        static void RemoverProduto(ProdutoBLL produtoBLL)
        {
            Console.Write("Insira o ID do produto que deseja remover: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var produto = produtoBLL.ObterProdutoPorId(id);
            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado.");
                return;
            }

            produtoBLL.RemoverProduto(id);
            Console.WriteLine("Produto removido com sucesso!");
        }

        #endregion Gestão de Produtos

        #region Área de Cliente
        static bool RealizarLoginCliente(ref int clienteId)
        {
            Console.WriteLine("Por favor, insira suas credenciais de login.");

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Palavra-passe: ");
            string palavraPasse = Console.ReadLine();

            ClienteBLL clienteBLL = new ClienteBLL();
            List<Cliente> clientes = clienteBLL.ObterClientes();

            var clienteLogado = clientes.FirstOrDefault(c => c.Email == email && c.PalavraPasse == palavraPasse);
            if (clienteLogado != null)
            {
                clienteId = clienteLogado.ClienteID; // Atribuir o ID do cliente autenticado
                return true;
            }

            return false; // Login falhou
        }

        static void MenuCliente()
        {
            Console.Clear();
            Console.WriteLine("Área de Cliente");

            int clienteId = -1;
            if (!RealizarLoginCliente(ref clienteId))
            {
                Console.WriteLine("Credenciais inválidas!");
                return;
            }

            bool continuarCliente = true;
            while (continuarCliente)
            {
                Console.Clear();
                Console.WriteLine("Bem-vindo à área do cliente!");
                Console.WriteLine("1. Editar Conta Cliente");
                Console.WriteLine("2. Remover Conta Cliente");
                Console.WriteLine("3. Consultar Campanhas");
                Console.WriteLine("4. Consultar Produtos Por Categoria");
                Console.WriteLine("5. Consultar Produtos Por Marcas");
                Console.WriteLine("6. Comprar Produtos");
                Console.WriteLine("7. Sair");

                Console.Write("Escolha uma opção: ");
                string escolhaCliente = Console.ReadLine();

                switch (escolhaCliente)
                {
                    case "1":
                        EditarCliente(clienteId);
                        break;
                    case "2":
                        RemoverCliente(clienteId);
                        continuarCliente = false; // Sair do menu após remover a conta
                        break;
                    case "3":
                        ConsultarCampanhas();
                        break;
                    case "4":
                        ConsultarProdutosPorCategoria();
                        break;
                    case "5":
                        ConsultarProdutosPorMarca();
                        break;
                    case "6":
                        ComprarProdutos(clienteId); // clienteId é o ID do cliente logado
                        break;
                    case "7":
                        continuarCliente = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Por favor, tente novamente.");
                        break;
                }

                if (continuarCliente)
                {
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        #region Métodos para Área de Clientes
        static void EditarCliente(int clienteId)
        {
            ClienteBLL clienteBLL = new ClienteBLL();
            Cliente cliente = clienteBLL.ObterClientePorId(clienteId);

            if (cliente == null)
            {
                Console.WriteLine("Cliente não encontrado.");
                return;
            }

            // Atualizar informações comuns a todos os clientes
            Console.Write("Nome atual: " + cliente.Nome + ". Novo nome (deixe em branco para manter): ");
            string novoNome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoNome))
            {
                cliente.Nome = novoNome;
            }

            Console.Write("Email atual: " + cliente.Email + ". Novo email (deixe em branco para manter): ");
            string novoEmail = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoEmail))
            {
                cliente.Email = novoEmail;
            }

            Console.Write("Nova palavra-passe (deixe em branco para manter): ");
            string novaPalavraPasse = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novaPalavraPasse))
            {
                cliente.PalavraPasse = novaPalavraPasse;
            }

            Console.Write("Morada atual: " + cliente.Morada + ". Nova morada (deixe em branco para manter): ");
            string novaMorada = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novaMorada))
            {
                cliente.Morada = novaMorada;
            }

            Console.Write("NIF atual: " + cliente.NIF + ". Novo NIF (deixe em branco para manter): ");
            string novoNif = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(novoNif))
            {
                cliente.NIF = novoNif;
            }

            // Atualizar informações específicas para Cliente Particular
            if (cliente is ClienteParticular clienteParticular)
            {
                Console.Write("Cartão de Crédito atual: " + clienteParticular.CartaoCredito + ". Novo cartão (deixe em branco para manter): ");
                string novoCartaoCredito = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(novoCartaoCredito))
                {
                    clienteParticular.CartaoCredito = novoCartaoCredito;
                }
            }

            // Atualizar informações específicas para Cliente Empresarial
            else if (cliente is ClienteEmpresarial clienteEmpresarial)
            {
                Console.Write("Nome do Responsável atual: " + clienteEmpresarial.NomeResponsavel + ". Novo nome (deixe em branco para manter): ");
                string novoNomeResponsavel = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(novoNomeResponsavel))
                {
                    clienteEmpresarial.NomeResponsavel = novoNomeResponsavel;
                }
            }

            // Salvar as alterações
            clienteBLL.EditarCliente(cliente);
            Console.WriteLine("Informações da conta atualizadas com sucesso.");
        }

        static void RemoverCliente(int clienteId)
        {
            ClienteBLL clienteBLL = new ClienteBLL();
            clienteBLL.RemoverCliente(clienteId);
            Console.WriteLine("Conta removida com sucesso.");
        }

        static void ConsultarCampanhas()
        {
            CampanhaBLL campanhaBLL = new CampanhaBLL();
            List<Campanha> campanhas = campanhaBLL.ObterCampanhas();

            Console.Clear();
            Console.WriteLine("Campanhas Disponíveis:");
            if (campanhas.Count == 0)
            {
                Console.WriteLine("Não há campanhas disponíveis no momento.");
            }
            else
            {
                foreach (Campanha campanha in campanhas)
                {
                    Console.WriteLine($"ID: {campanha.CampanhaId}, Nome: {campanha.Nome}, Início: {campanha.DataInicio.ToShortDateString()}, Fim: {campanha.DataFim.ToShortDateString()}, Desconto: {campanha.Desconto}%");
                }
            }

            Console.WriteLine("Pressione qualquer tecla para voltar.");
            Console.ReadKey();
        }

        static void ConsultarProdutosPorCategoria()
        {
            CategoriaBLL categoriaBLL = new CategoriaBLL();
            List<Categoria> categorias = categoriaBLL.ObterCategorias();

            Console.Clear();
            Console.WriteLine("Escolha uma categoria:");
            Console.WriteLine("1. Hardware");
            Console.WriteLine("2. Software");
            Console.WriteLine("3. Gadgets");

            Console.Write("Escolha uma opção: ");
            string escolhaCategoria = Console.ReadLine();
            List<Categoria> categoriasFiltradas;

            switch (escolhaCategoria)
            {
                case "1":
                    categoriasFiltradas = categorias.OfType<CategoriaHardware>().Cast<Categoria>().ToList();
                    break;
                case "2":
                    categoriasFiltradas = categorias.OfType<CategoriaSoftware>().Cast<Categoria>().ToList();
                    break;
                case "3":
                    categoriasFiltradas = categorias.OfType<CategoriaGadgets>().Cast<Categoria>().ToList();
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    return;
            }

            Console.Clear();
            Console.WriteLine("Categorias Disponíveis:");
            foreach (var cat in categoriasFiltradas)
            {
                Console.WriteLine($"{cat.CategoriaID}: {cat.Nome}");
            }

            Console.Write("Escolha uma categoria pelo ID: ");
            int categoriaId = Convert.ToInt32(Console.ReadLine());
            var categoriaEscolhida = categoriasFiltradas.FirstOrDefault(c => c.CategoriaID == categoriaId);

            if (categoriaEscolhida == null)
            {
                Console.WriteLine("Categoria inválida.");
                return;
            }

            ExibirProdutosPorCategoria(categoriaEscolhida.Nome);
        }

        static void ExibirProdutosPorCategoria(string nomeCategoria)
        {
            ProdutoBLL produtoBLL = new ProdutoBLL();
            List<Produto> produtos = produtoBLL.ObterProdutos().Where(p => p.Categoria.Nome == nomeCategoria).ToList();

            Console.Clear();
            Console.WriteLine($"Produtos na categoria '{nomeCategoria}':");
            foreach (var produto in produtos)
            {
                Console.WriteLine($"ID: {produto.ProdutoId}, Nome: {produto.Nome}, Preço: {produto.Preco}");
            }

            Console.WriteLine("Pressione qualquer tecla para voltar.");
            Console.ReadKey();
        }

        static void ConsultarProdutosPorMarca()
        {
            MarcaBLL marcaBLL = new MarcaBLL();
            List<Marca> marcas = marcaBLL.ObterMarcas();

            Console.Clear();
            Console.WriteLine("Marcas Disponíveis:");
            foreach (var marca in marcas)
            {
                Console.WriteLine($"{marca.MarcaID}: {marca.Nome}");
            }

            Console.Write("Escolha uma marca pelo ID: ");
            int marcaId = Convert.ToInt32(Console.ReadLine());
            var marcaEscolhida = marcas.FirstOrDefault(m => m.MarcaID == marcaId);

            if (marcaEscolhida == null)
            {
                Console.WriteLine("Marca inválida.");
                return;
            }

            ExibirProdutosPorMarca(marcaEscolhida.Nome);
        }

        static void ExibirProdutosPorMarca(string nomeMarca)
        {
            ProdutoBLL produtoBLL = new ProdutoBLL();
            List<Produto> produtos = produtoBLL.ObterProdutos().Where(p => p.Marca.Nome == nomeMarca).ToList();

            Console.Clear();
            Console.WriteLine($"Produtos da marca '{nomeMarca}':");
            foreach (var produto in produtos)
            {
                Console.WriteLine($"ID: {produto.ProdutoId}, Nome: {produto.Nome}, Preço: {produto.Preco}");
            }

            Console.WriteLine("Pressione qualquer tecla para voltar.");
            Console.ReadKey();
        }

        static void ComprarProdutos(int clienteId)
        {
            CarrinhoBLL carrinhoBLL = new CarrinhoBLL();
            CarrinhoDeCompras carrinho = new CarrinhoDeCompras();
            VendaBLL vendaBLL = new VendaBLL();

            bool continuarCompra = true;
            while (continuarCompra)
            {
                Console.WriteLine("Digite o ID do produto para adicionar ao carrinho ou 0 para finalizar:");
                int produtoId = Convert.ToInt32(Console.ReadLine());
                if (produtoId == 0)
                {
                    break;
                }

                Console.WriteLine("Digite a quantidade do produto:");
                int quantidade = Convert.ToInt32(Console.ReadLine());

                // Adiciona o produto ao carrinho
                carrinhoBLL.AdicionarAoCarrinho(carrinho, produtoId, quantidade);
            }

            // Finaliza a compra
            Venda novaVenda = carrinhoBLL.FinalizarCompra(carrinho, clienteId);
            Console.WriteLine($"Compra finalizada com sucesso! ID da Venda: {novaVenda.VendaId}, Total: {novaVenda.Total}");
        }

        #endregion Métodos para Área de Clientes
        #endregion Área de Cliente

        #region Registo de Contas Cliente
        static void RegistarContaCliente()
        {
            Console.Clear();
            Console.WriteLine("Registar nova conta de cliente");

            ClienteBLL clienteBLL = new ClienteBLL(); // Instância única da variável clienteBLL
            List<Cliente> clientesExistentes = clienteBLL.ObterClientes();

            // Solicitar informações do cliente
            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            string email;
            do
            {
                Console.Write("Email: ");
                email = Console.ReadLine();
                if (clientesExistentes.Any(c => c.Email == email))
                {
                    Console.WriteLine("Este email já está registado. Por favor, tente outro.");
                }
            } while (clientesExistentes.Any(c => c.Email == email));

            Console.Write("Palavra-passe: ");
            string palavraPasse = Console.ReadLine();

            Console.Write("Contacto: ");
            string contacto = Console.ReadLine();

            Console.Write("Morada: ");
            string morada = Console.ReadLine();

            string nif;
            do
            {
                Console.Write("NIF: ");
                nif = Console.ReadLine();
                if (clientesExistentes.Any(c => c.NIF == nif))
                {
                    Console.WriteLine("Este NIF já está registado. Por favor, tente outro.");
                }
            } while (clientesExistentes.Any(c => c.NIF == nif));

            // Perguntar se é cliente particular ou empresarial
            Console.WriteLine("Tipo de cliente (1 para Particular, 2 para Empresarial): ");
            string tipoCliente = Console.ReadLine();

            if (tipoCliente == "1")
            {
                Console.Write("Cartão de Crédito: ");
                string cartaoCredito = Console.ReadLine();

                clienteBLL.AdicionarClienteParticular(nome, email, palavraPasse, contacto, morada, nif, cartaoCredito);
            }
            else if (tipoCliente == "2")
            {
                Console.Write("Nome do Responsável: ");
                string nomeResponsavel = Console.ReadLine();

                clienteBLL.AdicionarClienteEmpresarial(nome, email, palavraPasse, contacto, morada, nif, nomeResponsavel);
            }
            else
            {
                Console.WriteLine("Tipo de cliente inválido.");
                return;
            }

            Console.WriteLine("Conta de cliente registada com sucesso!");
        }
        #endregion Registo de Contas Cliente
    }
}