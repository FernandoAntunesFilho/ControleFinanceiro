namespace ControleFinanceiro.Models.DTOs
{
    public interface ITransacaoDTO
    {
        int Id { get; set; }
        int ContaId { get; set; }
        decimal Valor { get; set; }
        DateTime DataTransacao { get; set; }
        bool Consolidada { get; set; }
    }
}
