using ConstruSys.Application.Services;
using ConstruSys.Domain.Entities;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace ConstruSys.Desktop.Views.Estoque
{
    public partial class JanelaMovimentacaoEstoque : Window
    {
        private readonly EstoqueService _estoqueService;
        private readonly ProdutoService _produtoService;

        private readonly Produto? _produtoSelecionadoInicial;

        private List<Produto> _produtos = new();

        public JanelaMovimentacaoEstoque(
            EstoqueService estoqueService,
            ProdutoService produtoService,
            Produto? produtoSelecionado = null)
        {
            InitializeComponent();

            _estoqueService = estoqueService;
            _produtoService = produtoService;

            _produtoSelecionadoInicial =
                produtoSelecionado;

            // Somente selecionamos depois que todos
            // os controles do XAML já foram criados.
            CmbTipo.SelectedIndex = 0;

            Loaded += JanelaMovimentacaoEstoque_Loaded;
        }

        private async void JanelaMovimentacaoEstoque_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            try
            {
                await CarregarProdutosAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Não foi possível carregar os produtos.\n\n{ex.Message}",
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                Close();
            }
        }

        private async Task CarregarProdutosAsync()
        {
            List<Produto> produtosBanco =
                await _produtoService
                    .ObterTodosAsync();

            _produtos =
                produtosBanco
                    .Where(p => p.Ativo)
                    .OrderBy(p => p.Nome)
                    .ToList();

            CmbProduto.ItemsSource =
                _produtos;

            if (_produtos.Count == 0)
            {
                CmbProduto.SelectedIndex = -1;

                TxtEstoqueAtual.Text =
                    "Nenhum produto ativo";

                return;
            }

            if (_produtoSelecionadoInicial != null)
            {
                Produto? produtoEncontrado =
                    _produtos.FirstOrDefault(
                        p =>
                            p.Id ==
                            _produtoSelecionadoInicial.Id);

                if (produtoEncontrado != null)
                {
                    CmbProduto.SelectedItem =
                        produtoEncontrado;

                    return;
                }
            }

            CmbProduto.SelectedIndex = 0;
        }

        private void CmbProduto_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            // Durante a criação da janela este evento
            // pode ocorrer antes do carregamento completo.
            if (!IsLoaded)
                return;

            AtualizarEstoqueAtual();
        }

        private void AtualizarEstoqueAtual()
        {
            if (TxtEstoqueAtual == null)
                return;

            if (CmbProduto?.SelectedItem
                is Produto produto)
            {
                TxtEstoqueAtual.Text =
                    $"{produto.EstoqueAtual:0.###} {produto.UnidadeMedida}";
            }
            else
            {
                TxtEstoqueAtual.Text =
                    "0,000";
            }
        }

        private void CmbTipo_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            // O SelectionChanged pode disparar durante
            // o InitializeComponent().
            if (TxtLabelQuantidade == null)
                return;

            if (CmbTipo?.SelectedItem
                is not ComboBoxItem item)
            {
                return;
            }

            string tipo =
                item.Content?.ToString()
                ?? "Entrada";

            if (tipo == "Ajuste")
            {
                TxtLabelQuantidade.Text =
                    "Novo saldo";
            }
            else
            {
                TxtLabelQuantidade.Text =
                    "Quantidade";
            }
        }

        private async void Registrar_Click(
            object sender,
            RoutedEventArgs e)
        {
            try
            {
                if (CmbProduto.SelectedItem
                    is not Produto produto)
                {
                    MessageBox.Show(
                        "Selecione um produto.",
                        "ConstruSys",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                    return;
                }

                if (CmbTipo.SelectedItem
                    is not ComboBoxItem itemTipo)
                {
                    MessageBox.Show(
                        "Selecione o tipo da movimentação.",
                        "ConstruSys",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                    return;
                }

                if (string.IsNullOrWhiteSpace(
                    TxtQuantidade.Text))
                {
                    MessageBox.Show(
                        "Informe a quantidade.",
                        "ConstruSys",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                    TxtQuantidade.Focus();

                    return;
                }

                if (!decimal.TryParse(
                    TxtQuantidade.Text,
                    NumberStyles.Number,
                    CultureInfo.CurrentCulture,
                    out decimal quantidade))
                {
                    MessageBox.Show(
                        "Informe uma quantidade válida.",
                        "ConstruSys",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                    TxtQuantidade.Focus();

                    return;
                }

                string tipo =
                    itemTipo.Content?
                        .ToString()
                    ?? "Entrada";

                string? observacao =
                    string.IsNullOrWhiteSpace(
                        TxtObservacao.Text)
                        ? null
                        : TxtObservacao.Text.Trim();

                switch (tipo)
                {
                    case "Entrada":

                        if (quantidade <= 0)
                        {
                            MessageBox.Show(
                                "A quantidade de entrada deve ser maior que zero.",
                                "ConstruSys",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);

                            return;
                        }

                        await _estoqueService
                            .RegistrarEntradaAsync(
                                produto.Id,
                                quantidade,
                                observacao);

                        break;

                    case "Saída":

                        if (quantidade <= 0)
                        {
                            MessageBox.Show(
                                "A quantidade de saída deve ser maior que zero.",
                                "ConstruSys",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);

                            return;
                        }

                        await _estoqueService
                            .RegistrarSaidaAsync(
                                produto.Id,
                                quantidade,
                                observacao);

                        break;

                    case "Ajuste":

                        if (quantidade < 0)
                        {
                            MessageBox.Show(
                                "O novo saldo não pode ser negativo.",
                                "ConstruSys",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);

                            return;
                        }

                        await _estoqueService
                            .RegistrarAjusteAsync(
                                produto.Id,
                                quantidade,
                                observacao);

                        break;

                    default:

                        MessageBox.Show(
                            "Tipo de movimentação inválido.",
                            "ConstruSys",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);

                        return;
                }

                MessageBox.Show(
                    "Movimentação registrada com sucesso.",
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                DialogResult = true;

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void Cancelar_Click(
            object sender,
            RoutedEventArgs e)
        {
            DialogResult = false;

            Close();
        }
    }
}