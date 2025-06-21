namespace ControleFinanceiro.Models.DTOs
{
    public class TransacaoDebitoCreditoDTO : TransacaoBaseDTO
    {
        public DateTime DataOriginal { get; set; }
        public string? Descricao { get; set; }
        public int TipoTransacao { get; set; }
        public int CategoriaId { get; set; }
    }
}
