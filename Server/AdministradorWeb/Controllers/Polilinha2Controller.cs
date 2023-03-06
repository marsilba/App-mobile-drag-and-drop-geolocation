using AdministradorWeb.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AdministradorWeb.Controllers
{
    [Route("api/polilinha")]
    public class Polilinha2Controller : Controller
    {
        private readonly MySqlContext _mySqlContext;

        public Polilinha2Controller(MySqlContext ctx)
        {
            _mySqlContext = ctx;
        }

        [HttpGet]
        public JsonResult Get(string placa)
        {
            if (!string.IsNullOrEmpty(placa))
            {
                var polilinha = _mySqlContext.HackatranPolilinhas.LastOrDefault(x => x.Placa.ToUpper() == placa.ToUpper());
                return Json(polilinha);
            }

            return Json("");
        }

        [HttpPost]
        public JsonResult Index(HackatranPolilinha model)
        {
            //HackatranPolilinha model = new HackatranPolilinha
            //{
            //    Rota = rota
            //};

            _mySqlContext.Set<HackatranPolilinha>().Add(model);
            _mySqlContext.SaveChanges();

            return Json(model.Rota);
        }
    }
}
