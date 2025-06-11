using ControleFinanceiro.Contexts;
using ControleFinanceiro.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private readonly IControleFinanceiroContext _context;
        public ContaRepository(IControleFinanceiroContext context)
        {
            _context = context;
        }

        public async Task<int> Add(Conta conta)
        {
            await _context.Contas.AddAsync(conta);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Conta conta)
        {
            _context.Contas.Remove(conta);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Conta>> GetAll()
        {
            return await _context.Contas.ToListAsync();
        }

        public async Task<Conta?> GetById(int id)
        {
            return await _context.Contas.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int> Update(Conta conta)
        {
            _context.Contas.Update(conta);
            return await _context.SaveChangesAsync();
        }
    }
}
