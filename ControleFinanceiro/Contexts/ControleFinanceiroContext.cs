using ControleFinanceiro.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Contexts
{
    public class ControleFinanceiroContext : DbContext, IControleFinanceiroContext
    {
        public ControleFinanceiroContext(DbContextOptions<ControleFinanceiroContext> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
    }
}
