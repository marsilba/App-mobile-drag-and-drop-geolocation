using AdministradorWeb.Models;
using AdministradorWeb.Repositorio;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdministradorWeb.Controllers
{
    public class VeiculoController : Controller
    {
        private readonly MySqlContext _mySqlContext;
        private readonly IHostingEnvironment _hostingEnvironment;

        public VeiculoController(MySqlContext ctx, IHostingEnvironment hostingEnvironment)
        {
            _mySqlContext = ctx;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Cadastro de Veículos";
            ViewBag.Placas = _mySqlContext.HackatranVeiculos.Select(x => new SelectListItem
            {
                Value = x.Placa,
                Text = x.Placa
            }).OrderBy(x => x.Value)
            .ToList();

            return View(new HackatranVeiculo());
        }

        public IActionResult Pesquisar(string placa)
        {
            List<HackatranVeiculo> result = new List<HackatranVeiculo>();

            if (string.IsNullOrEmpty(placa))
            {
                result = _mySqlContext.HackatranVeiculos.ToList();
            }
            else
            {
                result = _mySqlContext.HackatranVeiculos.Where(x => x.Placa.ToUpper() == placa.ToUpper()).ToList();
            }

            return View("_Grid", result);
        }

        public IActionResult Cadastrar()
        {
            ViewBag.Placas = _mySqlContext.HackatranVeiculos.Select(x => new SelectListItem
            {
                Value = x.Placa,
                Text = x.Placa
            }).OrderBy(x => x.Value)
            .ToList();

            return View("Formulario", new HackatranVeiculo());
        }

        public JsonResult Salvar(HackatranVeiculo model)
        {
            AjaxResult<HackatranVeiculo> ajaxResult = new AjaxResult<HackatranVeiculo>
            {
                Success = true,
                Message = "Existem dados inválidos ou não preenchidos.<br><br>"
            };

            if (string.IsNullOrEmpty(model.Motorista))
            {
                ajaxResult.Success = false;
                ajaxResult.Message += "<br>Informe o motorista.";
            }

            if (string.IsNullOrEmpty(model.Cnh))
            {
                ajaxResult.Success = true;
                ajaxResult.Message += "<br>Informe a CNH.";
            }

            if (string.IsNullOrEmpty(model.Placa))
            {
                ajaxResult.Success = true;
                ajaxResult.Message += "<br>Informe a placa.";
            }

            if (model.Peso == null)
            {
                ajaxResult.Success = true;
                ajaxResult.Message += "<br>Informe o peso (t) do veículo.";
            }

            if (model.Comprimento == null)
            {
                ajaxResult.Success = true;
                ajaxResult.Message += "<brInforme o comprimenro (m) do veículo.";
            }

            if (model.Altura == null)
            {
                ajaxResult.Success = true;
                ajaxResult.Message += "<brInforme a altura (m) do veículo.";
            }


            if (!ajaxResult.Success)
            {
                return Json(ajaxResult);
            }

            string nomeArquivo = "";
            if (model.Foto != null)
            {
                nomeArquivo = string.Concat("foto_veiculo_", DateTime.Now.ToString("HHmmssffff"), Path.GetExtension(model.Foto.FileName));
                model.UrlFoto = string.Format("http://www.busonline.com.br/images/hackatran/{0}", nomeArquivo);
            }

            if (model.Id == null)
            {
                _mySqlContext.Set<HackatranVeiculo>().Add(model);
            }
            else
            {
                _mySqlContext.Entry(model).State = EntityState.Modified;
            }

            _mySqlContext.SaveChanges();

            if (model.Foto != null)
            {
                string file = Path.Combine(_hostingEnvironment.WebRootPath, "images", "hackatran", nomeArquivo);
                using (var stream = new FileStream(file, FileMode.Create))
                {
                    model.Foto.CopyTo(stream);
                }
            }

            ajaxResult.Success = true;
            ajaxResult.Message = "Veículo cadastrado com sucesso.";
            ajaxResult.Model = model;

            return Json(ajaxResult);
        }

        public IActionResult Editar(int? id)
        {
            AjaxResult<HackatranVeiculo> ajaxResult = new AjaxResult<HackatranVeiculo>();

            if (id == null)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = "Informe o motorista.";

                return Json(ajaxResult);
            }

            var result = _mySqlContext.HackatranVeiculos.SingleOrDefault(x => x.Id == id);


            return View("Formulario", result);
        }

        public JsonResult Excluir(int id)
        {
            AjaxResult<HackatranVeiculo> ajaxResult = new AjaxResult<HackatranVeiculo>();

            var result = _mySqlContext.HackatranVeiculos.SingleOrDefault(x => x.Id == id);

            if (result == null)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = "Laboratório não localizado";

                return Json(ajaxResult);
            }

            _mySqlContext.Set<HackatranVeiculo>().Remove(result);
            _mySqlContext.SaveChanges();

            ajaxResult.Success = true;
            ajaxResult.Message = "Veículo excluído com sucesso";
            ajaxResult.Model = result;

            return Json(ajaxResult);
        }
    }
}