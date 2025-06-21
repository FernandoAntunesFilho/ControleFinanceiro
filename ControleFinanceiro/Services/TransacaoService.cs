using ControleFinanceiro.Models.DTOs;
using ControleFinanceiro.Models.Entities;
using ControleFinanceiro.Models.Enums;
using ControleFinanceiro.Repositories;

namespace ControleFinanceiro.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _repository;
        public TransacaoService(ITransacaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> AddAsync(TransacaoRequestAddDTO transacao)
        {
            ValidaTransacao(transacao.TipoTransacao, transacao.Valor);

            switch ((TipoTransacaoEnum)transacao.TipoTransacao)
            {
                case TipoTransacaoEnum.Debito:
                case TipoTransacaoEnum.Credito:
                    Transacao transacaoDebitoCredito = MapTransacaoDebitoCredito(transacao);
                    return await _repository.AddDebitoCredito(transacaoDebitoCredito);

                case TipoTransacaoEnum.Transferencia:
                    Transacao transacaoTransferenciaDebito, transacaoTransferenciaCredito;
                    MapTransacoesTransferencia(transacao, out transacaoTransferenciaDebito, out transacaoTransferenciaCredito);
                    return await _repository.AddTransferencia(transacaoTransferenciaDebito, transacaoTransferenciaCredito);

                default:
                    throw new ArgumentOutOfRangeException("Tipo de transação não é suportado.");
            }
        }        

        public async Task<int> DeleteAsync(int id)
        {
            var transacao = await _repository.GetById(id);
            if (transacao == null) throw new ArgumentNullException("Não é possível apagar a(s) transação(ões)");

            var transacoes = new List<Transacao>();

            switch ((TipoTransacaoEnum)transacao.TipoTransacao)
            {
                case TipoTransacaoEnum.Transferencia:
                    transacoes = await _repository.GetByTransferenciaId((Guid)transacao.TransferenciaId!);
                    break;

                default:
                    transacoes.Add(transacao);
                    break;
            }

            return await _repository.DeleteDebitoCredito(transacoes);
        }

        public async Task<List<object>> GetAsync(DateTime dataInicial, DateTime dataFinal)
        {
            var transacoesSaldoAnterior = await _repository.GetSaldoAnterior(dataInicial.AddDays(-1));

            var transacoesDebitoCredito = await _repository.GetDebitoCredito(dataInicial, dataFinal);
            var transacoesDebitoCreditoMapped = transacoesDebitoCredito.Select(t => new TransacaoDebitoCreditoDTO
            {
                Id = t.Id,
                ContaId = t.ContaId,
                Valor = t.Valor,
                DataTransacao = t.Data,
                Consolidada = t.Consolidada,
                DataOriginal = t.DataOriginal,
                Descricao = t.Descricao,
                TipoTransacao = t.TipoTransacao,
                CategoriaId = t.CategoriaId
            });

            var transacoesTransferencia = await _repository.GetTransferencia(dataInicial, dataFinal);
            var transacoesTransferenciaMapped = transacoesTransferencia.Select(t => new TransacaoTransferenciaDTO
            {
                Id = t.Id,
                ContaId = t.ContaId,
                Valor = t.Valor,
                DataTransacao = t.Data,
                Consolidada = t.Consolidada,
                DataOriginal = t.DataOriginal,
                Descricao = t.Descricao,
                TipoTransacao = t.TipoTransacao,
                CategoriaId = t.CategoriaId,
                ContaDestinoId = (int)t.ContaDestinoId!,
                TransferenciaId = (Guid)t.TransferenciaId!
            });

            var todasTransacoes = new List<object>();
            todasTransacoes.AddRange(transacoesSaldoAnterior);
            todasTransacoes.AddRange(transacoesDebitoCreditoMapped);
            todasTransacoes.AddRange(transacoesTransferenciaMapped);

            return todasTransacoes;
        }

        public async Task<int> UpdateAsync(TransacaoRequestDTO transacao)
        {
            ValidaTransacao(transacao.TipoTransacao, transacao.Valor);

            var transacaoExists = await _repository.GetById(transacao.Id);
            if (transacaoExists == null) throw new ArgumentNullException("Não é possível atualizar a transação");

            switch ((TipoTransacaoEnum)transacao.TipoTransacao)
            {
                case TipoTransacaoEnum.Debito:
                case TipoTransacaoEnum.Credito:
                    transacaoExists.ContaId = transacao.ContaId;
                    transacaoExists.Valor = transacao.Valor;
                    transacaoExists.Data = transacao.Data;
                    transacaoExists.Descricao = transacao.Descricao;
                    transacaoExists.TipoTransacao = transacao.TipoTransacao;
                    transacaoExists.CategoriaId = transacao.CategoriaId;
                    transacaoExists.Consolidada = transacao.Consolidada;

                    return await _repository.UpdateDebitoCredito(transacaoExists);

                case TipoTransacaoEnum.Transferencia:
                    var transacoesExists = await _repository.GetByTransferenciaId(transacao.TransferenciaId);
                    if (transacoesExists.Count != 2) throw new ArgumentNullException("Não é possível atualizar a transação");

                    var transacaoPrincipal = transacoesExists.First(t => t.Id == transacao.Id);
                    var transacaoSecundaria = transacoesExists.First(t => t.Id != transacao.Id);

                    transacaoPrincipal.ContaDestinoId = transacao.ContaDestinoId;
                    transacaoPrincipal.ContaId = transacao.ContaId;
                    transacaoPrincipal.Valor = transacao.Valor;
                    transacaoPrincipal.Data = transacao.Data;
                    transacaoPrincipal.Descricao = transacao.Descricao;
                    transacaoPrincipal.TipoTransacao = transacao.TipoTransacao;
                    transacaoPrincipal.CategoriaId = transacao.CategoriaId;
                    transacaoPrincipal.Consolidada = transacao.Consolidada;

                    transacaoSecundaria.ContaDestinoId = transacao.ContaId;
                    transacaoSecundaria.ContaId = transacao.ContaDestinoId;
                    transacaoSecundaria.Valor = transacao.Valor * -1;
                    transacaoSecundaria.Data = transacao.Data;
                    transacaoSecundaria.Descricao = transacao.Descricao;
                    transacaoSecundaria.TipoTransacao = transacao.TipoTransacao;
                    transacaoSecundaria.CategoriaId = transacao.CategoriaId;
                    transacaoSecundaria.Consolidada = transacao.Consolidada;

                    return await _repository.UpdateTransferencia(transacaoPrincipal, transacaoSecundaria);

                default:
                    throw new ArgumentOutOfRangeException("Tipo de transação não é suportado.");
            }
        }

        private static Transacao MapTransacaoDebitoCredito(TransacaoRequestAddDTO transacao)
        {
            return new Transacao
            {
                ContaId = transacao.ContaId,
                Valor = transacao.Valor,
                Data = transacao.Data,
                DataOriginal = transacao.Data,
                Descricao = transacao.Descricao,
                TipoTransacao = transacao.TipoTransacao,
                CategoriaId = transacao.CategoriaId,
                Consolidada = transacao.Consolidada,
            };
        }

        private static void MapTransacoesTransferencia(TransacaoRequestAddDTO transacao, out Transacao transacaoTransferenciaDebito, out Transacao transacaoTransferenciaCredito)
        {
            var transferenciaId = Guid.NewGuid();

            transacaoTransferenciaDebito = new Transacao
            {
                ContaDestinoId = transacao.ContaDestinoId,
                ContaId = transacao.ContaId,
                Valor = transacao.Valor,
                TransferenciaId = transferenciaId,
                Data = transacao.Data,
                DataOriginal = transacao.Data,
                Descricao = transacao.Descricao,
                TipoTransacao = transacao.TipoTransacao,
                CategoriaId = transacao.CategoriaId,
                Consolidada = transacao.Consolidada,
            };
            transacaoTransferenciaCredito = new Transacao
            {
                ContaDestinoId = transacao.ContaId,
                ContaId = transacao.ContaDestinoId,
                Valor = transacao.Valor * -1,
                TransferenciaId = transferenciaId,
                Data = transacao.Data,
                DataOriginal = transacao.Data,
                Descricao = transacao.Descricao,
                TipoTransacao = transacao.TipoTransacao,
                CategoriaId = transacao.CategoriaId,
                Consolidada = transacao.Consolidada,
            };
        }

        private static void ValidaTransacao(int tipoTransacao, decimal valor)
        {
            if ((TipoTransacaoEnum)tipoTransacao == TipoTransacaoEnum.Debito && valor > 0)
                throw new InvalidOperationException("Valor de débito não pode ser positivo.");

            if ((TipoTransacaoEnum)tipoTransacao == TipoTransacaoEnum.Credito && valor < 0)
                throw new InvalidOperationException("Valor de crédito não pode ser negativo.");
        }
    }
}