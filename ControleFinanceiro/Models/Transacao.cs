using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiro.Models
{
    public class Transacao
    {
        [Key]
        public int Id { get; set; }
        public int TipoTransacao { get; set; }
        public DateTime Data { get; set; }
        public string? Descricao { get; set; }
        public decimal Valor { get; set; }

        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        [ForeignKey("Conta")]
        public int ContaId { get; set; }
        public Conta? Conta { get; set; }
    }
}
