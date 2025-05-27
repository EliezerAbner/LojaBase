using LojaBase.DAL;
using LojaBase.Models;
using LojaBase.Services;
using Microsoft.AspNetCore.Mvc;

namespace LojaBase.Controllers
{
    public class AdminUsuarioController : Controller
    {
        private readonly UsuarioDB _usuarioDB;
        private readonly CepService _cepService;
        
        public AdminUsuarioController() 
        {
            _usuarioDB = new UsuarioDB();
            _cepService = new CepService();
        }

        [Route("admin/usuarios/buscaCep")]
        public async Task<IActionResult> BuscaCep()
        {
            string? cep = Convert.ToString(Request.Query["cepTxt"]);

            if (cep != "") 
            {
                Endereco end = await _cepService.ObterCep(cep);
                return Json(end); 
            }

            return BadRequest("O Cep não pode ser nulo");
        }


        [Route("admin/usuarios/cadastrar")]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [Route("admin/usuarios/novoCliente")]
        public IActionResult NovoUsuario(Usuario usu, Endereco end, Email email)
        {
            return Content("");
        }

        [Route("/admin/usuarios/lista")]
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
