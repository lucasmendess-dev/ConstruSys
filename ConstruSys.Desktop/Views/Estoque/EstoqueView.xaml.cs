using ConstruSys.Application.Services;
using ConstruSys.Domain.Entities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ConstruSys.Desktop.Views.Estoque
{
    public partial class EstoqueView : UserControl
    {
        private readonly EstoqueService _estoqueService;
        private readonly ProdutoService _produtoService;

        private List<Produto> _produtos =
            new();

        public EstoqueView(
            EstoqueService estoqueService,
            ProdutoService produtoService)
        {
            InitializeComponent();

            _estoqueService =
                estoqueService;

            _produtoService =
                produtoService;

            Loaded += EstoqueView_Loaded;
        }

        private async void EstoqueView_Loaded(
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

                _produtos =
                    _produtos
                        .Where(p => p.Ativo)
                        .ToList();

                AtualizarCards();

                AplicarFiltros();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao carregar estoque.\n\n{ex.Message}",
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void AtualizarCards()
        {
            TxtTotalProdutos.Text =
                _produtos.Count.ToString();

            TxtEstoqueNormal.Text =
                _produtos.Count(
                    p => p.EstoqueAtual >
                         p.EstoqueMinimo)
                .ToString();

            TxtEstoqueBaixo.Text =
                _produtos.Count(
                    p =>
                        p.EstoqueAtual > 0 &&
                        p.EstoqueAtual <=
                        p.EstoqueMinimo)
                .ToString();

            TxtSemEstoque.Text =
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

                        (p.Categoria ?? "")
                            .ToLower()
                            .Contains(termo));
            }

            string filtro =
                (CmbFiltro.SelectedItem
                    as ComboBoxItem)?
                .Content?
                .ToString()
                ?? "Todos";

            switch (filtro)
            {
                case "Normal":

                    resultado =
                        resultado.Where(
                            p =>
                                p.EstoqueAtual >
                                p.EstoqueMinimo);

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
                            p =>
                                p.EstoqueAtual <= 0);

                    break;
            }

            List<Produto> lista =
                resultado
                    .OrderBy(p => p.Nome)
                    .ToList();

            GridProdutos.ItemsSource =
                lista;

            TxtQuantidade.Text =
                $"{lista.Count} produto(s)";
        }

        private void NovaMovimentacao_Click(
            object sender,
            RoutedEventArgs e)
        {
            AbrirJanelaMovimentacao(
                null);
        }

        private void MovimentarProduto_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (sender is not Button button ||
                button.Tag is not Produto produto)
            {
                return;
            }

            AbrirJanelaMovimentacao(
                produto);
        }

        private void AbrirJanelaMovimentacao(
            Produto? produto)
        {
            JanelaMovimentacaoEstoque janela =
                new(
                    _estoqueService,
                    _produtoService,
                    produto);

            janela.Owner =
                Window.GetWindow(this);

            bool? resultado =
                janela.ShowDialog();

            if (resultado == true)
            {
                _ = CarregarProdutosAsync();
            }
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