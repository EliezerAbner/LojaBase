using System.Security.Cryptography;
using System.Text;

namespace LojaBase.Services
{
    //Responsável por encriptar a senha utilizada no login

    public class SenhaService
    {
        public string HashSenha(string senha)
        {
            SHA256 hash = SHA256.Create();
            var bytes = Encoding.Default.GetBytes(senha);

            var senhahashed = hash.ComputeHash(bytes);

            return Convert.ToHexString(senhahashed);
        }
    }
}
