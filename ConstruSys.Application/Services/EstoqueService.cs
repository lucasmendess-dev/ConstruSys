using ConstruSys.Domain.Entities;
using ConstruSys.Domain.Enums;
using ConstruSys.Domain.Interfaces;

namespace ConstruSys.Application.Services
{
    public class EstoqueService
    {
        private readonly IEstoqueRepository _estoqueRepository;

        public EstoqueService(
            IEstoqueRepository estoqueRepository)
        {
            _estoqueRepository =
                estoqueRepository;
        }

        public async Task<List<MovimentacaoEstoque>>
            ObterMovimentacoesAsync()
        {
            return await _estoqueRepository
                .ObterMovimentacoesAsync();
        }

        public async Task<List<MovimentacaoEstoque>>
            ObterMovimentacoesPorProdutoAsync(
                int produtoId)
        {
            return await _estoqueRepository
                .ObterMovimentacoesPorProdutoAsync(
                    produtoId);
        }

        public async Task RegistrarEntradaAsync(
            int produtoId,
            decimal quantidade,
            string? observacao)
        {
            await _estoqueRepository
                .RegistrarMovimentacaoAsync(
                    produtoId,
                    TipoMovimentacaoEstoque.Entrada,
                    quantidade,
                    observacao);
        }

        public async Task RegistrarSaidaAsync(
            int produtoId,
            decimal quantidade,
            string? observacao)
        {
            await _estoqueRepository
                .RegistrarMovimentacaoAsync(
                    produtoId,
                    TipoMovimentacaoEstoque.Saida,
                    quantidade,
                    observacao);
        }

        public async Task RegistrarAjusteAsync(
            int produtoId,
            decimal novoEstoque,
            string? observacao)
        {
            await _estoqueRepository
                .RegistrarAjusteAsync(
                    produtoId,
                    novoEstoque,
                    observacao);
        }
    }
}