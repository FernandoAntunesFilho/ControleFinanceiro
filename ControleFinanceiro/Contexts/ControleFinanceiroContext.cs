using ControleFinanceiro.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Contexts
{
    public class ControleFinanceiroContext : DbContext, IControleFinanceiroContext
    {
        public ControleFinanceiroContext(DbContextOptions<ControleFinanceiroContext> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
    }
}
