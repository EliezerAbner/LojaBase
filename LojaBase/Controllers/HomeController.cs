using LojaBase.DAL;
using LojaBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaBase.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ProdutoDB _produtoDB;

        public HomeController() 
        {
            _produtoDB = new ProdutoDB();
        }

        [Route("/")]
        public IActionResult Index()
        {
           List<Categoria> listaCategorias = new List<Categoria>();
           List<Produto> listaProdutos = new List<Produto>();

            try
            {
                listaCategorias = _produtoDB.listaCategorias();
                listaProdutos = _produtoDB.ListaProdutos();
            }
            catch (Exception ex) 
            {
                return RedirectToAction("Erro", "Erro", new { id = ex.Message });  
            }

            ViewData["categorias"] = listaCategorias;
            ViewData["produtos"] = listaProdutos;
            return View();
        }

        [Route("/pesquisa")]
        [Route("/pesquisa/{tipoPesquisa}/{id}")]
        public IActionResult Pesquisa(string search) 
        {
            string? tipoPesquisa = Convert.ToString(Request.RouteValues["tipoPesquisa"]);
            int id = Convert.ToInt32(Request.RouteValues["id"]);

            if (tipoPesquisa == "") 
            {
                //pesquisa geral
            }
            else if (tipoPesquisa == "categoria") 
            {
                //pesquisa produtos em determinada categoria
            }
            else if (tipoPesquisa != "categoria") 
            {
                //refirecionar para erro 404
            }

                return View();
        }
    }
}
