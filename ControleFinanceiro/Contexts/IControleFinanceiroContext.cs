using ControleFinanceiro.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Contexts
{
    public interface IControleFinanceiroContext
    {
        DbSet<Categoria> Categorias { get; set; }
        DbSet<Conta> Contas { get; set; }
        DbSet<Transacao> Transacoes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
