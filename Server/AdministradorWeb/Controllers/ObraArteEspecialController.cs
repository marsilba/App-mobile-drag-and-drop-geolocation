using AdministradorWeb.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AdministradorWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObraArteEspecialController : ControllerBase
    {
        private readonly MySqlContext _mySqlContext;

        public ObraArteEspecialController(MySqlContext ctx)
        {
            _mySqlContext = ctx;
        }

        // GET: api/<ObraArteEspecialController>
        [HttpGet]
        public IEnumerable<HackatranObraArteEspecial> Get()
        {
            return _mySqlContext.HackatranObrasArteEspeciais.ToList();
        }
    }
}