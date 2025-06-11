using ControleFinanceiro.DTOs;
using ControleFinanceiro.Models;
using ControleFinanceiro.Repositories;

namespace ControleFinanceiro.Services
{
    public class ContaService : IContaService
    {
        private readonly IContaRepository _repository;
        private const string contaNotFoundMessage = "Conta não encontrada.";
        private const string ContaNameRequiredMessage = "O nome da conta é obrigatório.";
        public ContaService(IContaRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> AddAsync(ContaRequestDTO conta)
        {
            if (string.IsNullOrWhiteSpace(conta.Nome)) throw new Exception(ContaNameRequiredMessage);

            var novaConta = new Conta
            {
                Nome = conta.Nome,
                ValorInicial = conta.ValorInicial,
                Ativo = conta.Ativo,
            };

            return await _repository.Add(novaConta);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var contaExistente = await _repository.GetById(id);
            if (contaExistente == null) throw new Exception(contaNotFoundMessage);

            return await _repository.Delete(contaExistente);
        }

        public async Task<List<Conta>> GetAllAsync()
        {
            return await _repository.GetAll();
        }

        public async Task<int> UpdateAsync(Conta conta)
        {
            var contaExistente = await _repository.GetById(conta.Id);
            if (contaExistente == null) throw new Exception(contaNotFoundMessage);

            contaExistente.Nome = conta.Nome;
            contaExistente.Ativo = conta.Ativo;
            contaExistente.ValorInicial = conta.ValorInicial;

            return await _repository.Update(contaExistente);
        }
    }
}
