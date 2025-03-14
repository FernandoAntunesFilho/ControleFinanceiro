using Microsoft.AspNetCore.Mvc;

[ApiController]
public class SaldoController : Controller
{
    [HttpGet]
    [Route("transacoes/saldos")]
    public IActionResult GetSaldoTransacoes(int mes, int ano, int contaId)
    {
        var conta = ContaRepository.Contas.FirstOrDefault(c => c.Id == contaId);
        if (conta == null) return NotFound("Conta não encontrada");
        if (mes < 1 && mes > 12) return NotFound("Mês inválido");

        var saldoInicial = conta.SaldoInicial;

        var saldos = TransacaoRepository.Transacoes
        .Where(t => t.Conta != null && t.Conta.Id == contaId)
        .GroupBy(t => t.Data.Date)
        .Select(g => new Saldo
        {
            Data = g.Key,
            SaldoDia = g.Sum(t => t.Valor)
        })
        .OrderBy(s => s.Data)
        .ToList();
        
        decimal saldoAcumulado = saldoInicial;
        foreach (var saldo in saldos)
        {
            saldoAcumulado += saldo.SaldoDia;
            saldo.SaldoDia = saldoAcumulado;
        }

        var ultimoDia = DateTime.DaysInMonth(ano, mes);
        return Ok(saldos.Where(c => c.Data.Date >= new DateTime(ano, mes, 1) &&
            c.Data.Date <= new DateTime(ano, mes, ultimoDia)));
    }
}