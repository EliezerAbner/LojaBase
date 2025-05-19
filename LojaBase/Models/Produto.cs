using System.ComponentModel.DataAnnotations;

namespace LojaBase.Models
{
    public class Produto
    {
        public int Id { get; set; }
        
        [Required]
        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public string? TipoQuantidade { get; set; }

        public double Quantidade { get; set; }
        public string? Imagem { get; set; }
        
        [Required]
        public bool Status { get; set; }

        [Required]
        public decimal Preco { get; set; }
    }
}
