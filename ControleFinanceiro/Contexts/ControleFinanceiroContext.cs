using ControleFinanceiro.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Contexts
{
    public class ControleFinanceiroContext : DbContext
    {
        public ControleFinanceiroContext(DbContextOptions<ControleFinanceiroContext> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
    }
}
