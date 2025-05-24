using LojaBase.DAL;
using LojaBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaBase.Controllers
{
    public class CompraController : Controller
    {
        private readonly CarrinhoDB _carrinhoDB;
        
        public CompraController() 
        {
            _carrinhoDB = new CarrinhoDB();
        }

        [Route("/compra/meu-carrinho/{userid}")]
        public IActionResult MeuCarrinho()
        {
            int usuarioId = Convert.ToInt32(Request.RouteValues["userId"]);
            
            if (usuarioId == 0)
            {
                return RedirectToAction("Erro", "Erro", new { id = "Ops! Parece que essa página não existe." });
            }

            try
            {
                List<Carrinho> listaItens = new List<Carrinho>();
                listaItens = _carrinhoDB.ItensCarrinho(usuarioId);

                ViewBag.listaItens = listaItens;
                ViewBag.Css = "/css/MeuCarrinho.css";

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Erro", "Erro", new { id = ex.Message });
            }
        }
        
        [Route("/compra/add-to-cart/{prodId}/{userId}")]
        public IActionResult AdicionarAoCarrinho()
        {
            int produtoId = Convert.ToInt32(Request.RouteValues["prodId"]);
            int usuarioId = Convert.ToInt32(Request.RouteValues["userId"]);

            if (produtoId == 0 || usuarioId == 0) 
            {
                return RedirectToAction("Erro", "Erro", new { id = "Ops! Parece que essa página não existe." });
            }

            try
            {
                Carrinho novoItem = new Carrinho()
                {
                    DataAdicao = DateTime.Now,
                    ProdutoId = produtoId,
                    UsuarioId = usuarioId
                };

                _carrinhoDB.AdicionarAoCarrinho(novoItem);

                return RedirectToAction("Produto", "Home", new { id = produtoId });
            }
            catch (Exception ex) 
            {
                return RedirectToAction("Erro", "Erro", new { id = ex.Message });
            }
        }
    }
}
