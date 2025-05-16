using System.ComponentModel.DataAnnotations;
using LojaBase.CustomValidators;

namespace LojaBase.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        
        [Required]
        public string? Nome { get; set; }

        [Required]
        [CpfValidatorAtribute]
        public string? Cpf { get; set; }

        [Required]
        public int Adm { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        [Phone(ErrorMessage = "Insira um número de telefone válido")]
        public string? Telefone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Insira um email válido")]
        [EmailValidatorAtribute]
        public string? Email { get; set; }
        
        [Required]
        public string? Senha { get; set; }

        [Required]
        [Compare("Senha", ErrorMessage = "As senhas não batem.")]
        public string? ConfirmacaoSenha { get; set; }
    }
}
