using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Models.Entities
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }

        public ICollection<Transacao>? Transacoes { get; set; }
    }
}
