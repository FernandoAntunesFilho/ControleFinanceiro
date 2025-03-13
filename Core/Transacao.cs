public class Transacao
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public string? Descricao { get; set; }
    public string? Categoria { get; set; }
    public Conta? Conta { get; set; }
    public decimal Valor { get; set; }
    public bool Consolidado { get; set; }
}