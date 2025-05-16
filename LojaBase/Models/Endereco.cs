using System.ComponentModel.DataAnnotations;

namespace LojaBase.Models
{
    public class Endereco
    {
        public int EnderecoId { get; set; }

        [Required]
        public string? Cep { get; set; }

        [Required]
        public string? Logradouro {  get; set; }
        public int Numero { get; set; }
        public string? Complemento { get; set; }
        public string? Unidade { get; set; }

        [Required]
        public string? Bairro { get; set; }

        [Required]
        public string? Localidade { get; set; }

        [Required]
        public string? Uf { get; set; }
        public int EstadoId { get; set; }
        public string? Estado { get; set; }
        public string? Regiao { get; set; }
        public string? Ibge { get; set; }
        public string? Gia { get; set; }
        public string? DDD { get; set; }
        public string? Siafi { get; set; }
    }
}
