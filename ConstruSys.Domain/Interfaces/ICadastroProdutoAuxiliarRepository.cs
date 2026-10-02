using ConstruSys.Domain.Entities;

namespace ConstruSys.Domain.Interfaces
{
    public interface ICadastroProdutoAuxiliarRepository
    {
        Task<List<CategoriaProduto>> ObterCategoriasAsync();

        Task<List<MarcaProduto>> ObterMarcasAsync();

        Task<List<LocalizacaoEstoqueCadastro>> ObterLocalizacoesAsync();

        Task<CategoriaProduto> AdicionarCategoriaAsync(
            CategoriaProduto categoria);

        Task<MarcaProduto> AdicionarMarcaAsync(
            MarcaProduto marca);

        Task<LocalizacaoEstoqueCadastro> AdicionarLocalizacaoAsync(
            LocalizacaoEstoqueCadastro localizacao);

        Task<bool> CategoriaExisteAsync(string nome);

        Task<bool> MarcaExisteAsync(string nome);

        Task<bool> LocalizacaoExisteAsync(string nome);
    }
}