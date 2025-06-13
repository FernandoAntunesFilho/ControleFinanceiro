using ControleFinanceiro.Models.Entities;

namespace ControleFinanceiro.Repositories
{
    public interface IContaRepository
    {
        Task<List<Conta>> GetAll();
        Task<Conta?> GetById(int id);
        Task<int> Add(Conta categoria);
        Task<int> Update(Conta categoria);
        Task<int> Delete(Conta categoria);
    }
}
