using ControleFinanceiro.Models;

namespace ControleFinanceiro.Repositories
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> GetAll();
        Task<int> Add(Categoria categoria);
        Task<int> Update(Categoria categoria);
        Task<int> Delete(Categoria categoria);
    }
}
