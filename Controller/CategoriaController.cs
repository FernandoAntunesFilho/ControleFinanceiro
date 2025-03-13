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

    [HttpPost]
    [Route("categoria")]
    public IActionResult PostCategoria(CategoriaRequest request)
    {
        var novaCategoria = request.CriarCategoria(_nextId++);

        CategoriaRepository.Categorias.Add(novaCategoria);

        return Ok(novaCategoria);
    }

    [HttpPut]
    [Route("categoria")]
    public IActionResult PutCategoria(int id, CategoriaRequest request)
    {
        var categoria = CategoriaRepository.Categorias.FirstOrDefault(c => c.Id == id);
        if (categoria == null) return NotFound("Categoria não encontrada");

        var categoriaAtualizada = request.AtualizarCategoria(id, categoria);

        return Ok(categoriaAtualizada);
    }
}