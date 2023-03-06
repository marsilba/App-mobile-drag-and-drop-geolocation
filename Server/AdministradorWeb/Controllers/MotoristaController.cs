using AdministradorWeb.Models;
using AdministradorWeb.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AdministradorWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoristaController : ControllerBase
    {
        private readonly MySqlContext _mySqlContext;

        public MotoristaController(MySqlContext ctx)
        {
            _mySqlContext = ctx;
        }

        [HttpGet]
        public VeiculoDenatran Get(string placa)
        {
            if (string.IsNullOrEmpty(placa))
            {
                return null;
            }

            VeiculoDenatran result = new VeiculoDenatran();

            HackatranVeiculo veiculo = _mySqlContext.HackatranVeiculos.FirstOrDefault(x => x.Placa.ToUpper() == placa.ToUpper());

            if (veiculo == null)
            {
                return null;   
            }

            result.Id = veiculo.Id;
            result.Altura = veiculo.Altura;
            result.Cnh = veiculo.Cnh;
            result.Comprimento = veiculo.Comprimento;
            result.Largura = veiculo.Largura;
            result.Motorista = veiculo.Motorista;
            result.Peso = veiculo.Peso;
            result.Placa = veiculo.Placa;
            result.UrlFoto = veiculo.UrlFoto;
            result.CodAet = DateTime.Now.ToString("yyyyddmmHHmmssfff");

            HackatranPolilinha polilinha = _mySqlContext.HackatranPolilinhas.LastOrDefault(x => x.Placa.ToUpper() == placa.ToUpper());

            if (polilinha != null)
            {
                result.Html = polilinha.Html;
                result.Rota = polilinha.Rota;
            }


            return result;
        }
    }
}
