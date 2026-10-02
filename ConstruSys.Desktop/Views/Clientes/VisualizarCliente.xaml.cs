using ConstruSys.Domain.Entities;
using System.Windows;
using System.Windows.Media;

namespace ConstruSys.Desktop.Views.Clientes
{
    public partial class VisualizarCliente : Window
    {
        private readonly Cliente _cliente;

        public VisualizarCliente(Cliente cliente)
        {
            InitializeComponent();

            _cliente = cliente;

            CarregarDados();
        }

        private void CarregarDados()
        {
            TxtNomeCabecalho.Text =
                _cliente.NomeRazaoSocial;

            TxtTipoPessoa.Text =
                ValorOuTraco(
                    _cliente.TipoPessoa);

            TxtCpfCnpj.Text =
                ValorOuTraco(
                    _cliente.CpfCnpj);

            TxtNome.Text =
                ValorOuTraco(
                    _cliente.NomeRazaoSocial);

            TxtNomeFantasia.Text =
                ValorOuTraco(
                    _cliente.NomeFantasia);

            TxtRgIe.Text =
                ValorOuTraco(
                    _cliente.RgIe);

            TxtTelefone.Text =
                ValorOuTraco(
                    _cliente.Telefone);

            TxtWhatsApp.Text =
                ValorOuTraco(
                    _cliente.WhatsApp);

            TxtEmail.Text =
                ValorOuTraco(
                    _cliente.Email);

            TxtCep.Text =
                ValorOuTraco(
                    _cliente.Cep);

            TxtEndereco.Text =
                ValorOuTraco(
                    _cliente.Endereco);

            TxtNumero.Text =
                ValorOuTraco(
                    _cliente.Numero);

            TxtComplemento.Text =
                ValorOuTraco(
                    _cliente.Complemento);

            TxtBairro.Text =
                ValorOuTraco(
                    _cliente.Bairro);

            TxtCidadeEstado.Text =
                MontarCidadeEstado();

            TxtLimiteCredito.Text =
                _cliente.LimiteCredito
                    .ToString("C2");

            TxtObservacoes.Text =
                ValorOuTraco(
                    _cliente.Observacoes);

            TxtDataCadastro.Text =
                _cliente.DataCadastro
                    .ToString("dd/MM/yyyy HH:mm");

            TxtDataAtualizacao.Text =
                _cliente.DataAtualizacao.HasValue
                    ? _cliente.DataAtualizacao.Value
                        .ToString("dd/MM/yyyy HH:mm")
                    : "Nunca atualizado";

            AtualizarStatus();
        }

        private void AtualizarStatus()
        {
            if (_cliente.Ativo)
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

        private string MontarCidadeEstado()
        {
            bool possuiCidade =
                !string.IsNullOrWhiteSpace(
                    _cliente.Cidade);

            bool possuiEstado =
                !string.IsNullOrWhiteSpace(
                    _cliente.Estado);

            if (possuiCidade &&
                possuiEstado)
            {
                return
                    $"{_cliente.Cidade} - {_cliente.Estado}";
            }

            if (possuiCidade)
                return _cliente.Cidade!;

            if (possuiEstado)
                return _cliente.Estado!;

            return "-";
        }

        private static string ValorOuTraco(
            string? valor)
        {
            return string.IsNullOrWhiteSpace(
                valor)
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