using LojaBase.DAL;
using System.ComponentModel.DataAnnotations;

namespace LojaBase.CustomValidators
{
    public class CpfValidatorAtribute : ValidationAttribute
    {
        public string DefaultErrorMessage { get; set; } = "CPF já registrado no sistema";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null) 
            {
                UsuarioDB db = new UsuarioDB();
                
                if (!db.VerificaCpf(Convert.ToString(value)))
                {
                    return ValidationResult.Success;
                }
                else 
                {
                    return new ValidationResult(DefaultErrorMessage);
                }
            }
            else
            {
                return null;
            }
        }
    }
}
