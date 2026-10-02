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

        private List<Produto> _produtos = new();

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
                _produtos =
                    await _produtoService
                        .ObterTodosAsync();

                AtualizarIndicadores();

                AplicarFiltros();
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

        private void AtualizarIndicadores()
        {
            TxtCardTotal.Text =
                _produtos.Count.ToString();

            TxtCardAtivos.Text =
                _produtos.Count(
                    p => p.Ativo)
                .ToString();

            TxtCardEstoqueBaixo.Text =
                _produtos.Count(
                    p =>
                        p.EstoqueAtual > 0 &&
                        p.EstoqueAtual <=
                        p.EstoqueMinimo)
                .ToString();

            TxtCardSemEstoque.Text =
                _produtos.Count(
                    p => p.EstoqueAtual <= 0)
                .ToString();
        }

        private void AplicarFiltros()
        {
            IEnumerable<Produto> resultado =
                _produtos;

            string termo =
                TxtPesquisa.Text
                    .Trim()
                    .ToLower();

            if (!string.IsNullOrWhiteSpace(termo))
            {
                resultado =
                    resultado.Where(p =>
                        p.Nome
                            .ToLower()
                            .Contains(termo) ||

                        p.Codigo
                            .ToLower()
                            .Contains(termo) ||

                        (p.CodigoBarras ?? "")
                            .ToLower()
                            .Contains(termo) ||

                        (p.Categoria ?? "")
                            .ToLower()
                            .Contains(termo) ||

                        (p.Marca ?? "")
                            .ToLower()
                            .Contains(termo));
            }

            string filtro =
                (CmbFiltro.SelectedItem as ComboBoxItem)?
                .Content?
                .ToString()
                ?? "Todos";

            switch (filtro)
            {
                case "Ativos":

                    resultado =
                        resultado.Where(
                            p => p.Ativo);

                    break;

                case "Inativos":

                    resultado =
                        resultado.Where(
                            p => !p.Ativo);

                    break;

                case "Estoque baixo":

                    resultado =
                        resultado.Where(
                            p =>
                                p.EstoqueAtual > 0 &&
                                p.EstoqueAtual <=
                                p.EstoqueMinimo);

                    break;

                case "Sem estoque":

                    resultado =
                        resultado.Where(
                            p => p.EstoqueAtual <= 0);

                    break;
            }

            List<Produto> lista =
                resultado
                    .OrderBy(p => p.Nome)
                    .ToList();

            GridProdutos.ItemsSource = lista;

            TxtTotalProdutos.Text =
                $"{lista.Count} produto(s)";
        }

        private void NovoProduto_Click(
            object sender,
            RoutedEventArgs e)
        {
            JanelaProduto janela =
                new(_produtoService);

            janela.Owner =
                Window.GetWindow(this);

            bool? resultado =
                janela.ShowDialog();

            if (resultado == true)
            {
                _ = CarregarProdutosAsync();
            }
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
            {
                MessageBox.Show(
                    "Produto não encontrado.",
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            JanelaProduto janela =
                new(
                    _produtoService,
                    produtoBanco);

            janela.Owner =
                Window.GetWindow(this);

            bool? resultado =
                janela.ShowDialog();

            if (resultado == true)
            {
                await CarregarProdutosAsync();
            }
        }

        private async void AlterarStatusProduto_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (sender is not Button button ||
                button.Tag is not Produto produto)
            {
                return;
            }

            bool novoStatus =
                !produto.Ativo;

            string acao =
                novoStatus
                    ? "ativar"
                    : "inativar";

            MessageBoxResult confirmacao =
                MessageBox.Show(
                    $"Deseja realmente {acao} o produto:\n\n{produto.Nome}?",
                    "ConstruSys",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

            if (confirmacao != MessageBoxResult.Yes)
                return;

            try
            {
                await _produtoService
                    .AlterarStatusAsync(
                        produto.Id,
                        novoStatus);

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

        private void Pesquisar_Click(
            object sender,
            RoutedEventArgs e)
        {
            AplicarFiltros();
        }

        private void TxtPesquisa_KeyUp(
            object sender,
            KeyEventArgs e)
        {
            AplicarFiltros();
        }

        private void CmbFiltro_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            if (!IsLoaded)
                return;

            AplicarFiltros();
        }
    }
}