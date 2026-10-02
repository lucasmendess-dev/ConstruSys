using ConstruSys.Domain.Entities;
using System.Windows;
using System.Windows.Media;

namespace ConstruSys.Desktop.Views.Produtos
{
    public partial class VisualizarProduto : Window
    {
        private readonly Produto _produto;

        public VisualizarProduto(
            Produto produto)
        {
            InitializeComponent();

            _produto = produto;

            CarregarDados();
        }

        private void CarregarDados()
        {
            TxtNomeCabecalho.Text =
                _produto.Nome;

            TxtCodigo.Text =
                ValorOuTraco(
                    _produto.Codigo);

            TxtCodigoBarras.Text =
                ValorOuTraco(
                    _produto.CodigoBarras);

            TxtNome.Text =
                ValorOuTraco(
                    _produto.Nome);

            TxtMarca.Text =
                ValorOuTraco(
                    _produto.Marca);

            TxtCategoria.Text =
                ValorOuTraco(
                    _produto.Categoria);

            TxtSubcategoria.Text =
                ValorOuTraco(
                    _produto.Subcategoria);

            TxtPrecoCusto.Text =
                _produto.PrecoCusto
                    .ToString("C2");

            TxtPrecoVenda.Text =
                _produto.PrecoVenda
                    .ToString("C2");

            TxtMargem.Text =
                $"{_produto.MargemLucro:N2}%";

            TxtEstoqueAtual.Text =
                _produto.EstoqueAtual
                    .ToString("N3");

            TxtEstoqueMinimo.Text =
                _produto.EstoqueMinimo
                    .ToString("N3");

            TxtEstoqueMaximo.Text =
                _produto.EstoqueMaximo.HasValue
                    ? _produto.EstoqueMaximo.Value
                        .ToString("N3")
                    : "-";

            TxtUnidade.Text =
                ValorOuTraco(
                    _produto.UnidadeMedida);

            TxtLocalizacao.Text =
                ValorOuTraco(
                    _produto.LocalizacaoEstoque);

            TxtSituacaoEstoque.Text =
                _produto.StatusEstoque;

            TxtDescricao.Text =
                ValorOuTraco(
                    _produto.Descricao);

            TxtDataCadastro.Text =
                _produto.DataCadastro
                    .ToString("dd/MM/yyyy HH:mm");

            TxtDataAtualizacao.Text =
                _produto.DataAtualizacao.HasValue
                    ? _produto.DataAtualizacao.Value
                        .ToString("dd/MM/yyyy HH:mm")
                    : "Nunca atualizado";

            AtualizarStatus();
        }

        private void AtualizarStatus()
        {
            if (_produto.Ativo)
            {
                TxtStatus.Text =
                    "Ativo";

                TxtStatus.Foreground =
                    new SolidColorBrush(
                        Color.FromRgb(
                            22,
                            101,
                            52));

                BadgeStatus.Background =
                    new SolidColorBrush(
                        Color.FromRgb(
                            220,
                            252,
                            231));
            }
            else
            {
                TxtStatus.Text =
                    "Inativo";

                TxtStatus.Foreground =
                    new SolidColorBrush(
                        Color.FromRgb(
                            153,
                            27,
                            27));

                BadgeStatus.Background =
                    new SolidColorBrush(
                        Color.FromRgb(
                            254,
                            226,
                            226));
            }
        }

        private static string ValorOuTraco(
            string? valor)
        {
            return string.IsNullOrWhiteSpace(valor)
                ? "-"
                : valor;
        }

        private void Fechar_Click(
            object sender,
            RoutedEventArgs e)
        {
            Close();
        }
    }
}