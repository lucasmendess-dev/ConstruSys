using ConstruSys.Application.Helpers;
using ConstruSys.Domain.Entities;
using ConstruSys.Domain.Interfaces;

namespace ConstruSys.Application.Services
{
    public class CadastroProdutoAuxiliarService
    {
        private readonly ICadastroProdutoAuxiliarRepository _repository;

        public CadastroProdutoAuxiliarService(
            ICadastroProdutoAuxiliarRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CategoriaProduto>>
            ObterCategoriasAsync()
        {
            return await _repository
                .ObterCategoriasAsync();
        }

        public async Task<List<MarcaProduto>>
            ObterMarcasAsync()
        {
            return await _repository
                .ObterMarcasAsync();
        }

        public async Task<List<LocalizacaoEstoqueCadastro>>
            ObterLocalizacoesAsync()
        {
            return await _repository
                .ObterLocalizacoesAsync();
        }

        public async Task<CategoriaProduto>
            AdicionarCategoriaAsync(
                string nome)
        {
            nome = TextoHelper.CaixaAlta(nome);

            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException(
                    "Informe o nome da categoria.");

            if (await _repository
                .CategoriaExisteAsync(nome))
            {
                throw new InvalidOperationException(
                    "Essa categoria já está cadastrada.");
            }

            CategoriaProduto categoria =
                new()
                {
                    Nome = nome,
                    Ativo = true,
                    DataCadastro = DateTime.Now
                };

            return await _repository
                .AdicionarCategoriaAsync(categoria);
        }

        public async Task<MarcaProduto>
            AdicionarMarcaAsync(
                string nome)
        {
            nome = TextoHelper.CaixaAlta(nome);

            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException(
                    "Informe o nome da marca.");

            if (await _repository
                .MarcaExisteAsync(nome))
            {
                throw new InvalidOperationException(
                    "Essa marca já está cadastrada.");
            }

            MarcaProduto marca =
                new()
                {
                    Nome = nome,
                    Ativo = true,
                    DataCadastro = DateTime.Now
                };

            return await _repository
                .AdicionarMarcaAsync(marca);
        }

        public async Task<LocalizacaoEstoqueCadastro>
            AdicionarLocalizacaoAsync(
                string nome)
        {
            nome = TextoHelper.CaixaAlta(nome);

            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException(
                    "Informe a localização.");

            if (await _repository
                .LocalizacaoExisteAsync(nome))
            {
                throw new InvalidOperationException(
                    "Essa localização já está cadastrada.");
            }

            LocalizacaoEstoqueCadastro localizacao =
                new()
                {
                    Nome = nome,
                    Ativo = true,
                    DataCadastro = DateTime.Now
                };

            return await _repository
                .AdicionarLocalizacaoAsync(localizacao);
        }
    }
}