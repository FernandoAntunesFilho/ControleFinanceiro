using ControleFinanceiro.Contexts;
using ControleFinanceiro.Models.DTOs;
using ControleFinanceiro.Models.Entities;
using ControleFinanceiro.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Repositories
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly IControleFinanceiroContext _context;

        public TransacaoRepository(IControleFinanceiroContext context)
        {
            _context = context;
        }

        public async Task<int> AddDebitoCredito(Transacao transacao)
        {
            await _context.Transacoes.AddAsync(transacao);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddTransferencia(Transacao transacaoDebito, Transacao transacaoCredito)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _context.Transacoes.AddAsync(transacaoDebito);
                await _context.Transacoes.AddAsync(transacaoCredito);
                int result = await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new DbUpdateException("Falha ao gravar a transação");
            }
        }

        public async Task<int> DeleteDebitoCredito(List<Transacao> transacoes)
        {
            _context.Transacoes.RemoveRange(transacoes);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteTransferencia(Transacao transacaoDebito, Transacao transacaoCredito)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Transacoes.Remove(transacaoDebito);
                _context.Transacoes.Remove(transacaoCredito);
                int result = await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new DbUpdateException("Falha ao apagar a transação");
            }
        }

        public async Task<Transacao?> GetById(int id)
        {
            return await _context.Transacoes.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Transacao>> GetByTransferenciaId(Guid transferenciaId)
        {
            return await _context.Transacoes.Where(t => t.TransferenciaId == transferenciaId).ToListAsync();
        }

        public async Task<List<Transacao>> GetDebitoCredito(DateTime dataInicio, DateTime dataFim)
        {
            return await _context.Transacoes.Where(t => t.Data.Date >= dataInicio.Date &&
                                                        t.Data.Date <= dataFim &&
                                                        t.TipoTransacao != ((int)TipoTransacaoEnum.Transferencia))
                .ToListAsync();
        }

        public async Task<List<Transacao>> GetTransferencia(DateTime dataInicio, DateTime dataFim)
        {
            return await _context.Transacoes.Where(t => t.Data.Date >= dataInicio.Date &&
                                                        t.Data.Date <= dataFim &&
                                                        t.TipoTransacao == ((int)TipoTransacaoEnum.Transferencia))
                .ToListAsync();
        }

        public async Task<List<TransacaoSaldoAnteriorDTO>> GetSaldoAnterior(DateTime dataFim)
        {
            var resultado = await _context.Contas
                .GroupJoin(
                _context.Transacoes.Where(t => t.Data <= dataFim),
                conta => conta.Id,
                transacao => transacao.ContaId,
                (conta, transacoes) => new TransacaoSaldoAnteriorDTO
                {
                    Id = conta.Id * -1,
                    ContaId = conta.Id,
                    Valor = transacoes.Sum(t => t.Valor),
                    DataTransacao = dataFim,
                    Consolidada = true
                })
                .ToListAsync();

            return resultado;
        }

        public async Task<int> UpdateDebitoCredito(Transacao transacao)
        {
            _context.Transacoes.Update(transacao);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateTransferencia(Transacao transacaoDebito, Transacao transacaoCredito)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Transacoes.Update(transacaoDebito);
                _context.Transacoes.Update(transacaoCredito);
                int result = await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new DbUpdateException("Falha ao atualizar a transação");
            }
        }
    }
}
