using ConstruSys.Application.Services;
using ConstruSys.Domain.Entities;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace ConstruSys.Desktop.Views.Produtos
{
    public partial class JanelaProduto : Window
    {
        private readonly ProdutoService _produtoService;

        private readonly Produto? _produtoEdicao;

        public JanelaProduto(
            ProdutoService produtoService,
            Produto? produto = null)
        {
            InitializeComponent();

            _produtoService = produtoService;
            _produtoEdicao = produto;

            CmbUnidade.SelectedIndex = 0;

            if (_produtoEdicao != null)
                CarregarProduto();
        }

        private void CarregarProduto()
        {
            TxtTitulo.Text = "Editar Produto";

            TxtCodigo.Text = _produtoEdicao!.Codigo;
            TxtCodigoBarras.Text = _produtoEdicao.CodigoBarras;
            TxtNome.Text = _produtoEdicao.Nome;
            TxtCategoria.Text = _produtoEdicao.Categoria;
            TxtMarca.Text = _produtoEdicao.Marca;
            TxtLocalizacao.Text = _produtoEdicao.LocalizacaoEstoque;

            TxtPrecoCusto.Text =
                _produtoEdicao.PrecoCusto.ToString("N2");

            TxtPrecoVenda.Text =
                _produtoEdicao.PrecoVenda.ToString("N2");

            TxtEstoqueAtual.Text =
                _produtoEdicao.EstoqueAtual.ToString("N3");

            TxtEstoqueMinimo.Text =
                _produtoEdicao.EstoqueMinimo.ToString("N3");

            ChkAtivo.IsChecked = _produtoEdicao.Ativo;

            foreach (ComboBoxItem item in CmbUnidade.Items)
            {
                if (item.Content?.ToString() ==
                    _produtoEdicao.UnidadeMedida)
                {
                    CmbUnidade.SelectedItem = item;
                    break;
                }
            }
        }

        private async void Salvar_Click(
            object sender,
            RoutedEventArgs e)
        {
            try
            {
                if (!decimal.TryParse(
                    TxtPrecoCusto.Text,
                    NumberStyles.Number,
                    CultureInfo.CurrentCulture,
                    out decimal precoCusto))
                {
                    MessageBox.Show(
                        "Informe um preço de custo válido.");

                    return;
                }

                if (!decimal.TryParse(
                    TxtPrecoVenda.Text,
                    NumberStyles.Number,
                    CultureInfo.CurrentCulture,
                    out decimal precoVenda))
                {
                    MessageBox.Show(
                        "Informe um preço de venda válido.");

                    return;
                }

                if (!decimal.TryParse(
                    TxtEstoqueAtual.Text,
                    NumberStyles.Number,
                    CultureInfo.CurrentCulture,
                    out decimal estoqueAtual))
                {
                    MessageBox.Show(
                        "Informe um estoque atual válido.");

                    return;
                }

                if (!decimal.TryParse(
                    TxtEstoqueMinimo.Text,
                    NumberStyles.Number,
                    CultureInfo.CurrentCulture,
                    out decimal estoqueMinimo))
                {
                    MessageBox.Show(
                        "Informe um estoque mínimo válido.");

                    return;
                }

                string unidade =
                    (CmbUnidade.SelectedItem as ComboBoxItem)?
                    .Content?
                    .ToString() ?? "UN";

                if (_produtoEdicao == null)
                {
                    Produto produto = new()
                    {
                        Codigo = TxtCodigo.Text.Trim(),

                        CodigoBarras =
                            string.IsNullOrWhiteSpace(
                                TxtCodigoBarras.Text)
                                ? null
                                : TxtCodigoBarras.Text.Trim(),

                        Nome = TxtNome.Text.Trim(),

                        Categoria =
                            string.IsNullOrWhiteSpace(
                                TxtCategoria.Text)
                                ? null
                                : TxtCategoria.Text.Trim(),

                        Marca =
                            string.IsNullOrWhiteSpace(
                                TxtMarca.Text)
                                ? null
                                : TxtMarca.Text.Trim(),

                        UnidadeMedida = unidade,

                        LocalizacaoEstoque =
                            string.IsNullOrWhiteSpace(
                                TxtLocalizacao.Text)
                                ? null
                                : TxtLocalizacao.Text.Trim(),

                        PrecoCusto = precoCusto,
                        PrecoVenda = precoVenda,
                        EstoqueAtual = estoqueAtual,
                        EstoqueMinimo = estoqueMinimo,
                        Ativo = ChkAtivo.IsChecked == true
                    };

                    await _produtoService.AdicionarAsync(produto);
                }
                else
                {
                    _produtoEdicao.Codigo =
                        TxtCodigo.Text.Trim();

                    _produtoEdicao.CodigoBarras =
                        string.IsNullOrWhiteSpace(
                            TxtCodigoBarras.Text)
                            ? null
                            : TxtCodigoBarras.Text.Trim();

                    _produtoEdicao.Nome =
                        TxtNome.Text.Trim();

                    _produtoEdicao.Categoria =
                        string.IsNullOrWhiteSpace(
                            TxtCategoria.Text)
                            ? null
                            : TxtCategoria.Text.Trim();

                    _produtoEdicao.Marca =
                        string.IsNullOrWhiteSpace(
                            TxtMarca.Text)
                            ? null
                            : TxtMarca.Text.Trim();

                    _produtoEdicao.UnidadeMedida =
                        unidade;

                    _produtoEdicao.LocalizacaoEstoque =
                        string.IsNullOrWhiteSpace(
                            TxtLocalizacao.Text)
                            ? null
                            : TxtLocalizacao.Text.Trim();

                    _produtoEdicao.PrecoCusto =
                        precoCusto;

                    _produtoEdicao.PrecoVenda =
                        precoVenda;

                    _produtoEdicao.EstoqueAtual =
                        estoqueAtual;

                    _produtoEdicao.EstoqueMinimo =
                        estoqueMinimo;

                    _produtoEdicao.Ativo =
                        ChkAtivo.IsChecked == true;

                    await _produtoService
                        .AtualizarAsync(_produtoEdicao);
                }

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