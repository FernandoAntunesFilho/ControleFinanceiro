using ControleFinanceiro.DTOs;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.Services
{
    public interface IContaService
    {
        Task<List<Conta>> GetAllAsync();
        Task<int> AddAsync(ContaRequestDTO conta);
        Task<int> UpdateAsync(Conta conta);
        Task<int> DeleteAsync(int id);
    }
}
