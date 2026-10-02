using ConstruSys.Domain.Entities;
using ConstruSys.Domain.Interfaces;
using ConstruSys.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ConstruSys.Infrastructure.Repositories
{
    public class CadastroProdutoAuxiliarRepository
        : ICadastroProdutoAuxiliarRepository
    {
        private readonly AppDbContext _context;

        public CadastroProdutoAuxiliarRepository(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoriaProduto>>
            ObterCategoriasAsync()
        {
            return await _context.CategoriasProduto
                .AsNoTracking()
                .Where(x => x.Ativo)
                .OrderBy(x => x.Nome)
                .ToListAsync();
        }

        public async Task<List<MarcaProduto>>
            ObterMarcasAsync()
        {
            return await _context.MarcasProduto
                .AsNoTracking()
                .Where(x => x.Ativo)
                .OrderBy(x => x.Nome)
                .ToListAsync();
        }

        public async Task<List<LocalizacaoEstoqueCadastro>>
            ObterLocalizacoesAsync()
        {
            return await _context.LocalizacoesEstoque
                .AsNoTracking()
                .Where(x => x.Ativo)
                .OrderBy(x => x.Nome)
                .ToListAsync();
        }

        public async Task<CategoriaProduto>
            AdicionarCategoriaAsync(
                CategoriaProduto categoria)
        {
            _context.CategoriasProduto.Add(
                categoria);

            await _context.SaveChangesAsync();

            return categoria;
        }

        public async Task<MarcaProduto>
            AdicionarMarcaAsync(
                MarcaProduto marca)
        {
            _context.MarcasProduto.Add(
                marca);

            await _context.SaveChangesAsync();

            return marca;
        }

        public async Task<LocalizacaoEstoqueCadastro>
            AdicionarLocalizacaoAsync(
                LocalizacaoEstoqueCadastro localizacao)
        {
            _context.LocalizacoesEstoque.Add(
                localizacao);

            await _context.SaveChangesAsync();

            return localizacao;
        }

        public async Task<bool> CategoriaExisteAsync(
            string nome)
        {
            return await _context.CategoriasProduto
                .AnyAsync(x => x.Nome == nome);
        }

        public async Task<bool> MarcaExisteAsync(
            string nome)
        {
            return await _context.MarcasProduto
                .AnyAsync(x => x.Nome == nome);
        }

        public async Task<bool> LocalizacaoExisteAsync(
            string nome)
        {
            return await _context.LocalizacoesEstoque
                .AnyAsync(x => x.Nome == nome);
        }
    }
}