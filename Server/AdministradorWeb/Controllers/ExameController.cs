using AdministradorWeb.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AdministradorWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExameController : ControllerBase
    {
        private readonly MySqlContext _mySqlContext;

        public ExameController(MySqlContext ctx)
        {
            _mySqlContext = ctx;
        }

        [HttpGet]
        public IEnumerable<HackatranExame> Get()
        {
            var result = _mySqlContext.HackatranExames.ToList();
            return result;
        }

        // GET api/<ValuesController>/560.481.970-04
        [Route("api/[controller]/cpf")]
        [HttpGet("{cpf}")]
        public HackatranExame Get(string cpf)
        {
            var result = _mySqlContext.HackatranExames.Where(x => x.Cpf == cpf).SingleOrDefault();
            return result;
        }
    }
}