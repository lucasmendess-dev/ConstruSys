using ConstruSys.Application.Services;
using ConstruSys.Domain.Entities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ConstruSys.Desktop.Views.Produtos
{
    public partial class ProdutosView : UserControl
    {
        private readonly ProdutoService _produtoService;

        public ProdutosView(
            ProdutoService produtoService)
        {
            InitializeComponent();

            _produtoService = produtoService;

            Loaded += ProdutosView_Loaded;
        }

        private async void ProdutosView_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            await CarregarProdutosAsync();
        }

        private async Task CarregarProdutosAsync()
        {
            try
            {
                List<Produto> produtos =
                    await _produtoService.ObterTodosAsync();

                GridProdutos.ItemsSource = produtos;

                TxtTotalProdutos.Text =
                    $"{produtos.Count} produto(s)";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao carregar produtos.\n\n{ex.Message}",
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private async Task PesquisarAsync()
        {
            try
            {
                string termo =
                    TxtPesquisa.Text.Trim();

                List<Produto> produtos =
                    await _produtoService
                        .PesquisarAsync(termo);

                GridProdutos.ItemsSource =
                    produtos;

                TxtTotalProdutos.Text =
                    $"{produtos.Count} produto(s)";
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

        private void NovoProduto_Click(
            object sender,
            RoutedEventArgs e)
        {
            JanelaProduto janela = new(
                _produtoService);

            janela.Owner =
                Window.GetWindow(this);

            bool? resultado =
                janela.ShowDialog();

            if (resultado == true)
                _ = CarregarProdutosAsync();
        }

        private async void EditarProduto_Click(
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
                    .ObterPorIdAsync(produto.Id);

            if (produtoBanco == null)
                return;

            JanelaProduto janela = new(
                _produtoService,
                produtoBanco);

            janela.Owner =
                Window.GetWindow(this);

            bool? resultado =
                janela.ShowDialog();

            if (resultado == true)
                await CarregarProdutosAsync();
        }

        private async void ExcluirProduto_Click(
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
                    $"Deseja realmente excluir o produto:\n\n{produto.Nome}?",
                    "Excluir Produto",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

            if (resultado != MessageBoxResult.Yes)
                return;

            try
            {
                await _produtoService
                    .ExcluirAsync(produto.Id);

                await CarregarProdutosAsync();
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

        private async void Pesquisar_Click(
            object sender,
            RoutedEventArgs e)
        {
            await PesquisarAsync();
        }

        private async void TxtPesquisa_KeyUp(
            object sender,
            KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                await PesquisarAsync();

            if (string.IsNullOrWhiteSpace(
                TxtPesquisa.Text))
            {
                await CarregarProdutosAsync();
            }
        }
    }
}