using ConstruSys.Domain.Enums;

namespace ConstruSys.Domain.Entities
{
    public class MovimentacaoEstoque
    {
        public int Id { get; set; }

        public int ProdutoId { get; set; }

        public Produto Produto { get; set; } = null!;

        public TipoMovimentacaoEstoque Tipo { get; set; }

        public decimal Quantidade { get; set; }

        public decimal EstoqueAnterior { get; set; }

        public decimal EstoquePosterior { get; set; }

        public string? Observacao { get; set; }

        public DateTime DataMovimentacao { get; set; } = DateTime.Now;
    }
}