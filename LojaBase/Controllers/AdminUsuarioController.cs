using LojaBase.DAL;
using LojaBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaBase.Controllers
{
    public class AdminUsuarioController : Controller
    {
        private readonly UsuarioDB _usuarioDB;
        
        public AdminUsuarioController() 
        {
            _usuarioDB = new UsuarioDB();
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        public IActionResult ListaUsuarios()
        {
            try
            {
                List<Usuario> listaUsuariosAdm = new List<Usuario>();   
                listaUsuariosAdm = _usuarioDB.ListaUsuarios(true); 

                ViewBag.ListaUsuarios = listaUsuariosAdm;
                return View();
            }
            catch (Exception ex) 
            {
                return RedirectToAction("Erro", "Erro", new { id = ex.Message });
            } 
        }

        public IActionResult Permissoes()
        {
            return View();
        }
    }
}
