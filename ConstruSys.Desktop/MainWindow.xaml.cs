using ConstruSys.Application.Services;
using ConstruSys.Desktop.Views.Clientes;
using ConstruSys.Desktop.Views.Configuracoes;
using ConstruSys.Desktop.Views.Dashboard;
using ConstruSys.Desktop.Views.Estoque;
using ConstruSys.Desktop.Views.PDV;
using ConstruSys.Desktop.Views.Produtos;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ConstruSys.Desktop
{
    public partial class MainWindow : Window
    {
        private readonly ProdutoService
            _produtoService;

        private readonly ClienteService
            _clienteService;

        private readonly EstoqueService
            _estoqueService;

        private readonly CadastroProdutoAuxiliarService
            _cadastroProdutoAuxiliarService;

        private Button? _botaoSelecionado;

        public MainWindow(
            ProdutoService produtoService,
            ClienteService clienteService,
            EstoqueService estoqueService,
            CadastroProdutoAuxiliarService cadastroProdutoAuxiliarService)
        {
            InitializeComponent();

            _produtoService =
                produtoService;

            _clienteService =
                clienteService;

            _estoqueService =
                estoqueService;

            _cadastroProdutoAuxiliarService =
                cadastroProdutoAuxiliarService;

            AbrirDashboard();
        }

        private void MenuButton_Click(
                object sender,
                RoutedEventArgs e)
        {
            if (sender is not Button botao)
                return;

            string modulo =
                botao.Tag?.ToString()
                ?? string.Empty;

            switch (modulo)
            {
                case "Dashboard":

                    MainContent.Content =
                        new DashboardView();

                    break;

                case "PDV":

                    MainContent.Content =
                        new PdvView();

                    break;

                case "Vendas":

                    ModuloNaoImplementado(
                        "Vendas");

                    return;

                case "Orcamentos":

                    ModuloNaoImplementado(
                        "Orçamentos");

                    return;

                case "Produtos":

                    MainContent.Content =
                        new ProdutosView(
                            _produtoService,
                            _cadastroProdutoAuxiliarService);

                    break;

                case "Estoque":

                    MainContent.Content =
                        new EstoqueView(
                            _estoqueService,
                            _produtoService);

                    break;

                case "Compras":

                    ModuloNaoImplementado(
                        "Compras");

                    return;

                case "Clientes":

                    MainContent.Content =
                        new ClientesView(
                            _clienteService);

                    break;

                case "Fornecedores":

                    ModuloNaoImplementado(
                        "Fornecedores");

                    return;

                case "Caixa":

                    ModuloNaoImplementado(
                        "Caixa");

                    return;

                case "Financeiro":

                    ModuloNaoImplementado(
                        "Financeiro");

                    return;

                case "Relatorios":

                    ModuloNaoImplementado(
                        "Relatórios");

                    return;

                case "Lixeira":

                    MainContent.Content =
                        new LixeiraView(
                            _clienteService,
                            _produtoService);

                    break;

                case "Configuracoes":

                    ModuloNaoImplementado(
                        "Configurações");

                    return;

                default:

                    MessageBox.Show(
                        "Módulo não identificado.",
                        "ConstruSys",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                    return;
            }

            MarcarBotaoSelecionado(
                botao);
        }

        private void AbrirDashboard()
        {
            MainContent.Content =
                new DashboardView();

            MarcarBotaoSelecionado(
                BtnDashboard);
        }

        private void ModuloNaoImplementado(
            string modulo)
        {
            MessageBox.Show(
                $"O módulo {modulo} ainda será implementado.",
                "ConstruSys",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void MarcarBotaoSelecionado(
            Button botao)
        {
            if (_botaoSelecionado != null)
            {
                _botaoSelecionado.Background =
                    Brushes.Transparent;

                _botaoSelecionado.Foreground =
                    new SolidColorBrush(
                        Color.FromRgb(
                            203,
                            213,
                            225));
            }

            botao.Background =
                new SolidColorBrush(
                    Color.FromRgb(
                        30,
                        41,
                        59));

            botao.Foreground =
                Brushes.White;

            _botaoSelecionado =
                botao;
        }
    }
}