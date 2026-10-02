using ConstruSys.Domain.Entities;

namespace ConstruSys.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<List<Produto>> ObterTodosAsync();

        Task<Produto?> ObterPorIdAsync(int id);

        Task<List<Produto>> PesquisarAsync(string termo);

        Task AdicionarAsync(Produto produto);

        Task AtualizarAsync(Produto produto);

        Task ExcluirAsync(int id);

        Task<bool> CodigoExisteAsync(string codigo, int? ignorarId = null);

        Task<bool> CodigoBarrasExisteAsync(string codigoBarras, int? ignorarId = null);
    }
}