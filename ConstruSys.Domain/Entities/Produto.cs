namespace ConstruSys.Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }

        public string Codigo { get; set; } = string.Empty;

        public string? CodigoBarras { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        public string? Categoria { get; set; }

        public string? Subcategoria { get; set; }

        public string? Marca { get; set; }

        public string UnidadeMedida { get; set; } = "UN";

        public decimal PrecoCusto { get; set; }

        public decimal PrecoVenda { get; set; }

        public decimal EstoqueAtual { get; set; }

        public decimal EstoqueMinimo { get; set; }

        public decimal? EstoqueMaximo { get; set; }

        public string? LocalizacaoEstoque { get; set; }

        public decimal? Peso { get; set; }

        public bool Ativo { get; set; } = true;

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public DateTime? DataAtualizacao { get; set; }

        public decimal MargemLucro
        {
            get
            {
                if (PrecoCusto <= 0)
                    return 0;

                return ((PrecoVenda - PrecoCusto) / PrecoCusto) * 100;
            }
        }
    }
}