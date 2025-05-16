using LojaBase.DAL;
using System.ComponentModel.DataAnnotations;

namespace LojaBase.CustomValidators
{
    public class EmailValidatorAtribute : ValidationAttribute
    {
        public string DefaultErrorMessage = "Email já registrado no sistema";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                UsuarioDB db = new UsuarioDB();

                if (!db.VerificaEmail(Convert.ToString(value)))
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
