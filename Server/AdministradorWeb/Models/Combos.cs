using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AdministradorWeb.Models
{
    public static class Combos
    {
        public static List<SelectListItem> CriarLaboratorios()
        {
            return new List<SelectListItem>
            {
                new SelectListItem() { Value = "1", Text = "Contraprova Análises, Ensino e Pesquisas LTDA" },
                new SelectListItem() { Value = "2", Text = "Labet – Exames Toxicológicos (Citilab Diagnósticos LTDA)" },
                new SelectListItem() { Value = "3", Text = "Laboratório Chromatox LTDA" },
                new SelectListItem() { Value = "4", Text = "Laboratório Sodre (Laboratório Morales LTDA)" },
                new SelectListItem() { Value = "5", Text = "Maxilabor Diagnósticos LTDA" },
                new SelectListItem() { Value = "6", Text = "Psychmedics Brasil Exames Toxicológicos LTDA" }
            };
        }

        public static List<SelectListItem> CriarTiposObrasArtesEspeciais()
        {
            return new List<SelectListItem>
            {
                new SelectListItem() { Value = "1", Text = "Ponte" },
                new SelectListItem() { Value = "2", Text = "Túnel" },
                new SelectListItem() { Value = "3", Text = "Viaduto" },
            };
        }
    }
}