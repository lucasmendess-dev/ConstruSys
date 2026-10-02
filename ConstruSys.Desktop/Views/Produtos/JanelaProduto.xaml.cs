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

        private readonly CadastroProdutoAuxiliarService
            _cadastroAuxiliarService;

        private readonly Produto? _produtoEdicao;

        public JanelaProduto(
            ProdutoService produtoService,
            CadastroProdutoAuxiliarService cadastroAuxiliarService,
            Produto? produto = null)
        {
            InitializeComponent();

            _produtoService =
                produtoService;

            _cadastroAuxiliarService =
                cadastroAuxiliarService;

            _produtoEdicao =
                produto;

            CmbUnidade.SelectedIndex =
                0;

            Loaded +=
                JanelaProduto_Loaded;
        }

        private async void JanelaProduto_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            try
            {
                await CarregarCadastrosAuxiliaresAsync();

                if (_produtoEdicao != null)
                {
                    CarregarProduto();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erro ao carregar o formulário.\n\n{ex.Message}",
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private async Task CarregarCadastrosAuxiliaresAsync()
        {
            List<CategoriaProduto> categorias =
                await _cadastroAuxiliarService
                    .ObterCategoriasAsync();

            List<MarcaProduto> marcas =
                await _cadastroAuxiliarService
                    .ObterMarcasAsync();

            List<LocalizacaoEstoqueCadastro> localizacoes =
                await _cadastroAuxiliarService
                    .ObterLocalizacoesAsync();

            CmbCategoria.ItemsSource =
                categorias;

            CmbMarca.ItemsSource =
                marcas;

            CmbLocalizacao.ItemsSource =
                localizacoes;
        }

        private void CarregarProduto()
        {
            if (_produtoEdicao == null)
                return;

            TxtTitulo.Text =
                "Editar Produto";

            TxtCodigo.Text =
                _produtoEdicao.Codigo;

            TxtCodigoBarras.Text =
                _produtoEdicao.CodigoBarras;

            TxtNome.Text =
                _produtoEdicao.Nome;

            TxtSubcategoria.Text =
                _produtoEdicao.Subcategoria;

            TxtPeso.Text =
                _produtoEdicao.Peso?
                    .ToString("0.###");

            TxtPrecoCusto.Text =
                _produtoEdicao.PrecoCusto
                    .ToString("0.00");

            TxtPrecoVenda.Text =
                _produtoEdicao.PrecoVenda
                    .ToString("0.00");

            TxtEstoqueAtual.Text =
                _produtoEdicao.EstoqueAtual
                    .ToString("0.###");

            TxtEstoqueMinimo.Text =
                _produtoEdicao.EstoqueMinimo
                    .ToString("0.###");

            TxtEstoqueMaximo.Text =
                _produtoEdicao.EstoqueMaximo?
                    .ToString("0.###");

            ChkAtivo.IsChecked =
                _produtoEdicao.Ativo;

            SelecionarCategoria(
                _produtoEdicao.Categoria);

            SelecionarMarca(
                _produtoEdicao.Marca);

            SelecionarLocalizacao(
                _produtoEdicao.LocalizacaoEstoque);

            foreach (ComboBoxItem item
                     in CmbUnidade.Items)
            {
                if (item.Content?.ToString() ==
                    _produtoEdicao.UnidadeMedida)
                {
                    CmbUnidade.SelectedItem =
                        item;

                    break;
                }
            }
        }

        private void SelecionarCategoria(
            string? nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return;

            CategoriaProduto? categoria =
                CmbCategoria.Items
                    .Cast<CategoriaProduto>()
                    .FirstOrDefault(
                        x => x.Nome == nome);

            CmbCategoria.SelectedItem =
                categoria;
        }

        private void SelecionarMarca(
            string? nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return;

            MarcaProduto? marca =
                CmbMarca.Items
                    .Cast<MarcaProduto>()
                    .FirstOrDefault(
                        x => x.Nome == nome);

            CmbMarca.SelectedItem =
                marca;
        }

        private void SelecionarLocalizacao(
            string? nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return;

            LocalizacaoEstoqueCadastro? localizacao =
                CmbLocalizacao.Items
                    .Cast<LocalizacaoEstoqueCadastro>()
                    .FirstOrDefault(
                        x => x.Nome == nome);

            CmbLocalizacao.SelectedItem =
                localizacao;
        }

        private async void NovaCategoria_Click(
            object sender,
            RoutedEventArgs e)
        {
            JanelaCadastroAuxiliar janela =
                new(
                    "Nova Categoria",
                    "Informe o nome da nova categoria.");

            janela.Owner =
                this;

            if (janela.ShowDialog() != true)
                return;

            try
            {
                CategoriaProduto categoria =
                    await _cadastroAuxiliarService
                        .AdicionarCategoriaAsync(
                            janela.ValorDigitado);

                await CarregarCadastrosAuxiliaresAsync();

                SelecionarCategoria(
                    categoria.Nome);
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

        private async void NovaMarca_Click(
            object sender,
            RoutedEventArgs e)
        {
            JanelaCadastroAuxiliar janela =
                new(
                    "Nova Marca",
                    "Informe o nome da nova marca.");

            janela.Owner =
                this;

            if (janela.ShowDialog() != true)
                return;

            try
            {
                MarcaProduto marca =
                    await _cadastroAuxiliarService
                        .AdicionarMarcaAsync(
                            janela.ValorDigitado);

                await CarregarCadastrosAuxiliaresAsync();

                SelecionarMarca(
                    marca.Nome);
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

        private async void NovaLocalizacao_Click(
            object sender,
            RoutedEventArgs e)
        {
            JanelaCadastroAuxiliar janela =
                new(
                    "Nova Localização",
                    "Informe a nova localização de estoque.");

            janela.Owner =
                this;

            if (janela.ShowDialog() != true)
                return;

            try
            {
                LocalizacaoEstoqueCadastro localizacao =
                    await _cadastroAuxiliarService
                        .AdicionarLocalizacaoAsync(
                            janela.ValorDigitado);

                await CarregarCadastrosAuxiliaresAsync();

                SelecionarLocalizacao(
                    localizacao.Nome);
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
                    precoCusto = 0;
                }

                if (!decimal.TryParse(
                    TxtPrecoVenda.Text,
                    NumberStyles.Number,
                    CultureInfo.CurrentCulture,
                    out decimal precoVenda))
                {
                    precoVenda = 0;
                }

                if (!decimal.TryParse(
                    TxtEstoqueAtual.Text,
                    NumberStyles.Number,
                    CultureInfo.CurrentCulture,
                    out decimal estoqueAtual))
                {
                    estoqueAtual = 0;
                }

                if (!decimal.TryParse(
                    TxtEstoqueMinimo.Text,
                    NumberStyles.Number,
                    CultureInfo.CurrentCulture,
                    out decimal estoqueMinimo))
                {
                    estoqueMinimo = 0;
                }

                decimal? estoqueMaximo = null;

                if (!string.IsNullOrWhiteSpace(
                    TxtEstoqueMaximo.Text))
                {
                    if (!decimal.TryParse(
                        TxtEstoqueMaximo.Text,
                        NumberStyles.Number,
                        CultureInfo.CurrentCulture,
                        out decimal valorMaximo))
                    {
                        MessageBox.Show(
                            "Informe um estoque máximo válido.");

                        return;
                    }

                    estoqueMaximo =
                        valorMaximo;
                }

                decimal? peso = null;

                if (!string.IsNullOrWhiteSpace(
                    TxtPeso.Text))
                {
                    if (!decimal.TryParse(
                        TxtPeso.Text,
                        NumberStyles.Number,
                        CultureInfo.CurrentCulture,
                        out decimal valorPeso))
                    {
                        MessageBox.Show(
                            "Informe um peso válido.");

                        return;
                    }

                    peso =
                        valorPeso;
                }

                string unidade =
                    (CmbUnidade.SelectedItem
                        as ComboBoxItem)?
                    .Content?
                    .ToString()
                    ?? "UN";

                string? categoria =
                    (CmbCategoria.SelectedItem
                        as CategoriaProduto)?
                    .Nome;

                string? marca =
                    (CmbMarca.SelectedItem
                        as MarcaProduto)?
                    .Nome;

                string? localizacao =
                    (CmbLocalizacao.SelectedItem
                        as LocalizacaoEstoqueCadastro)?
                    .Nome;

                if (_produtoEdicao == null)
                {
                    Produto produto =
                        new()
                        {
                            Codigo =
                                TxtCodigo.Text,

                            CodigoBarras =
                                TextoOuNull(
                                    TxtCodigoBarras.Text),

                            Nome =
                                TxtNome.Text,

                            Categoria =
                                categoria,

                            Subcategoria =
                                TextoOuNull(
                                    TxtSubcategoria.Text),

                            Marca =
                                marca,

                            UnidadeMedida =
                                unidade,

                            LocalizacaoEstoque =
                                localizacao,

                            Peso =
                                peso,

                            PrecoCusto =
                                precoCusto,

                            PrecoVenda =
                                precoVenda,

                            EstoqueAtual =
                                estoqueAtual,

                            EstoqueMinimo =
                                estoqueMinimo,

                            EstoqueMaximo =
                                estoqueMaximo,

                            Ativo =
                                ChkAtivo.IsChecked == true
                        };

                    await _produtoService
                        .AdicionarAsync(produto);
                }
                else
                {
                    _produtoEdicao.Codigo =
                        TxtCodigo.Text;

                    _produtoEdicao.CodigoBarras =
                        TextoOuNull(
                            TxtCodigoBarras.Text);

                    _produtoEdicao.Nome =
                        TxtNome.Text;

                    _produtoEdicao.Categoria =
                        categoria;

                    _produtoEdicao.Subcategoria =
                        TextoOuNull(
                            TxtSubcategoria.Text);

                    _produtoEdicao.Marca =
                        marca;

                    _produtoEdicao.UnidadeMedida =
                        unidade;

                    _produtoEdicao.LocalizacaoEstoque =
                        localizacao;

                    _produtoEdicao.Peso =
                        peso;

                    _produtoEdicao.PrecoCusto =
                        precoCusto;

                    _produtoEdicao.PrecoVenda =
                        precoVenda;

                    _produtoEdicao.EstoqueAtual =
                        estoqueAtual;

                    _produtoEdicao.EstoqueMinimo =
                        estoqueMinimo;

                    _produtoEdicao.EstoqueMaximo =
                        estoqueMaximo;

                    _produtoEdicao.Ativo =
                        ChkAtivo.IsChecked == true;

                    await _produtoService
                        .AtualizarAsync(
                            _produtoEdicao);
                }

                DialogResult =
                    true;

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

        private static string? TextoOuNull(
            string texto)
        {
            return string.IsNullOrWhiteSpace(texto)
                ? null
                : texto.Trim();
        }

        private void Cancelar_Click(
            object sender,
            RoutedEventArgs e)
        {
            DialogResult =
                false;

            Close();
        }
    }
}