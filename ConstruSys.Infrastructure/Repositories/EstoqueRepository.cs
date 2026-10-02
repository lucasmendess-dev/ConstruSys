using ConstruSys.Domain.Entities;
using ConstruSys.Domain.Enums;
using ConstruSys.Domain.Interfaces;
using ConstruSys.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ConstruSys.Infrastructure.Repositories
{
    public class EstoqueRepository : IEstoqueRepository
    {
        private readonly AppDbContext _context;

        public EstoqueRepository(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MovimentacaoEstoque>>
            ObterMovimentacoesAsync()
        {
            return await _context.MovimentacoesEstoque
                .AsNoTracking()
                .Include(m => m.Produto)
                .OrderByDescending(m => m.DataMovimentacao)
                .ToListAsync();
        }

        public async Task<List<MovimentacaoEstoque>>
            ObterMovimentacoesPorProdutoAsync(
                int produtoId)
        {
            return await _context.MovimentacoesEstoque
                .AsNoTracking()
                .Include(m => m.Produto)
                .Where(m => m.ProdutoId == produtoId)
                .OrderByDescending(m => m.DataMovimentacao)
                .ToListAsync();
        }

        public async Task RegistrarMovimentacaoAsync(
            int produtoId,
            TipoMovimentacaoEstoque tipo,
            decimal quantidade,
            string? observacao)
        {
            if (quantidade <= 0)
            {
                throw new ArgumentException(
                    "A quantidade deve ser maior que zero.");
            }

            await using var transaction =
                await _context.Database
                    .BeginTransactionAsync();

            try
            {
                Produto? produto =
                    await _context.Produtos
                        .FirstOrDefaultAsync(
                            p => p.Id == produtoId);

                if (produto == null)
                {
                    throw new InvalidOperationException(
                        "Produto não encontrado.");
                }

                if (!produto.Ativo)
                {
                    throw new InvalidOperationException(
                        "Não é possível movimentar estoque de um produto inativo.");
                }

                decimal estoqueAnterior =
                    produto.EstoqueAtual;

                decimal estoquePosterior;

                switch (tipo)
                {
                    case TipoMovimentacaoEstoque.Entrada:

                        estoquePosterior =
                            estoqueAnterior + quantidade;

                        break;

                    case TipoMovimentacaoEstoque.Saida:

                        if (quantidade > estoqueAnterior)
                        {
                            throw new InvalidOperationException(
                                $"Estoque insuficiente. Saldo disponível: {estoqueAnterior:N3}.");
                        }

                        estoquePosterior =
                            estoqueAnterior - quantidade;

                        break;

                    default:

                        throw new InvalidOperationException(
                            "Tipo de movimentação inválido.");
                }

                MovimentacaoEstoque movimentacao =
                    new()
                    {
                        ProdutoId = produto.Id,
                        Tipo = tipo,
                        Quantidade = quantidade,
                        EstoqueAnterior = estoqueAnterior,
                        EstoquePosterior = estoquePosterior,
                        Observacao = observacao,
                        DataMovimentacao = DateTime.Now
                    };

                produto.EstoqueAtual =
                    estoquePosterior;

                produto.DataAtualizacao =
                    DateTime.Now;

                _context.MovimentacoesEstoque
                    .Add(movimentacao);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();

                throw;
            }
        }

        public async Task RegistrarAjusteAsync(
            int produtoId,
            decimal novoEstoque,
            string? observacao)
        {
            if (novoEstoque < 0)
            {
                throw new ArgumentException(
                    "O novo saldo de estoque não pode ser negativo.");
            }

            await using var transaction =
                await _context.Database
                    .BeginTransactionAsync();

            try
            {
                Produto? produto =
                    await _context.Produtos
                        .FirstOrDefaultAsync(
                            p => p.Id == produtoId);

                if (produto == null)
                {
                    throw new InvalidOperationException(
                        "Produto não encontrado.");
                }

                decimal estoqueAnterior =
                    produto.EstoqueAtual;

                decimal diferenca =
                    Math.Abs(
                        novoEstoque -
                        estoqueAnterior);

                MovimentacaoEstoque movimentacao =
                    new()
                    {
                        ProdutoId = produto.Id,
                        Tipo = TipoMovimentacaoEstoque.Ajuste,
                        Quantidade = diferenca,
                        EstoqueAnterior = estoqueAnterior,
                        EstoquePosterior = novoEstoque,
                        Observacao = observacao,
                        DataMovimentacao = DateTime.Now
                    };

                produto.EstoqueAtual =
                    novoEstoque;

                produto.DataAtualizacao =
                    DateTime.Now;

                _context.MovimentacoesEstoque
                    .Add(movimentacao);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();

                throw;
            }
        }
    }
}