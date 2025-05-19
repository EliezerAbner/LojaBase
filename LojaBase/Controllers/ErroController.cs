using Microsoft.AspNetCore.Mvc;

namespace LojaBase.Controllers
{
    public class ErroController : Controller
    {
        [Route("/erro/{id}")]
        public IActionResult Erro()
        {
            string? erro = Convert.ToString(Request.RouteValues["id"]);
            ViewData["erro"] = erro;

            return View();
        }
    }
}
