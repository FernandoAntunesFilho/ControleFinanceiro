namespace ControleFinanceiro.Models.DTOs
{
    public class TransacaoDebitoCreditoDTO : TransacaoSaldoAnteriorDTO
    {
        public DateTime DataOriginal { get; set; }
        public string? Descricao { get; set; }
        public char? TipoTransacao { get; set; }
        public int CategoriaId { get; set; }
    }
}
