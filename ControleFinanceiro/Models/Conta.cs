using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Models
{
    public class Conta
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public decimal ValorInicial { get; set; }
        public bool Ativo {  get; set; }
    }
}
