using Microsoft.AspNetCore.Mvc;

namespace LojaBase.Controllers
{
    public class AdminHomeController : Controller
    {
        [Route("/admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/admin/usuarios")]
        public IActionResult Usuario()
        {
            return View();
        }

        [Route("/admin/produtos")]
        public IActionResult Produto()
        {
            return View();
        }

        [Route("/admin/logs")]
        public IActionResult LogSistema()
        {
            return View();
        }

        [Route("/admin/bugs")]
        public IActionResult BugReport()
        {
            return View();
        }
    }
}
