using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ContaController : Controller
{
    private static int _nextId = 1;
    
    [HttpGet]
    [Route("contas")]
    public IActionResult GetContas()
    {
        return StatusCode(200, ContaRepository.Contas);
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