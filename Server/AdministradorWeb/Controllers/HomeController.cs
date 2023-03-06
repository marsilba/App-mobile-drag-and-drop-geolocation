using AdministradorWeb.Models;
using AdministradorWeb.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AdministradorWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly MySqlContext _mySqlContext;

        public HomeController(MySqlContext _context)
        {
            _mySqlContext = _context;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Dashboard";
            return View();
        }

        [HttpPost]
        public JsonResult DashBoard()
        {
            Dashboard dash = new Dashboard(_mySqlContext);

            dash.CriarGraficoDebitoUf("chartMensal");
            dash.CriarGraficoMensal("chartSemanal");
            dash.CriarQuantidadeOcorrencias();
            dash.CriarMapa();

            return Json(dash);
        }

        public IActionResult LoginFinanceiro()
        {
            Configurador.NomeSistema = "Gestor OnLine";
            return View();
        }

        public IActionResult LoginExame()
        {
            Configurador.NomeSistema = "LabOn";
            return View();
        }

        public IActionResult LoginAet()
        {
            Configurador.NomeSistema = "AET Digital";
            return View();
        }

        [HttpPost]
        public IActionResult LoginFinanceiro(LoginViewModel model)
        {
            AjaxResult<LoginViewModel> ajaxResult = new AjaxResult<LoginViewModel>()
            {
                Success = false,
                Message = "Dados inválidos"
            };

            if (ModelState.IsValid)
            {
                HackatranUsuario usuario = ValidarLogin(model);
                if (usuario != null)
                {
                    Configurar(usuario);

                    ajaxResult.Success = true;
                    ajaxResult.Url = "Home/Index";

                    UsuarioLogado.TipoLogin = 1;
                }
            }

            return Json(ajaxResult);
        }

        [HttpPost]
        public IActionResult LoginExame(LoginViewModel model)
        {
            AjaxResult<LoginViewModel> ajaxResult = new AjaxResult<LoginViewModel>()
            {
                Success = false,
                Message = "Dados inválidos"
            };

            if (ModelState.IsValid)
            {
                HackatranUsuario usuario = ValidarLogin(model);
                if (usuario != null)
                {
                    Configurar(usuario);

                    ajaxResult.Success = true;
                    ajaxResult.Url = "Laboratorio/Index";

                    UsuarioLogado.TipoLogin = 2;
                }
            }

            return Json(ajaxResult);
        }

        [HttpPost]
        public IActionResult LoginAet(LoginViewModel model)
        {
            AjaxResult<LoginViewModel> ajaxResult = new AjaxResult<LoginViewModel>()
            {
                Success = false,
                Message = "Dados inválidos"
            };

            if (ModelState.IsValid)
            {
                HackatranUsuario usuario = ValidarLogin(model);
                if (usuario != null)
                {
                    Configurar(usuario);

                    ajaxResult.Success = true;
                    ajaxResult.Url = "RotaAET/Index";

                    UsuarioLogado.TipoLogin = 3;
                }
            }

            return Json(ajaxResult);
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }

        private HackatranUsuario ValidarLogin(LoginViewModel login)
        {
            return _mySqlContext.HackatranUsuarios.Where(x => x.Login.ToLower() == login.UserName.ToLower() && x.Senha == login.Password).SingleOrDefault();
        }

        private void Configurar(HackatranUsuario usuario)
        {
            UsuarioLogado.Id = usuario.Id;
            UsuarioLogado.Login = usuario.Login;
            UsuarioLogado.Matricula = usuario.Matricula;
            UsuarioLogado.Nome = usuario.Nome;
            UsuarioLogado.Perfil = usuario.Perfil;
        }
    }
}