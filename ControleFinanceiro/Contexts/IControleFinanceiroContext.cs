using ControleFinanceiro.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Contexts
{
    public interface IControleFinanceiroContext
    {
        DbSet<Categoria> Categorias { get; set; }
        DbSet<Conta> Contas { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
