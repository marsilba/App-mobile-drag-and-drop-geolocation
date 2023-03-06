using System;

namespace AdministradorWeb.Models
{
    public class Grafico
    {
        public string ChartName { get; set; }
        public string Type { get; set; }
        public string[] Labels { get; set; }
        public int[] Series { get; set; }
    }

    //public class GraficoLinhaTempo
    //{
    //    public GraficoLinhaTempo()
    //    {
    //        Labels = new string[4];
    //        Series = new double[4];
    //        DatasHoras = new string[4];
    //    }

    //    public string TempoTotal { get; set; }
    //    public string[] Labels { get; set; }
    //    public double[] Series { get; set; }
    //    public string[] DatasHoras { get; set; }
    //}
}
