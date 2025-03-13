using Microsoft.AspNetCore.Mvc;

[ApiController]
public class TransacaoController : Controller
{
    private static int _nextId = 1;

    [HttpGet]
    [Route("transacoes")]
    public IActionResult GetTransacoes(int mes, int ano)
    {
        if (mes < 1 || mes > 12) return StatusCode(404, "Mês Inválido");

        return StatusCode(200, TransacaoRepository.Transacoes);
    }

    [HttpPost]
    [Route("transacao")]
    public IActionResult PostTransacao(TransacaoRequest request)
    {
        var conta = EncontrarConta(request);
        if (conta == null) return NotFound("Conta não encontrada");

        Transacao novaTransacao = request.CriarTransacao(_nextId++, conta);
        TransacaoRepository.Transacoes.Add(novaTransacao);

        return StatusCode(201, novaTransacao);
    }    

    [HttpPut]
    [Route("transacao")]
    public IActionResult PutTransacao(int id, TransacaoRequest request)
    {
        var transacao = TransacaoRepository.Transacoes.FirstOrDefault(c => c.Id == id);
        var conta = EncontrarConta(request);
        if (transacao == null) return NotFound("Transação não encontrada");
        if (conta == null) return NotFound("Conta não encontrada");

        var transacaoAtualizada = request.AtualizarTransacao(id, conta, transacao);

        return Ok(transacaoAtualizada);
    }

    private static Conta? EncontrarConta(TransacaoRequest request)
    {
        return ContaRepository.Contas.FirstOrDefault(c => c.Id == request.ContaId);
    }
}