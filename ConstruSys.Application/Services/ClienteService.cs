using ConstruSys.Application.Helpers;
using ConstruSys.Domain.Entities;
using ConstruSys.Domain.Interfaces;

namespace ConstruSys.Application.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(
            IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<List<Cliente>> ObterTodosAsync()
        {
            return await _clienteRepository
                .ObterTodosAsync();
        }

        public async Task<List<Cliente>> ObterExcluidosAsync()
        {
            return await _clienteRepository
                .ObterExcluidosAsync();
        }

        public async Task<Cliente?> ObterPorIdAsync(
            int id)
        {
            return await _clienteRepository
                .ObterPorIdAsync(id);
        }

        public async Task<Cliente?> ObterExcluidoPorIdAsync(
            int id)
        {
            return await _clienteRepository
                .ObterExcluidoPorIdAsync(id);
        }

        public async Task<List<Cliente>> PesquisarAsync(
            string termo)
        {
            return await _clienteRepository
                .PesquisarAsync(termo);
        }

        public async Task AdicionarAsync(
            Cliente cliente)
        {
            Normalizar(cliente);

            Validar(cliente);

            if (!string.IsNullOrWhiteSpace(
                cliente.CpfCnpj))
            {
                bool existe =
                    await _clienteRepository
                        .CpfCnpjExisteAsync(
                            cliente.CpfCnpj);

                if (existe)
                {
                    throw new InvalidOperationException(
                        "Já existe um cliente, inclusive na lixeira, com este CPF/CNPJ.");
                }
            }

            cliente.DataCadastro =
                DateTime.Now;

            cliente.Excluido =
                false;

            await _clienteRepository
                .AdicionarAsync(cliente);
        }

        public async Task AtualizarAsync(
            Cliente cliente)
        {
            Normalizar(cliente);

            Validar(cliente);

            if (!string.IsNullOrWhiteSpace(
                cliente.CpfCnpj))
            {
                bool existe =
                    await _clienteRepository
                        .CpfCnpjExisteAsync(
                            cliente.CpfCnpj,
                            cliente.Id);

                if (existe)
                {
                    throw new InvalidOperationException(
                        "Já existe outro cliente com este CPF/CNPJ.");
                }
            }

            cliente.DataAtualizacao =
                DateTime.Now;

            await _clienteRepository
                .AtualizarAsync(cliente);
        }

        public async Task AlterarStatusAsync(
            int id,
            bool ativo)
        {
            await _clienteRepository
                .AlterarStatusAsync(
                    id,
                    ativo);
        }

        public async Task ExcluirAsync(
            int id)
        {
            await _clienteRepository
                .ExcluirAsync(id);
        }

        public async Task RestaurarAsync(
            int id)
        {
            await _clienteRepository
                .RestaurarAsync(id);
        }

        public async Task ExcluirDefinitivamenteAsync(
            int id)
        {
            await _clienteRepository
                .ExcluirDefinitivamenteAsync(id);
        }

        private static void Normalizar(
            Cliente cliente)
        {
            cliente.TipoPessoa =
                TextoHelper.CaixaAlta(
                    cliente.TipoPessoa);

            cliente.NomeRazaoSocial =
                TextoHelper.CaixaAlta(
                    cliente.NomeRazaoSocial);

            cliente.NomeFantasia =
                TextoHelper.CaixaAltaOuNull(
                    cliente.NomeFantasia);

            cliente.CpfCnpj =
                TextoHelper.ApenasTrimOuNull(
                    cliente.CpfCnpj);

            cliente.RgIe =
                TextoHelper.CaixaAltaOuNull(
                    cliente.RgIe);

            cliente.Telefone =
                TextoHelper.ApenasTrimOuNull(
                    cliente.Telefone);

            cliente.WhatsApp =
                TextoHelper.ApenasTrimOuNull(
                    cliente.WhatsApp);

            cliente.Email =
                TextoHelper.MinusculoOuNull(
                    cliente.Email);

            cliente.Cep =
                TextoHelper.ApenasTrimOuNull(
                    cliente.Cep);

            cliente.Endereco =
                TextoHelper.CaixaAltaOuNull(
                    cliente.Endereco);

            cliente.Numero =
                TextoHelper.CaixaAltaOuNull(
                    cliente.Numero);

            cliente.Complemento =
                TextoHelper.CaixaAltaOuNull(
                    cliente.Complemento);

            cliente.Bairro =
                TextoHelper.CaixaAltaOuNull(
                    cliente.Bairro);

            cliente.Cidade =
                TextoHelper.CaixaAltaOuNull(
                    cliente.Cidade);

            cliente.Estado =
                TextoHelper.CaixaAltaOuNull(
                    cliente.Estado);

            cliente.Observacoes =
                TextoHelper.CaixaAltaOuNull(
                    cliente.Observacoes);
        }

        private static void Validar(
            Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(
                cliente.TipoPessoa))
            {
                throw new ArgumentException(
                    "Informe o tipo de pessoa.");
            }

            if (string.IsNullOrWhiteSpace(
                cliente.NomeRazaoSocial))
            {
                throw new ArgumentException(
                    "Informe o nome ou razão social.");
            }

            if (cliente.LimiteCredito < 0)
            {
                throw new ArgumentException(
                    "O limite de crédito não pode ser negativo.");
            }
        }
    }
}