using ControleFinanceiro.Models.DTOs;
using ControleFinanceiro.Models.Entities;

namespace ControleFinanceiro.Repositories
{
    public interface ITransacaoRepository
    {
        Task<Transacao?> GetById(int id);
        Task<List<TransacaoSaldoAnteriorDTO>> GetSaldoAnterior(DateTime dataFim);
        Task<List<Transacao>> GetDebitoCredito(DateTime dataInicio, DateTime dataFim);
        Task<int> AddDebitoCredito(Transacao transacao);
        Task<int> UpdateDebitoCredito(Transacao transacao);
        Task<int> DeleteDebitoCredito(List<Transacao> transacoes);
        Task<List<Transacao>> GetTransferencia(DateTime dataInicio, DateTime dataFim);
        Task<int> AddTransferencia(Transacao transacaoDebito, Transacao transacaoCredito);
        Task<int> UpdateTransferencia(Transacao transacaoDebito, Transacao transacaoCredito);
        Task<int> DeleteTransferencia(Transacao transacaoDebito, Transacao transacaoCredito);
        Task<List<Transacao>> GetByTransferenciaId(Guid transferenciaId);
    }
}
