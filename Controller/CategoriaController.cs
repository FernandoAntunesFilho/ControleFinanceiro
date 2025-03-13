using Microsoft.AspNetCore.Mvc;

[ApiController]
public class CategoriaController : Controller
{
    private static int _nextId = 1;

    [HttpGet]
    [Route("categorias")]
    public IActionResult GetCategorias()
    {
        return Ok(CategoriaRepository.Categorias);
    }
}