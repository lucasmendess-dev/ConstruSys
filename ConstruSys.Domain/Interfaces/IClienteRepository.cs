using ConstruSys.Domain.Entities;

namespace ConstruSys.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> ObterTodosAsync();

        Task<List<Cliente>> ObterExcluidosAsync();

        Task<Cliente?> ObterPorIdAsync(int id);

        Task<Cliente?> ObterExcluidoPorIdAsync(int id);

        Task<List<Cliente>> PesquisarAsync(
            string termo);

        Task AdicionarAsync(
            Cliente cliente);

        Task AtualizarAsync(
            Cliente cliente);

        Task AlterarStatusAsync(
            int id,
            bool ativo);

        Task ExcluirAsync(int id);

        Task RestaurarAsync(int id);

        Task ExcluirDefinitivamenteAsync(int id);

        Task<bool> CpfCnpjExisteAsync(
            string cpfCnpj,
            int? ignorarId = null);
    }
}