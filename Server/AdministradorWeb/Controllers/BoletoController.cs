using AdministradorWeb.Models;
using AdministradorWeb.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AdministradorWeb.Controllers
{
    public class BoletoController : Controller
    {
        private readonly MySqlContext _mySqlContext;

        public BoletoController(MySqlContext context)
        {
            _mySqlContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string GerarBoleto()
        {
            HackatranBoleto pagador = _mySqlContext.HackatranBoletos.FirstOrDefault();
            return new BoletoDenatran().BoletoTeste(pagador);
        }
    }
}
