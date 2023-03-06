using AdministradorWeb.Models;
using AdministradorWeb.Repositorio;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdministradorWeb.Controllers
{
    public class LaboratorioController : Controller
    {
        private readonly MySqlContext _mySqlContext;
        private readonly IHostingEnvironment _hostingEnvironment;

        public LaboratorioController(MySqlContext _context, IHostingEnvironment hostingEnvironment)
        {
            _mySqlContext = _context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Credenciamento de Laboratórios";

            var model = new HackatranLaboratorio
            {
                Laboratorios = ObterLaboratorios()
            };

            return View(model);
        }

        public IActionResult Cadastrar()
        {
            HackatranLaboratorio model = new HackatranLaboratorio
            {
                Laboratorios = _mySqlContext.HackatranLaboratorios.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Nome.ToUpper()
                }).OrderBy(x => x.Text)
                .Distinct()
                .ToList()
            };

            return View("CredenciamentoLab", model);
        }

        public IActionResult CadastrarExame()
        {
            @ViewBag.Motoristas = _mySqlContext.HackatranExames.Where(x => x.Resultado == null).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Nome.ToUpper()
            })
            .OrderBy(x => x.Text)
            .ToList();


            @ViewBag.Laboratorios = _mySqlContext.HackatranLaboratorios.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Nome.ToUpper()
            })
            .OrderBy(x => x.Text)
            .ToList();

            return View("Motorista", new HackatranExame());
        }

        public IActionResult Editar(int? id)
        {
            AjaxResult<HackatranLaboratorio> ajaxResult = new AjaxResult<HackatranLaboratorio>();

            if (id == null)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = "Informe o laboratório";

                return Json(ajaxResult);
            }

            var laboratorio = _mySqlContext.HackatranLaboratorios.SingleOrDefault(x => x.Id == id);
            if (id == null)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = "Laboratório não localizado";

                return Json(ajaxResult);
            }

            laboratorio.Laboratorios = _mySqlContext.HackatranLaboratorios.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Nome.ToUpper()
            }).OrderBy(x => x.Text)
            .Distinct()
            .ToList();


            return View("CredenciamentoLab", laboratorio);
        }

        public IActionResult EditarExame(int? id)
        {
            AjaxResult<HackatranExame> ajaxResult = new AjaxResult<HackatranExame>();

            if (id == null)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = "Informe o motorista.";

                return Json(ajaxResult);
            }

            var result = _mySqlContext.HackatranExames.SingleOrDefault(x => x.Id == id);
            result.FlagResultado = result.Resultado == null ? false : (bool)result.Resultado;

            if (id == null)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = "Motorista não localizado.";

                return Json(ajaxResult);
            }

            @ViewBag.Motoristas = _mySqlContext.HackatranExames.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Nome.ToUpper()
            })
            .OrderBy(x => x.Text)
            .ToList();

            @ViewBag.Laboratorios = _mySqlContext.HackatranLaboratorios.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Nome.ToUpper()
            })
            .OrderBy(x => x.Text)
            .ToList();


            return View("Motorista", result);
        }

        public JsonResult Salvar(HackatranLaboratorio model)
        {
            //HackatranLaboratorio laboratorio = _mySqlContext.HackatranLaboratorios.SingleOrDefault(x => x.Id == model.Id);
            //laboratorio.Endereco = model.Endereco;
            //laboratorio.Horario = model.Horario;
            //laboratorio.Latitude = model.Latitude;
            //laboratorio.Longitude = model.Longitude;

            if (model.Id == null)
            {
                _mySqlContext.Set<HackatranLaboratorio>().Add(model);
            }
            else
            {
                _mySqlContext.Entry(model).State = EntityState.Modified;
            }

            _mySqlContext.SaveChanges();

            AjaxResult<HackatranLaboratorio> ajaxResult = new AjaxResult<HackatranLaboratorio>
            {
                Success = true,
                Message = "Laboratório credenciado com sucesso.",
                Model = model
            };

            return Json(ajaxResult);
        }

        public JsonResult SalvarExame(HackatranExame model)
        {
            AjaxResult<HackatranExame> ajaxResult = new AjaxResult<HackatranExame>
            {
                Success = true,
                Message = "Existem dados inválidos ou não preenchidos.<br><br>"
            };

            if (string.IsNullOrEmpty(model.Nome))
            {
                ajaxResult.Success = false;
                ajaxResult.Message += "<br>Informe o laboratório responsável.";
            }

            if (string.IsNullOrEmpty(model.Cnh))
            {
                ajaxResult.Success = true;
                ajaxResult.Message += "<br>Informe a CNH.";
            }

            if (string.IsNullOrEmpty(model.Cpf))
            {
                ajaxResult.Success = true;
                ajaxResult.Message += "<br>Informe o CPF.";
            }

            if (model.LaboratorioId == null)
            {
                ajaxResult.Success = true;
                ajaxResult.Message += "<br>Informe o laboratório responsável.";
            }

            if (!ajaxResult.Success)
            {
                return Json(ajaxResult);
            }

            string nomeArquivo = "";
            if (model.Foto != null)
            {
                nomeArquivo = string.Concat("foto_exame_", DateTime.Now.ToString("HHmmssffff"), Path.GetExtension(model.Foto.FileName));
                model.UrlFoto = string.Format("http://www.busonline.com.br/images/hackatran/{0}", nomeArquivo);
            }

            var motorista = _mySqlContext.HackatranExames.FirstOrDefault(x => x.Id == model.Id);
            motorista.Cnh = model.Cnh;
            motorista.Cpf = model.Cpf;
            motorista.Data = model.Data;
            motorista.Foto = model.Foto;
            motorista.Nome = model.Nome;
            motorista.Resultado = model.FlagResultado;
            motorista.UrlFoto = model.UrlFoto;
            motorista.LaboratorioId = model.LaboratorioId;
            motorista.Laboratorio = model.Laboratorio;

            _mySqlContext.Entry(motorista).State = EntityState.Modified;
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
            ajaxResult.Message = "Exame cadastrado com sucesso.";
            ajaxResult.Model = model;

            return Json(ajaxResult);
        }

        public JsonResult Excluir(int id)
        {
            AjaxResult<HackatranLaboratorio> ajaxResult = new AjaxResult<HackatranLaboratorio>();

            var laboratorio = _mySqlContext.HackatranLaboratorios.SingleOrDefault(x => x.Id == id);

            if (laboratorio == null)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = "Laboratório não localizado";

                return Json(ajaxResult);
            }

            _mySqlContext.Set<HackatranLaboratorio>().Remove(laboratorio);
            _mySqlContext.SaveChanges();

            ajaxResult.Success = true;
            ajaxResult.Message = "Laboratório excluído com sucesso";
            ajaxResult.Model = laboratorio;

            return Json(ajaxResult);
        }

        public JsonResult ExcluirExame(int id)
        {
            AjaxResult<HackatranExame> ajaxResult = new AjaxResult<HackatranExame>();

            var result = _mySqlContext.HackatranExames.SingleOrDefault(x => x.Id == id);

            if (result == null)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = "Laboratório não localizado";

                return Json(ajaxResult);
            }

            _mySqlContext.Set<HackatranExame>().Remove(result);
            _mySqlContext.SaveChanges();

            ajaxResult.Success = true;
            ajaxResult.Message = "Laboratório excluído com sucesso";
            ajaxResult.Model = result;

            return Json(ajaxResult);
        }

        public IActionResult ResultadoExame()
        {
            ViewBag.Title = "Resultado de Exames";

            ViewBag.Motoristas = _mySqlContext.HackatranExames.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Nome.ToUpper()
            }).ToList();

            return View();
        }

        public IActionResult ObterResultado(int? id)
        {
            List<HackatranExame> result = new List<HackatranExame>();

            if (id == null)
            {
                result = _mySqlContext.HackatranExames.ToList();
            }
            else
            {
                result = _mySqlContext.HackatranExames.Where(x => x.Id == id).ToList();
            }

            return View("_GridExame", result);
        }

        public IActionResult PesquisarLaboratorios(int? id)
        {
            List<HackatranLaboratorio> laboratorios = new List<HackatranLaboratorio>();

            if (id == null)
            {
                laboratorios = _mySqlContext.HackatranLaboratorios.ToList();
            }
            else
            {
                laboratorios = _mySqlContext.HackatranLaboratorios.Where(x => x.Id == id).ToList();
            }

            return View("_Grid", laboratorios);
        }

        private List<SelectListItem> ObterLaboratorios()
        {
            return _mySqlContext.HackatranLaboratorios.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Nome.ToUpper()
            }).ToList();
        }
    }
}