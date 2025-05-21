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

                ViewData["categorias"] = listaCategorias;
                ViewData["produtos"] = listaProdutos;
                ViewData["titulo"] = "LojaBase";
                return View();
            }
            catch (Exception ex) 
            {
                return RedirectToAction("Erro", "Erro", new { id = ex.Message });  
            }
        }

        [Route("/pesquisa")]
        [Route("/pesquisa/{tipoPesquisa}/{id}")]
        public IActionResult Pesquisa(string search) 
        {
            string? tipoPesquisa = Convert.ToString(Request.RouteValues["tipoPesquisa"]);
            int id = Convert.ToInt32(Request.RouteValues["id"]);

            List<Produto> produtos = new List<Produto>();

            try
            {
                if (tipoPesquisa == "")
                {
                    produtos = _produtoDB.BuscaProdutos(0, search);
                    ViewData["titulo"] = $"LojaBase - {search}";
                }
                else if (tipoPesquisa == "categoria")
                {
                    produtos = _produtoDB.BuscaProdutos(id);
                    ViewData["titulo"] = "LojaBase";
                }
                else if (tipoPesquisa != "categoria")
                {
                    return RedirectToAction("Erro", "Erro", new { id = "Ops! Parece que essa página não existe." });
                }

                ViewData["produtos"] = produtos;

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Erro", "Erro", new { id = ex.Message });
            }
        }

        [Route("/produto/{id}")]
        public IActionResult Produto()
        {
            try
            {
                int id = Convert.ToInt32(Request.RouteValues["id"]);
                Produto? prod = _produtoDB.BuscaProduto(id);

                if (prod.Id == 0) 
                {
                    return RedirectToAction("Erro", "Erro", new { id = "Ops! Parece que essa página não existe." });
                }

                List<Imagem> images = _produtoDB.ImagensProduto(id);
                List<Comentario> comentarios = _produtoDB.ListaComentarios(id);
                List<Produto> prodParecidos = _produtoDB.BuscaProdutos(prod.CategoriaId);

                ViewBag.Produto = prod;
                ViewBag.Images = images;
                ViewBag.Comentarios = comentarios;
                ViewBag.ProdParecidos = prodParecidos;
                ViewBag.Css = "/css/Produto.css";
                ViewData["titulo"] = $"LojaBase - {prod.Nome}";

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Erro", "Erro", new { id = ex.Message });
            }
        }
    }
}
