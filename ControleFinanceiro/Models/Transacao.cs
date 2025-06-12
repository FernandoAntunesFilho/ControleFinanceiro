using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleFinanceiro.Models
{
    public class Transacao
    {
        [Key]
        public int Id { get; set; }
        
        public int? ContaDestinoId { get; set; }
        [ForeignKey(nameof(ContaDestinoId))]
        public Conta? ContaDestino { get; set; }

        public int ContaId { get; set; }
        [ForeignKey(nameof(ContaId))]
        public Conta? Conta { get; set; }

        public decimal Valor { get; set; }
        public Guid? TransferenciaId { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataOriginal { get; set; }
        public string? Descricao { get; set; }

        public int TipoTransacao { get; set; }

        public int CategoriaId { get; set; }
        [ForeignKey(nameof(CategoriaId))]
        public Categoria? Categoria { get; set; }

        public bool Consolidada { get; set; }
    }
}
