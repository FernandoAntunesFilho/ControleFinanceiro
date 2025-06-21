using ControleFinanceiro.Models.DTOs;

namespace ControleFinanceiro.Services
{
    public interface ITransacaoService
    {
        Task<List<object>> GetAsync(DateTime dataInicial, DateTime dataFinal);
        Task<int> AddAsync(TransacaoRequestAddDTO transacoes);
        Task<int> UpdateAsync(TransacaoRequestDTO transacoes);
        Task<int> DeleteAsync(int id);
    }
}
