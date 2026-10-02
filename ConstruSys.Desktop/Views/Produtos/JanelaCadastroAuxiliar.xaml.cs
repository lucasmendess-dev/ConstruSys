using System.Windows;

namespace ConstruSys.Desktop.Views.Produtos
{
    public partial class JanelaCadastroAuxiliar : Window
    {
        public string ValorDigitado { get; private set; }
            = string.Empty;

        public JanelaCadastroAuxiliar(
            string titulo,
            string subtitulo)
        {
            InitializeComponent();

            Title = titulo;

            TxtTitulo.Text = titulo;
            TxtSubtitulo.Text = subtitulo;

            Loaded += (_, _) =>
            {
                TxtValor.Focus();
                TxtValor.SelectAll();
            };
        }

        private void Cadastrar_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                TxtValor.Text))
            {
                MessageBox.Show(
                    "Informe um valor.",
                    "ConstruSys",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                TxtValor.Focus();
                return;
            }

            ValorDigitado =
                TxtValor.Text.Trim();

            DialogResult = true;
            Close();
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