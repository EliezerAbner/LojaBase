using LojaBase.Models;
using LojaBase.Services;
using Microsoft.AspNetCore.Mvc;

namespace LojaBase.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public async Task <IActionResult> Index()
        {
            Endereco teste = new Endereco();

            return Content($"<h1>Welcome</h1> <p>{teste.Cep}</p> <br> <p>{teste.Logradouro}</p> <br><p>Ola</p>", "text/html");
        }
    }
}
