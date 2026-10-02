using ConstruSys.Domain.Entities;
using ConstruSys.Domain.Interfaces;
using ConstruSys.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ConstruSys.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Produto>> ObterTodosAsync()
        {
            return await _context.Produtos
                .AsNoTracking()
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<Produto?> ObterPorIdAsync(int id)
        {
            return await _context.Produtos
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Produto>> PesquisarAsync(string termo)
        {
            termo = termo.Trim();

            if (string.IsNullOrWhiteSpace(termo))
                return await ObterTodosAsync();

            return await _context.Produtos
                .AsNoTracking()
                .Where(p =>
                    p.Nome.Contains(termo) ||
                    p.Codigo.Contains(termo) ||
                    (p.CodigoBarras != null && p.CodigoBarras.Contains(termo)) ||
                    (p.Marca != null && p.Marca.Contains(termo)) ||
                    (p.Categoria != null && p.Categoria.Contains(termo)))
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task AdicionarAsync(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirAsync(int id)
        {
            Produto? produto = await _context.Produtos
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
                return;

            _context.Produtos.Remove(produto);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> CodigoExisteAsync(string codigo, int? ignorarId = null)
        {
            IQueryable<Produto> query = _context.Produtos
                .AsNoTracking()
                .Where(p => p.Codigo == codigo);

            if (ignorarId.HasValue)
                query = query.Where(p => p.Id != ignorarId.Value);

            return await query.AnyAsync();
        }

        public async Task<bool> CodigoBarrasExisteAsync(
            string codigoBarras,
            int? ignorarId = null)
        {
            if (string.IsNullOrWhiteSpace(codigoBarras))
                return false;

            IQueryable<Produto> query = _context.Produtos
                .AsNoTracking()
                .Where(p => p.CodigoBarras == codigoBarras);

            if (ignorarId.HasValue)
                query = query.Where(p => p.Id != ignorarId.Value);

            return await query.AnyAsync();
        }
    }
}