using ControleFinanceiro.Contexts;
using ControleFinanceiro.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly IControleFinanceiroContext _context;
        public CategoriaRepository(IControleFinanceiroContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
            return await _context.SaveChangesAsync();
        }
        
        public async Task<List<Categoria>> GetAll()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categoria?> GetById(int id)
        {
            return await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int> Update(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            return await _context.SaveChangesAsync();
        }
    }
}
