namespace ControleFinanceiro.Models.DTOs
{
    public abstract class TransacaoBaseDTO
    {
        public int Id { get; set; }
        public int ContaId { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataTransacao { get; set; }
        public bool Consolidada { get; set; }
    }
}
