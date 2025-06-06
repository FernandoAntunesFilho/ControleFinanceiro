using ControleFinanceiro.DTOs;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.Services
{
    public interface ICategoriaService
    {
        Task<List<Categoria>> GetAllAsync();
        Task<int> AddAsync(CategoriaRequestDTO categoria);
        Task<int> UpdateAsync(Categoria categoria);
        Task<int> DeleteAsync(int id);
    }
}
