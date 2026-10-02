namespace ConstruSys.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }

        public string TipoPessoa { get; set; } = "Física";

        public string NomeRazaoSocial { get; set; } = string.Empty;

        public string? NomeFantasia { get; set; }

        public string? CpfCnpj { get; set; }

        public string? RgIe { get; set; }

        public string? Telefone { get; set; }

        public string? WhatsApp { get; set; }

        public string? Email { get; set; }

        public string? Cep { get; set; }

        public string? Endereco { get; set; }

        public string? Numero { get; set; }

        public string? Complemento { get; set; }

        public string? Bairro { get; set; }

        public string? Cidade { get; set; }

        public string? Estado { get; set; }

        public decimal LimiteCredito { get; set; }

        public bool Ativo { get; set; } = true;

        public string? Observacoes { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public DateTime? DataAtualizacao { get; set; }
    }
}