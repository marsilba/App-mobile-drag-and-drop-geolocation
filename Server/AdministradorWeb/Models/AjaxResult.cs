using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace AdministradorWeb.Models
{
    public class AjaxResult<T> where T : class
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Model { get; set; }
        public int Valor { get; set; }
        public string Json { get; set; }
        public string Url { get; set; }

        public AjaxResult(T entidade)
        {
            Model = entidade;
        }

        public AjaxResult()
        { }

        public string ConfigurarModelStateErrors(ModelStateDictionary modelState)
        {
            List<string> erros = modelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).Distinct().ToList();

            Message = "Existem dados inválidos ou não preenchidos.<br><br>";
            foreach (var e in erros)
            {
                Message += "<br>" + e + ".";
            }

            return Message;
        }
    }
}