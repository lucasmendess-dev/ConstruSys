namespace ConstruSys.Domain.Entities
{
    public class MarcaProduto
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public bool Ativo { get; set; } = true;

        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}