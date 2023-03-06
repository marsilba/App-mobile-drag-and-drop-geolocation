using AdministradorWeb.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdministradorWeb.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly MySqlContext _mySqlContext;

        public RelatorioController(MySqlContext _context)
        {
            _mySqlContext = _context;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Relatório de Autuações";

            ViewBag.Ufs = CriarUfs();

            return View();
        }

        public IActionResult Pesquisar(string id)
        {
            List<HackatranArrecadacao> result;

            if (string.IsNullOrEmpty(id))
            {
                
                result = _mySqlContext.HackatranArrecadacoes.ToList();
            }
            else
            {
                string uf = _mySqlContext.HackatranUfs.FirstOrDefault(x => x.Id == Convert.ToInt64(id)).Sigla;
                result = _mySqlContext.HackatranArrecadacoes.Where(x => x.Uf == uf).OrderBy(x => x.Uf).ToList();
            }

            return PartialView("_Grid", result);
        }

        public List<SelectListItem> CriarUfs()
        {
            var result = _mySqlContext.HackatranUfs.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Nome
            })
            .OrderBy(x => x.Text)
            .ToList();

            return result;
        }
    }
}