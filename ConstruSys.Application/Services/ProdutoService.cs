using ConstruSys.Domain.Entities;
using ConstruSys.Domain.Interfaces;

namespace ConstruSys.Application.Services
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(
            IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<List<Produto>> ObterTodosAsync()
        {
            return await _produtoRepository
                .ObterTodosAsync();
        }

        public async Task<List<Produto>> PesquisarAsync(
            string termo)
        {
            return await _produtoRepository
                .PesquisarAsync(termo);
        }

        public async Task<Produto?> ObterPorIdAsync(int id)
        {
            return await _produtoRepository
                .ObterPorIdAsync(id);
        }

        public async Task AdicionarAsync(
            Produto produto)
        {
            ValidarProduto(produto);

            if (await _produtoRepository
                .CodigoExisteAsync(produto.Codigo))
            {
                throw new InvalidOperationException(
                    "Já existe um produto cadastrado com esse código.");
            }

            if (!string.IsNullOrWhiteSpace(
                produto.CodigoBarras))
            {
                bool codigoBarrasExiste =
                    await _produtoRepository
                        .CodigoBarrasExisteAsync(
                            produto.CodigoBarras);

                if (codigoBarrasExiste)
                {
                    throw new InvalidOperationException(
                        "Já existe um produto cadastrado com esse código de barras.");
                }
            }

            produto.DataCadastro = DateTime.Now;

            await _produtoRepository
                .AdicionarAsync(produto);
        }

        public async Task AtualizarAsync(
            Produto produto)
        {
            ValidarProduto(produto);

            if (await _produtoRepository
                .CodigoExisteAsync(
                    produto.Codigo,
                    produto.Id))
            {
                throw new InvalidOperationException(
                    "Já existe outro produto cadastrado com esse código.");
            }

            if (!string.IsNullOrWhiteSpace(
                produto.CodigoBarras))
            {
                bool codigoBarrasExiste =
                    await _produtoRepository
                        .CodigoBarrasExisteAsync(
                            produto.CodigoBarras,
                            produto.Id);

                if (codigoBarrasExiste)
                {
                    throw new InvalidOperationException(
                        "Já existe outro produto cadastrado com esse código de barras.");
                }
            }

            produto.DataAtualizacao = DateTime.Now;

            await _produtoRepository
                .AtualizarAsync(produto);
        }

        public async Task AlterarStatusAsync(
            int id,
            bool ativo)
        {
            await _produtoRepository
                .AlterarStatusAsync(id, ativo);
        }

        public async Task ExcluirAsync(int id)
        {
            await _produtoRepository
                .ExcluirAsync(id);
        }

        private static void ValidarProduto(
            Produto produto)
        {
            if (string.IsNullOrWhiteSpace(
                produto.Codigo))
            {
                throw new ArgumentException(
                    "Informe o código do produto.");
            }

            if (string.IsNullOrWhiteSpace(
                produto.Nome))
            {
                throw new ArgumentException(
                    "Informe o nome do produto.");
            }

            if (string.IsNullOrWhiteSpace(
                produto.UnidadeMedida))
            {
                throw new ArgumentException(
                    "Informe a unidade de medida.");
            }

            if (produto.PrecoCusto < 0)
            {
                throw new ArgumentException(
                    "O preço de custo não pode ser negativo.");
            }

            if (produto.PrecoVenda < 0)
            {
                throw new ArgumentException(
                    "O preço de venda não pode ser negativo.");
            }

            if (produto.EstoqueAtual < 0)
            {
                throw new ArgumentException(
                    "O estoque atual não pode ser negativo.");
            }

            if (produto.EstoqueMinimo < 0)
            {
                throw new ArgumentException(
                    "O estoque mínimo não pode ser negativo.");
            }
        }
    }
}