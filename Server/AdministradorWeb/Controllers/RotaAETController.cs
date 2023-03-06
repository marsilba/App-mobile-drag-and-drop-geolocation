using AdministradorWeb.Models;
using AdministradorWeb.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdministradorWeb.Controllers
{
    public class RotaAETController : Controller
    {
        private readonly MySqlContext _mySqlContext;
        public RotaAETController(MySqlContext ctx)
        {
            _mySqlContext = ctx;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Obra de Arte Especiais";
            ViewBag.TiposObras = Combos.CriarTiposObrasArtesEspeciais();

            return View(new HackatranObraArteEspecial());
        }

        public IActionResult Pesquisar(int? id)
        {
            List<HackatranObraArteEspecial> result = new List<HackatranObraArteEspecial>();

            if (id == null)
            {
                result = _mySqlContext.HackatranObrasArteEspeciais.ToList();
            }
            else
            {
                result = _mySqlContext.HackatranObrasArteEspeciais.Where(x => x.TipoId == id).ToList();
            }

            return View("_Grid", result);
        }

        public IActionResult Cadastrar()
        {
            ViewBag.TipoObras = Combos.CriarTiposObrasArtesEspeciais();

            return View("Formulario", new HackatranObraArteEspecial());
        }

        public IActionResult Editar(int? id)
        {
            ViewBag.TipoObras = Combos.CriarTiposObrasArtesEspeciais();

            AjaxResult<HackatranObraArteEspecial> ajaxResult = new AjaxResult<HackatranObraArteEspecial>();

            if (id == null)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = "Informe o a obra de arte especial.";

                return Json(ajaxResult);
            }

            var result = _mySqlContext.HackatranObrasArteEspeciais.SingleOrDefault(x => x.Id == id);
            if (id == null)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = "Obra de arte especial não localizado";

                return Json(ajaxResult);
            }

            return View("Formulario", result);
        }

        [HttpPost]
        public JsonResult Salvar(HackatranObraArteEspecial model)
        {
            AjaxResult<HackatranObraArteEspecial> ajaxResult = new AjaxResult<HackatranObraArteEspecial>();
            if (!TryValidateModel(model))
            {
                ajaxResult.ConfigurarModelStateErrors(ModelState);
                return Json(ajaxResult);
            }

           

            if ((model.TipoId == 2 && (model.Largura <= 0 || model.Altura <= 0)))
            {
                ajaxResult.Success = false;
                ajaxResult.Message = "Os dados de largura e altura devem ser informados.";
                return Json(ajaxResult);
            }

            if ((model.TipoId != 2 && (model.Largura <= 0 || model.Peso <= 0)))
            {
                ajaxResult.Success = false;
                ajaxResult.Message = "Os dados de largura e peso devem ser informados.";
                return Json(ajaxResult);
            }

            model.Tipo = Combos.CriarTiposObrasArtesEspeciais().FirstOrDefault(x => x.Value == Convert.ToString(model.TipoId)).Text;

            if (model.Id == null)
            {
                _mySqlContext.Set<HackatranObraArteEspecial>().Add(model);
            }
            else
            {
                HackatranObraArteEspecial obra = _mySqlContext.HackatranObrasArteEspeciais.FirstOrDefault(x => x.Id == model.Id);
                obra.Altura = model.Altura;
                obra.Endereco = model.Endereco;
                obra.Largura = model.Largura;
                obra.Latitude = model.Latitude;
                obra.Longitude = model.Longitude;
                obra.Peso = model.Peso;
                obra.Tipo = model.Tipo;
                obra.TipoId = model.TipoId;

                _mySqlContext.Entry(obra).State = EntityState.Modified;
            }

            _mySqlContext.SaveChanges();


            ajaxResult.Success = true;
            ajaxResult.Message = "Obra de Arte Especial cadastrada com sucesso.";
            ajaxResult.Model = model;

            return Json(ajaxResult);
        }

        public JsonResult Excluir(int id)
        {
            AjaxResult<HackatranObraArteEspecial> ajaxResult = new AjaxResult<HackatranObraArteEspecial>();

            var result = _mySqlContext.HackatranObrasArteEspeciais.SingleOrDefault(x => x.Id == id);

            if (result == null)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = "Laboratório não localizado";

                return Json(ajaxResult);
            }

            _mySqlContext.Set<HackatranObraArteEspecial>().Remove(result);
            _mySqlContext.SaveChanges();

            ajaxResult.Success = true;
            ajaxResult.Message = "Obra excluída com sucesso";
            ajaxResult.Model = result;

            return Json(ajaxResult);
        }
    }
}
