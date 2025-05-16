using LojaBase.Models;
using LojaBase.DAL;
using LojaBase.Services;
using Microsoft.AspNetCore.Mvc;

namespace LojaBase.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public async Task <IActionResult> Index()
        {

            Usuario userTeste = new Usuario()
            {
                Adm = 1,
                Nome = "Teste da Silva 2",
                Cpf = "09905841787",
                Email = "joazinhoteste@teste.com",
                Telefone = "44987452147",
                DataNascimento = Convert.ToDateTime("1998-04-15"),
                Senha = "123",
                ConfirmacaoSenha = "123"
            };

            Endereco endTeste = new Endereco();

            CepService testeServico = new CepService();
            endTeste = await testeServico.ObterCep("87116202");

            try
            {
                UsuarioDB dB = new UsuarioDB();
                dB.NovoUsuario(userTeste, endTeste);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return Content("");
        }
    }
}
