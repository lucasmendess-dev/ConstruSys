namespace ConstruSys.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Login { get; set; } = string.Empty;

        public string SenhaHash { get; set; } = string.Empty;

        public string Perfil { get; set; } = "Vendedor";

        public bool Ativo { get; set; } = true;

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public DateTime? UltimoAcesso { get; set; }
    }
}