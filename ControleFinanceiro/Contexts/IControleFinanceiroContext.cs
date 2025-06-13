using ControleFinanceiro.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ControleFinanceiro.Contexts
{
    public interface IControleFinanceiroContext
    {
        DbSet<Categoria> Categorias { get; set; }
        DbSet<Conta> Contas { get; set; }
        DbSet<Transacao> Transacoes { get; set; }
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
