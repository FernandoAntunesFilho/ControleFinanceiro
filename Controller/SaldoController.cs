using Microsoft.AspNetCore.Mvc;

[ApiController]
public class SaldoController : Controller
{
    //private List<Saldo> _saldos = new();

    [HttpGet]
    [Route("transacoes/saldos")]
    public IActionResult GetSaldoTransacoes(int mes, int ano, int contaId)
    {
        var conta = ContaRepository.Contas.FirstOrDefault(c => c.Id == contaId);
        if (conta == null) return NotFound("Conta não encontrada");
        if (mes < 1 && mes > 12) return NotFound("Mês inválido");

        var saldoInicial = conta.SaldoInicial;

        var saldos = TransacaoRepository.Transacoes
        .Where(t => t.Conta != null && t.Conta.Id == contaId) // Filtra pelo ID da conta
        .GroupBy(t => t.Data.Date) // Agrupa por data, ignorando a hora
        .Select(g => new Saldo
        {
            Data = g.Key,
            SaldoDia = g.Sum(t => t.Valor) // Soma os valores das transações do dia
        })
        .OrderBy(s => s.Data) // Ordena por data para garantir a sequência correta
        .ToList();

        // Aplicando o saldo acumulado
        decimal saldoAcumulado = saldoInicial;
        foreach (var saldo in saldos)
        {
            saldoAcumulado += saldo.SaldoDia;
            saldo.SaldoDia = saldoAcumulado; // Atualiza o SaldoDia com o acumulado
        }

        var ultimoDia = DateTime.DaysInMonth(ano, mes);
        return Ok(saldos.Where(c => c.Data.Date >= new DateTime(ano, mes, 1) &&
            c.Data.Date <= new DateTime(ano, mes, ultimoDia)));
    }
}