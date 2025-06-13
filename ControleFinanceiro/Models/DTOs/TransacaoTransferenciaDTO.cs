namespace ControleFinanceiro.Models.DTOs
{
    public class TransacaoTransferenciaDTO : TransacaoDebitoCreditoDTO
    {
        public int ContaDestinoId { get; set; }
        public Guid TransferenciaId { get; set; }
    }
}