using System.ComponentModel.DataAnnotations;

namespace LojaBase.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        
        [Required]
        public string? Nome { get; set; }
        
        [Required]
        public string? Icone { get; set; }
        
        public string? Descricao { get; set; }
    }
}
