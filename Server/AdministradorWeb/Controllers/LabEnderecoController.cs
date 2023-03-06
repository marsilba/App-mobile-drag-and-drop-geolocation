using AdministradorWeb.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AdministradorWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabEnderecoController : ControllerBase
    {
        private readonly MySqlContext _mySqlContext;

        public LabEnderecoController(MySqlContext ctx)
        {
            _mySqlContext = ctx;
        }

        // GET: api/<LabEndereco>
        [HttpGet]
        public IEnumerable<HackatranLaboratorio> Get()
        {
            return _mySqlContext.HackatranLaboratorios
                                .Where(x => !string.IsNullOrEmpty(x.Endereco))
                                .ToList();
        }

        // GET api/<LabEndereco>/5
        [HttpGet("{id}")]
        public HackatranLaboratorio Get(int id)
        {
            return _mySqlContext.HackatranLaboratorios.FirstOrDefault();
        }
    }
}
