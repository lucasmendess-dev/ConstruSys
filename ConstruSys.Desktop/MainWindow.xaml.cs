using ConstruSys.Desktop.Views.Clientes;
using ConstruSys.Desktop.Views.Dashboard;
using ConstruSys.Desktop.Views.Estoque;
using ConstruSys.Desktop.Views.PDV;
using ConstruSys.Desktop.Views.Produtos;
using System.Windows;
using System.Windows.Controls;

namespace ConstruSys.Desktop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainContent.Content = new DashboardView();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button)
                return;

            string pagina = button.Tag?.ToString() ?? string.Empty;

            switch (pagina)
            {
                case "Dashboard":
                    MainContent.Content = new DashboardView();
                    break;

                case "Produtos":
                    MainContent.Content = new ProdutosView();
                    break;

                case "Clientes":
                    MainContent.Content = new ClientesView();
                    break;

                case "Estoque":
                    MainContent.Content = new EstoqueView();
                    break;

                case "PDV":
                    MainContent.Content = new PdvView();
                    break;

                default:
                    MessageBox.Show(
                        "Este módulo ainda será implementado.",
                        "ConstruSys",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    break;
            }
        }
    }
}