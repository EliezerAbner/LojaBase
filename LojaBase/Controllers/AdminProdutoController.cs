using Microsoft.AspNetCore.Mvc;

namespace LojaBase.Controllers
{
    public class AdminProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
