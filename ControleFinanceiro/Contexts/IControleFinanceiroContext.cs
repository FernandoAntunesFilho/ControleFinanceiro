using ControleFinanceiro.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Contexts
{
    public interface IControleFinanceiroContext
    {
        DbSet<Categoria> Categorias { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
