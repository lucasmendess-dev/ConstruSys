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
        private readonly ProdutoService _produtoService;
        private readonly ClienteService _clienteService;
        private readonly EstoqueService _estoqueService;

        private Button? _botaoSelecionado;

        public MainWindow(
            ProdutoService produtoService,
            ClienteService clienteService,
            EstoqueService estoqueService)
        {
            InitializeComponent();

            _produtoService =
                produtoService;

            _clienteService =
                clienteService;

            _estoqueService =
                estoqueService;

            MainContent.Content =
                new DashboardView();

            MarcarBotaoSelecionado(
                BtnDashboard);
        }

        private void MenuButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (sender is not Button button)
                return;

            string pagina =
                button.Tag?.ToString()
                ?? string.Empty;

            bool paginaValida =
                true;

            switch (pagina)
            {
                case "Dashboard":

                    MainContent.Content =
                        new DashboardView();

                    break;

                case "Produtos":

                    MainContent.Content =
                        new ProdutosView(
                            _produtoService);

                    break;

                case "Clientes":

                    MainContent.Content =
                        new ClientesView(
                            _clienteService);

                    break;

                case "Estoque":

                    MainContent.Content =
                        new EstoqueView(
                            _estoqueService,
                            _produtoService);

                    break;

                case "PDV":

                    MainContent.Content =
                        new PdvView();

                    break;

                case "Lixeira":

                    MainContent.Content =
                        new LixeiraView(
                            _clienteService,
                            _produtoService);

                    break;

                default:

                    paginaValida =
                        false;

                    MessageBox.Show(
                        "Este módulo ainda será implementado.",
                        "ConstruSys",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    break;
            }

            if (paginaValida)
            {
                MarcarBotaoSelecionado(
                    button);
            }
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

            _botaoSelecionado =
                botao;

            botao.Background =
                new SolidColorBrush(
                    Color.FromRgb(
                        30,
                        41,
                        59));

            botao.Foreground =
                Brushes.White;
        }
    }
}