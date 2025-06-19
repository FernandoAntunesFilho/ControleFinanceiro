namespace ControleFinanceiro.Models.DTOs
{
    public class TransacaoRequestDTO
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        public bool Consolidada { get; set; }
        public int ContaDestinoId { get; set; }
        public int ContaId { get; set; }
        public DateTime Data { get; set; }
        public string? Descricao { get; set; }
        public int TipoTransacao { get; set; }
        public decimal Valor { get; set; }
        public Guid TransferenciaId { get; set; }
    }
}
