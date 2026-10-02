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

        private readonly CadastroProdutoAuxiliarService
            _cadastroAuxiliarService;

        private List<Produto> _produtos = new();

        public ProdutosView(
            ProdutoService produtoService,
            CadastroProdutoAuxiliarService cadastroAuxiliarService)
        {
            InitializeComponent();

            _produtoService =
                produtoService;

            _cadastroAuxiliarService =
                cadastroAuxiliarService;

            Loaded += ProdutosView_Loaded;
        }

        // =========================================================
        // CARREGAMENTO
        // =========================================================

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
                    $"Não foi possível carregar os produtos.\n\n{ex.Message}",
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        // =========================================================
        // INDICADORES
        // =========================================================

        private void AtualizarIndicadores()
        {
            int total =
                _produtos.Count;

            int ativos =
                _produtos.Count(
                    p => p.Ativo);

            int estoqueBaixo =
                _produtos.Count(
                    p =>
                        p.Ativo &&
                        p.EstoqueAtual > 0 &&
                        p.EstoqueAtual <= p.EstoqueMinimo);

            int semEstoque =
                _produtos.Count(
                    p =>
                        p.Ativo &&
                        p.EstoqueAtual <= 0);

            TxtTotalProdutos.Text =
                total.ToString();

            TxtProdutosAtivos.Text =
                ativos.ToString();

            TxtEstoqueBaixo.Text =
                estoqueBaixo.ToString();

            TxtSemEstoque.Text =
                semEstoque.ToString();
        }

        // =========================================================
        // FILTROS
        // =========================================================

        private void AplicarFiltros()
        {
            IEnumerable<Produto> consulta =
                _produtos;

            string termo =
                TxtPesquisa.Text?
                    .Trim()
                ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(
                termo))
            {
                consulta =
                    consulta.Where(
                        produto =>
                            produto.Codigo.Contains(
                                termo,
                                StringComparison.OrdinalIgnoreCase)

                            ||

                            produto.Nome.Contains(
                                termo,
                                StringComparison.OrdinalIgnoreCase)

                            ||

                            (!string.IsNullOrWhiteSpace(
                                    produto.CodigoBarras)
                                &&
                                produto.CodigoBarras.Contains(
                                    termo,
                                    StringComparison.OrdinalIgnoreCase))

                            ||

                            (!string.IsNullOrWhiteSpace(
                                    produto.Categoria)
                                &&
                                produto.Categoria.Contains(
                                    termo,
                                    StringComparison.OrdinalIgnoreCase))

                            ||

                            (!string.IsNullOrWhiteSpace(
                                    produto.Subcategoria)
                                &&
                                produto.Subcategoria.Contains(
                                    termo,
                                    StringComparison.OrdinalIgnoreCase))

                            ||

                            (!string.IsNullOrWhiteSpace(
                                    produto.Marca)
                                &&
                                produto.Marca.Contains(
                                    termo,
                                    StringComparison.OrdinalIgnoreCase))

                            ||

                            (!string.IsNullOrWhiteSpace(
                                    produto.LocalizacaoEstoque)
                                &&
                                produto.LocalizacaoEstoque.Contains(
                                    termo,
                                    StringComparison.OrdinalIgnoreCase)));
            }

            string filtro =
                (CmbFiltro.SelectedItem
                    as ComboBoxItem)?
                .Content?
                .ToString()
                ?? "Todos";

            switch (filtro)
            {
                case "Ativos":

                    consulta =
                        consulta.Where(
                            p => p.Ativo);

                    break;

                case "Inativos":

                    consulta =
                        consulta.Where(
                            p => !p.Ativo);

                    break;

                case "Estoque baixo":

                    consulta =
                        consulta.Where(
                            p =>
                                p.EstoqueAtual > 0 &&
                                p.EstoqueAtual <=
                                p.EstoqueMinimo);

                    break;

                case "Sem estoque":

                    consulta =
                        consulta.Where(
                            p =>
                                p.EstoqueAtual <= 0);

                    break;
            }

            DgProdutos.ItemsSource =
                consulta
                    .OrderBy(
                        p => p.Nome)
                    .ToList();
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

        // =========================================================
        // NOVO PRODUTO
        // =========================================================

        private async void NovoProduto_Click(
            object sender,
            RoutedEventArgs e)
        {
            JanelaProduto janela =
                new(
                    _produtoService,
                    _cadastroAuxiliarService);

            Window? owner =
                Window.GetWindow(this);

            if (owner != null)
            {
                janela.Owner =
                    owner;
            }

            bool? resultado =
                janela.ShowDialog();

            if (resultado == true)
            {
                await CarregarProdutosAsync();
            }
        }

        // =========================================================
        // VISUALIZAR
        // =========================================================

        private async void VisualizarProduto_Click(
            object sender,
            RoutedEventArgs e)
        {
            Produto? produto =
                ObterProdutoDoBotao(
                    sender);

            if (produto == null)
                return;

            try
            {
                Produto? produtoBanco =
                    await _produtoService
                        .ObterPorIdAsync(
                            produto.Id);

                if (produtoBanco == null)
                {
                    MessageBox.Show(
                        "O produto não foi encontrado.",
                        "ConstruSys",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                    await CarregarProdutosAsync();

                    return;
                }

                VisualizarProduto janela =
                    new(
                        produtoBanco);

                Window? owner =
                    Window.GetWindow(this);

                if (owner != null)
                {
                    janela.Owner =
                        owner;
                }

                janela.ShowDialog();
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

        // =========================================================
        // EDITAR
        // =========================================================

        private async void EditarProduto_Click(
            object sender,
            RoutedEventArgs e)
        {
            Produto? produto =
                ObterProdutoDoBotao(
                    sender);

            if (produto == null)
                return;

            try
            {
                Produto? produtoBanco =
                    await _produtoService
                        .ObterPorIdAsync(
                            produto.Id);

                if (produtoBanco == null)
                {
                    MessageBox.Show(
                        "O produto não foi encontrado.",
                        "ConstruSys",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                    await CarregarProdutosAsync();

                    return;
                }

                JanelaProduto janela =
                    new(
                        _produtoService,
                        _cadastroAuxiliarService,
                        produtoBanco);

                Window? owner =
                    Window.GetWindow(this);

                if (owner != null)
                {
                    janela.Owner =
                        owner;
                }

                bool? resultado =
                    janela.ShowDialog();

                if (resultado == true)
                {
                    await CarregarProdutosAsync();
                }
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

        // =========================================================
        // ATIVAR / INATIVAR
        // =========================================================

        private async void AlterarStatusProduto_Click(
            object sender,
            RoutedEventArgs e)
        {
            Produto? produto =
                ObterProdutoDoBotao(
                    sender);

            if (produto == null)
                return;

            bool novoStatus =
                !produto.Ativo;

            string acao =
                novoStatus
                    ? "ativar"
                    : "inativar";

            MessageBoxResult confirmacao =
                MessageBox.Show(
                    $"Deseja realmente {acao} o produto \"{produto.Nome}\"?",
                    "ConstruSys",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

            if (confirmacao !=
                MessageBoxResult.Yes)
            {
                return;
            }

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

        // =========================================================
        // EXCLUIR / LIXEIRA
        // =========================================================

        private async void ExcluirProduto_Click(
            object sender,
            RoutedEventArgs e)
        {
            Produto? produto =
                ObterProdutoDoBotao(
                    sender);

            if (produto == null)
                return;

            MessageBoxResult confirmacao =
                MessageBox.Show(
                    $"Deseja mover o produto \"{produto.Nome}\" para a lixeira?\n\n" +
                    "O produto poderá ser restaurado posteriormente.",
                    "Mover para a lixeira",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

            if (confirmacao !=
                MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                await _produtoService
                    .ExcluirAsync(
                        produto.Id);

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

        // =========================================================
        // AUXILIAR
        // =========================================================

        private static Produto? ObterProdutoDoBotao(
            object sender)
        {
            if (sender is Button button &&
                button.Tag is Produto produto)
            {
                return produto;
            }

            return null;
        }
    }
}