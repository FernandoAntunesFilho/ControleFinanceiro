using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ContaController : Controller
{
    private static int _nextId = 1;
    private static List<ContaResponse> _contasResponse = new();

    [HttpGet]
    [Route("contas")]
    public IActionResult GetContas()
    {
        _contasResponse.Clear();
        foreach (var conta in ContaRepository.Contas)
        {
            var valorTransacoes = TransacaoRepository.Transacoes
            .Where(t => t.Conta != null && t.Conta.Id == conta.Id && t.Data.Date <= DateTime.Now.Date)
            .Sum(t => t.Valor);

            _contasResponse.Add(new ContaResponse
            {
                Conta = conta,
                SaldoAtual = conta.SaldoInicial + valorTransacoes
            });
        }


        return StatusCode(200, _contasResponse);
    }

    [HttpPost]
    [Route("conta")]
    public IActionResult PostConta(ContaRequest request)
    {
        Conta conta = request.CriarConta(_nextId++);
        ContaRepository.Contas.Add(conta);
        return StatusCode(201, conta);
    }

    [HttpPut]
    [Route("conta")]
    public IActionResult PutConta(int id, ContaRequest request)
    {
        var conta = ContaRepository.Contas.FirstOrDefault(c => c.Id == id);
        if (conta == null) return NotFound("Conta não encontrada");

        var contaAtualizada = request.AlterarConta(conta);

        return Ok(contaAtualizada);
    }
}