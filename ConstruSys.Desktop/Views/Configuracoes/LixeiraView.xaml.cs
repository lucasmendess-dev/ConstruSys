using ConstruSys.Application.Services;
using ConstruSys.Domain.Entities;
using ConstruSys.Desktop.Views.Clientes;
using ConstruSys.Desktop.Views.Produtos;
using System.Windows;
using System.Windows.Controls;

namespace ConstruSys.Desktop.Views.Configuracoes
{
    public partial class LixeiraView : UserControl
    {
        private readonly ClienteService _clienteService;
        private readonly ProdutoService _produtoService;

        public LixeiraView(
            ClienteService clienteService,
            ProdutoService produtoService)
        {
            InitializeComponent();

            _clienteService = clienteService;
            _produtoService = produtoService;

            Loaded += LixeiraView_Loaded;
        }

        private async void LixeiraView_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            await CarregarTudoAsync();
        }

        private async Task CarregarTudoAsync()
        {
            await CarregarClientesAsync();
            await CarregarProdutosAsync();
        }

        private async Task CarregarClientesAsync()
        {
            try
            {
                List<Cliente> clientes =
                    await _clienteService
                        .ObterExcluidosAsync();

                GridClientesExcluidos.ItemsSource =
                    clientes;

                TxtTotalClientesExcluidos.Text =
                    $"{clientes.Count} cliente(s)";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao carregar clientes excluídos.\n\n{ex.Message}",
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private async Task CarregarProdutosAsync()
        {
            try
            {
                List<Produto> produtos =
                    await _produtoService
                        .ObterExcluidosAsync();

                GridProdutosExcluidos.ItemsSource =
                    produtos;

                TxtTotalProdutosExcluidos.Text =
                    $"{produtos.Count} produto(s)";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao carregar produtos excluídos.\n\n{ex.Message}",
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private async void RestaurarCliente_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (sender is not Button button ||
                button.Tag is not Cliente cliente)
            {
                return;
            }

            MessageBoxResult resultado =
                MessageBox.Show(
                    $"Deseja restaurar o cliente:\n\n{cliente.NomeRazaoSocial}?\n\n" +
                    "O cadastro será restaurado como inativo.",
                    "Restaurar Cliente",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

            if (resultado != MessageBoxResult.Yes)
                return;

            try
            {
                await _clienteService
                    .RestaurarAsync(cliente.Id);

                await CarregarClientesAsync();

                MessageBox.Show(
                    "Cliente restaurado com sucesso.",
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private async void RestaurarProduto_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (sender is not Button button ||
                button.Tag is not Produto produto)
            {
                return;
            }

            MessageBoxResult resultado =
                MessageBox.Show(
                    $"Deseja restaurar o produto:\n\n{produto.Nome}?\n\n" +
                    "O cadastro será restaurado como inativo.",
                    "Restaurar Produto",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

            if (resultado != MessageBoxResult.Yes)
                return;

            try
            {
                await _produtoService
                    .RestaurarAsync(produto.Id);

                await CarregarProdutosAsync();

                MessageBox.Show(
                    "Produto restaurado com sucesso.",
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private async void VisualizarClienteExcluido_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (sender is not Button button ||
                button.Tag is not Cliente cliente)
            {
                return;
            }

            Cliente? clienteBanco =
                await _clienteService
                    .ObterExcluidoPorIdAsync(cliente.Id);

            if (clienteBanco == null)
            {
                MessageBox.Show(
                    "Cliente excluído não encontrado.",
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            VisualizarCliente janela =
                new(clienteBanco);

            janela.Owner =
                Window.GetWindow(this);

            janela.ShowDialog();
        }

        private async void VisualizarProdutoExcluido_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (sender is not Button button ||
                button.Tag is not Produto produto)
            {
                return;
            }

            Produto? produtoBanco =
                await _produtoService
                    .ObterExcluidoPorIdAsync(produto.Id);

            if (produtoBanco == null)
            {
                MessageBox.Show(
                    "Produto excluído não encontrado.",
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            VisualizarProduto janela =
                new(produtoBanco);

            janela.Owner =
                Window.GetWindow(this);

            janela.ShowDialog();
        }

        private async void ExcluirClienteDefinitivamente_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (sender is not Button button ||
                button.Tag is not Cliente cliente)
            {
                return;
            }

            MessageBoxResult primeiraConfirmacao =
                MessageBox.Show(
                    $"ATENÇÃO\n\n" +
                    $"Deseja excluir definitivamente o cliente:\n\n" +
                    $"{cliente.NomeRazaoSocial}?\n\n" +
                    "Esta operação não poderá ser desfeita.",
                    "Exclusão Definitiva",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

            if (primeiraConfirmacao != MessageBoxResult.Yes)
                return;

            MessageBoxResult segundaConfirmacao =
                MessageBox.Show(
                    "Confirma novamente a exclusão definitiva?\n\n" +
                    "O registro será removido fisicamente do banco de dados.",
                    "Confirmação Final",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Stop);

            if (segundaConfirmacao != MessageBoxResult.Yes)
                return;

            try
            {
                await _clienteService
                    .ExcluirDefinitivamenteAsync(cliente.Id);

                await CarregarClientesAsync();

                MessageBox.Show(
                    "Cliente excluído definitivamente.",
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Não foi possível excluir definitivamente.\n\n{ex.Message}",
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private async void ExcluirProdutoDefinitivamente_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (sender is not Button button ||
                button.Tag is not Produto produto)
            {
                return;
            }

            MessageBoxResult primeiraConfirmacao =
                MessageBox.Show(
                    $"ATENÇÃO\n\n" +
                    $"Deseja excluir definitivamente o produto:\n\n" +
                    $"{produto.Nome}?\n\n" +
                    "Esta operação não poderá ser desfeita.",
                    "Exclusão Definitiva",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

            if (primeiraConfirmacao != MessageBoxResult.Yes)
                return;

            MessageBoxResult segundaConfirmacao =
                MessageBox.Show(
                    "Confirma novamente a exclusão definitiva?\n\n" +
                    "O registro será removido fisicamente do banco de dados.",
                    "Confirmação Final",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Stop);

            if (segundaConfirmacao != MessageBoxResult.Yes)
                return;

            try
            {
                await _produtoService
                    .ExcluirDefinitivamenteAsync(produto.Id);

                await CarregarProdutosAsync();

                MessageBox.Show(
                    "Produto excluído definitivamente.",
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Não foi possível excluir definitivamente.\n\n{ex.Message}",
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}