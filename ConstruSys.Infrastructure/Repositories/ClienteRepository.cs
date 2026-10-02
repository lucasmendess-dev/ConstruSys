using ConstruSys.Domain.Entities;
using ConstruSys.Domain.Interfaces;
using ConstruSys.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ConstruSys.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> ObterTodosAsync()
        {
            return await _context.Clientes
                .AsNoTracking()
                .OrderBy(
                    c => c.NomeRazaoSocial)
                .ToListAsync();
        }

        public async Task<List<Cliente>> ObterExcluidosAsync()
        {
            return await _context.Clientes
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Where(c => c.Excluido)
                .OrderByDescending(
                    c => c.DataExclusao)
                .ToListAsync();
        }

        public async Task<Cliente?> ObterPorIdAsync(int id)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(
                    c => c.Id == id);
        }

        public async Task<Cliente?> ObterExcluidoPorIdAsync(
            int id)
        {
            return await _context.Clientes
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(
                    c => c.Id == id &&
                         c.Excluido);
        }

        public async Task<List<Cliente>> PesquisarAsync(
            string termo)
        {
            termo = termo.Trim();

            if (string.IsNullOrWhiteSpace(termo))
                return await ObterTodosAsync();

            return await _context.Clientes
                .AsNoTracking()
                .Where(c =>
                    c.NomeRazaoSocial.Contains(termo) ||
                    (c.NomeFantasia != null &&
                     c.NomeFantasia.Contains(termo)) ||
                    (c.CpfCnpj != null &&
                     c.CpfCnpj.Contains(termo)) ||
                    (c.Telefone != null &&
                     c.Telefone.Contains(termo)) ||
                    (c.WhatsApp != null &&
                     c.WhatsApp.Contains(termo)) ||
                    (c.Email != null &&
                     c.Email.Contains(termo)))
                .OrderBy(c => c.NomeRazaoSocial)
                .ToListAsync();
        }

        public async Task AdicionarAsync(
            Cliente cliente)
        {
            cliente.Excluido = false;

            _context.Clientes.Add(cliente);

            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(
            Cliente cliente)
        {
            _context.Clientes.Update(cliente);

            await _context.SaveChangesAsync();
        }

        public async Task AlterarStatusAsync(
            int id,
            bool ativo)
        {
            Cliente? cliente =
                await _context.Clientes
                    .FirstOrDefaultAsync(
                        c => c.Id == id);

            if (cliente == null)
            {
                throw new InvalidOperationException(
                    "Cliente não encontrado.");
            }

            cliente.Ativo = ativo;
            cliente.DataAtualizacao = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task ExcluirAsync(int id)
        {
            Cliente? cliente =
                await _context.Clientes
                    .FirstOrDefaultAsync(
                        c => c.Id == id);

            if (cliente == null)
            {
                throw new InvalidOperationException(
                    "Cliente não encontrado.");
            }

            cliente.Excluido = true;
            cliente.Ativo = false;
            cliente.DataExclusao = DateTime.Now;
            cliente.DataAtualizacao = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task RestaurarAsync(int id)
        {
            Cliente? cliente =
                await _context.Clientes
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(
                        c => c.Id == id &&
                             c.Excluido);

            if (cliente == null)
            {
                throw new InvalidOperationException(
                    "Cliente excluído não encontrado.");
            }

            cliente.Excluido = false;
            cliente.Ativo = false;
            cliente.DataExclusao = null;
            cliente.DataAtualizacao = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task ExcluirDefinitivamenteAsync(
            int id)
        {
            Cliente? cliente =
                await _context.Clientes
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(
                        c => c.Id == id &&
                             c.Excluido);

            if (cliente == null)
            {
                throw new InvalidOperationException(
                    "Cliente excluído não encontrado.");
            }

            _context.Clientes.Remove(cliente);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> CpfCnpjExisteAsync(
            string cpfCnpj,
            int? ignorarId = null)
        {
            if (string.IsNullOrWhiteSpace(cpfCnpj))
                return false;

            IQueryable<Cliente> query =
                _context.Clientes
                    .IgnoreQueryFilters()
                    .AsNoTracking()
                    .Where(c =>
                        c.CpfCnpj == cpfCnpj);

            if (ignorarId.HasValue)
            {
                query =
                    query.Where(
                        c => c.Id != ignorarId.Value);
            }

            return await query.AnyAsync();
        }
    }
}