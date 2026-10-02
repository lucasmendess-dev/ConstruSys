using ConstruSys.Domain.Entities;
using ConstruSys.Domain.Enums;

namespace ConstruSys.Domain.Interfaces
{
    public interface IEstoqueRepository
    {
        Task<List<MovimentacaoEstoque>> ObterMovimentacoesAsync();

        Task<List<MovimentacaoEstoque>> ObterMovimentacoesPorProdutoAsync(
            int produtoId);

        Task RegistrarMovimentacaoAsync(
            int produtoId,
            TipoMovimentacaoEstoque tipo,
            decimal quantidade,
            string? observacao);

        Task RegistrarAjusteAsync(
            int produtoId,
            decimal novoEstoque,
            string? observacao);
    }
}