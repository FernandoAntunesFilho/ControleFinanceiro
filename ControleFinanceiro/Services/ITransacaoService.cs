using ControleFinanceiro.Models.DTOs;

namespace ControleFinanceiro.Services
{
    public interface ITransacaoService
    {
        Task<List<ITransacaoDTO>> GetAsync(DateTime dataInicial, DateTime dataFinal);
        Task<int> AddAsync(TransacaoRequestDTO transacoes);
        Task<int> UpdateAsync(TransacaoRequestDTO transacoes);
        Task<int> DeleteAsync(List<int> transacoesId);
    }
}
