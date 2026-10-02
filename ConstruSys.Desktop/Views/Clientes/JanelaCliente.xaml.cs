using ConstruSys.Application.Services;
using ConstruSys.Domain.Entities;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace ConstruSys.Desktop.Views.Clientes
{
    public partial class JanelaCliente : Window
    {
        private readonly ClienteService _clienteService;
        private readonly Cliente? _clienteEdicao;

        public JanelaCliente(
            ClienteService clienteService,
            Cliente? cliente = null)
        {
            InitializeComponent();

            _clienteService = clienteService;
            _clienteEdicao = cliente;

            if (_clienteEdicao != null)
                CarregarCliente();
        }

        private void CarregarCliente()
        {
            TxtTitulo.Text = "Editar Cliente";

            foreach (ComboBoxItem item in CmbTipoPessoa.Items)
            {
                if (item.Content?.ToString() ==
                    _clienteEdicao!.TipoPessoa)
                {
                    CmbTipoPessoa.SelectedItem = item;
                    break;
                }
            }

            TxtCpfCnpj.Text =
                _clienteEdicao!.CpfCnpj;

            TxtNome.Text =
                _clienteEdicao.NomeRazaoSocial;

            TxtNomeFantasia.Text =
                _clienteEdicao.NomeFantasia;

            TxtRgIe.Text =
                _clienteEdicao.RgIe;

            TxtTelefone.Text =
                _clienteEdicao.Telefone;

            TxtWhatsApp.Text =
                _clienteEdicao.WhatsApp;

            TxtEmail.Text =
                _clienteEdicao.Email;

            TxtCep.Text =
                _clienteEdicao.Cep;

            TxtEndereco.Text =
                _clienteEdicao.Endereco;

            TxtNumero.Text =
                _clienteEdicao.Numero;

            TxtBairro.Text =
                _clienteEdicao.Bairro;

            TxtCidade.Text =
                _clienteEdicao.Cidade;

            TxtEstado.Text =
                _clienteEdicao.Estado;

            TxtLimiteCredito.Text =
                _clienteEdicao.LimiteCredito
                    .ToString("N2");

            ChkAtivo.IsChecked =
                _clienteEdicao.Ativo;
        }

        private async void Salvar_Click(
            object sender,
            RoutedEventArgs e)
        {
            try
            {
                decimal limiteCredito = 0;

                if (!string.IsNullOrWhiteSpace(
                    TxtLimiteCredito.Text))
                {
                    if (!decimal.TryParse(
                        TxtLimiteCredito.Text,
                        NumberStyles.Number,
                        CultureInfo.CurrentCulture,
                        out limiteCredito))
                    {
                        MessageBox.Show(
                            "Informe um limite de crédito válido.");

                        return;
                    }
                }

                string tipoPessoa =
                    (CmbTipoPessoa.SelectedItem
                        as ComboBoxItem)?
                    .Content?
                    .ToString()
                    ?? "Física";

                if (_clienteEdicao == null)
                {
                    Cliente cliente = new()
                    {
                        TipoPessoa = tipoPessoa,
                        NomeRazaoSocial =
                            TxtNome.Text.Trim(),

                        NomeFantasia =
                            TextoOuNull(
                                TxtNomeFantasia.Text),

                        CpfCnpj =
                            TextoOuNull(
                                TxtCpfCnpj.Text),

                        RgIe =
                            TextoOuNull(
                                TxtRgIe.Text),

                        Telefone =
                            TextoOuNull(
                                TxtTelefone.Text),

                        WhatsApp =
                            TextoOuNull(
                                TxtWhatsApp.Text),

                        Email =
                            TextoOuNull(
                                TxtEmail.Text),

                        Cep =
                            TextoOuNull(
                                TxtCep.Text),

                        Endereco =
                            TextoOuNull(
                                TxtEndereco.Text),

                        Numero =
                            TextoOuNull(
                                TxtNumero.Text),

                        Bairro =
                            TextoOuNull(
                                TxtBairro.Text),

                        Cidade =
                            TextoOuNull(
                                TxtCidade.Text),

                        Estado =
                            TextoOuNull(
                                TxtEstado.Text)?
                            .ToUpper(),

                        LimiteCredito =
                            limiteCredito,

                        Ativo =
                            ChkAtivo.IsChecked == true
                    };

                    await _clienteService
                        .AdicionarAsync(cliente);
                }
                else
                {
                    _clienteEdicao.TipoPessoa =
                        tipoPessoa;

                    _clienteEdicao.NomeRazaoSocial =
                        TxtNome.Text.Trim();

                    _clienteEdicao.NomeFantasia =
                        TextoOuNull(
                            TxtNomeFantasia.Text);

                    _clienteEdicao.CpfCnpj =
                        TextoOuNull(
                            TxtCpfCnpj.Text);

                    _clienteEdicao.RgIe =
                        TextoOuNull(
                            TxtRgIe.Text);

                    _clienteEdicao.Telefone =
                        TextoOuNull(
                            TxtTelefone.Text);

                    _clienteEdicao.WhatsApp =
                        TextoOuNull(
                            TxtWhatsApp.Text);

                    _clienteEdicao.Email =
                        TextoOuNull(
                            TxtEmail.Text);

                    _clienteEdicao.Cep =
                        TextoOuNull(
                            TxtCep.Text);

                    _clienteEdicao.Endereco =
                        TextoOuNull(
                            TxtEndereco.Text);

                    _clienteEdicao.Numero =
                        TextoOuNull(
                            TxtNumero.Text);

                    _clienteEdicao.Bairro =
                        TextoOuNull(
                            TxtBairro.Text);

                    _clienteEdicao.Cidade =
                        TextoOuNull(
                            TxtCidade.Text);

                    _clienteEdicao.Estado =
                        TextoOuNull(
                            TxtEstado.Text)?
                        .ToUpper();

                    _clienteEdicao.LimiteCredito =
                        limiteCredito;

                    _clienteEdicao.Ativo =
                        ChkAtivo.IsChecked == true;

                    await _clienteService
                        .AtualizarAsync(
                            _clienteEdicao);
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
            DialogResult = false;

            Close();
        }
    }
}