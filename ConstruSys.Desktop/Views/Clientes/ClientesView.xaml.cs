using ConstruSys.Application.Services;
using ConstruSys.Domain.Entities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ConstruSys.Desktop.Views.Clientes
{
    public partial class ClientesView : UserControl
    {
        private readonly ClienteService _clienteService;

        private List<Cliente> _clientes = new();

        public ClientesView(
            ClienteService clienteService)
        {
            InitializeComponent();

            _clienteService = clienteService;

            Loaded += ClientesView_Loaded;
        }

        private async void ClientesView_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            await CarregarClientesAsync();
        }

        private async Task CarregarClientesAsync()
        {
            try
            {
                _clientes =
                    await _clienteService
                        .ObterTodosAsync();

                AtualizarIndicadores();

                AplicarFiltros();
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

        private void AtualizarIndicadores()
        {
            TxtTotalCard.Text =
                _clientes.Count.ToString();

            TxtAtivosCard.Text =
                _clientes.Count(
                    c => c.Ativo)
                .ToString();

            TxtInativosCard.Text =
                _clientes.Count(
                    c => !c.Ativo)
                .ToString();
        }

        private void AplicarFiltros()
        {
            IEnumerable<Cliente> resultado =
                _clientes;

            string termo =
                TxtPesquisa.Text
                    .Trim()
                    .ToLower();

            if (!string.IsNullOrWhiteSpace(termo))
            {
                resultado =
                    resultado.Where(c =>
                        c.NomeRazaoSocial
                            .ToLower()
                            .Contains(termo) ||

                        (c.NomeFantasia ?? "")
                            .ToLower()
                            .Contains(termo) ||

                        (c.CpfCnpj ?? "")
                            .ToLower()
                            .Contains(termo) ||

                        (c.Telefone ?? "")
                            .ToLower()
                            .Contains(termo) ||

                        (c.WhatsApp ?? "")
                            .ToLower()
                            .Contains(termo));
            }

            string filtro =
                (CmbFiltroStatus.SelectedItem
                    as ComboBoxItem)?
                .Content?
                .ToString()
                ?? "Todos";

            if (filtro == "Ativos")
            {
                resultado =
                    resultado.Where(
                        c => c.Ativo);
            }
            else if (filtro == "Inativos")
            {
                resultado =
                    resultado.Where(
                        c => !c.Ativo);
            }

            List<Cliente> lista =
                resultado
                    .OrderBy(
                        c => c.NomeRazaoSocial)
                    .ToList();

            GridClientes.ItemsSource =
                lista;

            TxtQuantidade.Text =
                $"{lista.Count} cliente(s)";
        }

        private void NovoCliente_Click(
            object sender,
            RoutedEventArgs e)
        {
            JanelaCliente janela =
                new(_clienteService);

            janela.Owner =
                Window.GetWindow(this);

            bool? resultado =
                janela.ShowDialog();

            if (resultado == true)
            {
                _ = CarregarClientesAsync();
            }
        }

        private async void VisualizarCliente_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (sender is not Button button ||
                button.Tag is not Cliente cliente)
            {
                return;
            }

            try
            {
                Cliente? clienteBanco =
                    await _clienteService
                        .ObterPorIdAsync(cliente.Id);

                if (clienteBanco == null)
                {
                    MessageBox.Show(
                        "Cliente não encontrado.",
                        "ConstruSys",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                    return;
                }

                VisualizarCliente janela =
                    new(clienteBanco);

                janela.Owner =
                    Window.GetWindow(this);

                janela.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Não foi possível visualizar o cliente.\n\n{ex.Message}",
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private async void EditarCliente_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (sender is not Button button ||
                button.Tag is not Cliente cliente)
            {
                return;
            }

            Cliente? clienteBanco =
                await _clienteService
                    .ObterPorIdAsync(
                        cliente.Id);

            if (clienteBanco == null)
            {
                MessageBox.Show(
                    "Cliente não encontrado.",
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            JanelaCliente janela =
                new(
                    _clienteService,
                    clienteBanco);

            janela.Owner =
                Window.GetWindow(this);

            bool? resultado =
                janela.ShowDialog();

            if (resultado == true)
            {
                await CarregarClientesAsync();
            }
        }

        private async void AlterarStatusCliente_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (sender is not Button button ||
                button.Tag is not Cliente cliente)
            {
                return;
            }

            bool novoStatus =
                !cliente.Ativo;

            string acao =
                novoStatus
                    ? "ativar"
                    : "inativar";

            MessageBoxResult resultado =
                MessageBox.Show(
                    $"Deseja realmente {acao} o cliente:\n\n{cliente.NomeRazaoSocial}?",
                    "ConstruSys",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

            if (resultado != MessageBoxResult.Yes)
                return;

            try
            {
                await _clienteService
                    .AlterarStatusAsync(
                        cliente.Id,
                        novoStatus);

                await CarregarClientesAsync();
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

        private async void ExcluirCliente_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (sender is not Button button ||
                button.Tag is not Cliente cliente)
            {
                return;
            }

            MessageBoxResult resultado =
                MessageBox.Show(
                    $"Deseja mover o cliente para a lixeira?\n\n" +
                    $"{cliente.NomeRazaoSocial}\n\n" +
                    "O histórico será preservado e o cadastro poderá ser restaurado posteriormente.",
                    "Mover Cliente para Lixeira",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

            if (resultado != MessageBoxResult.Yes)
                return;

            try
            {
                await _clienteService
                    .ExcluirAsync(cliente.Id);

                await CarregarClientesAsync();

                MessageBox.Show(
                    "Cliente movido para a lixeira com sucesso.",
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Não foi possível excluir o cliente.\n\n{ex.Message}",
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

        private void CmbFiltroStatus_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            if (!IsLoaded)
                return;

            AplicarFiltros();
        }
    }
}