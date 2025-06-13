namespace ControleFinanceiro.Models.DTOs
{
    public class ContaRequestDTO
    {
        public string? Nome { get; set; }
        public decimal ValorInicial { get; set; }
        public bool Ativo { get; set; }
    }
}
