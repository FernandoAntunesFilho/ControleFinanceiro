public class CategoriaRequest
{
    public string? Nome { get; set; }

    public Categoria CriarCategoria(int id)
    {
        return new Categoria()
        {
            Id = id,
            Nome = Nome
        };
    }
}