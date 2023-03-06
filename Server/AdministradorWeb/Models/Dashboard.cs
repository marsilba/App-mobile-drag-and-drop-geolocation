using AdministradorWeb.Models.Denatran;
using AdministradorWeb.Repositorio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AdministradorWeb.Models
{
    public class Dashboard
    {
        #region Declaracao

        private readonly MySqlContext _mySqlContext;
        
        private DateTime Hoje { get; set; }

        private DateTime PrimeiroDiaDaSemanaCorrente { get; set; }

        private DateTime UltimoDiaDaSemanaCorrente { get; set; }

        private DateTime PrimeiroDiaDoMesCorrente { get; set; }

        private DateTime UltimoDiaDoMesCorrente { get; set; }

        public Dashboard(MySqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
            Graficos = new List<Grafico>();
            Mapas = new List<MapaDash>();
        }

        public int TotalOcorrencias { get; set; }
        public string LegendaTotalOcorrencias { get; set; }

        public decimal ValorBruto { get; set; }
        public string LegendaValorBruto { get; set; }

        public decimal ValorFunset { get; set; }
        public string LegendaValorFunset { get; set; }

        public decimal ValorDetran { get; set; }
        public string LegendaValorDetran { get; set; }

        public decimal ValorDenatran { get; set; }
        public decimal ValorOrgaoAutuador { get; set; }

        public List<Grafico> Graficos { get; set; }
        public List<MapaDash> Mapas { get; set; }

        #endregion

        #region Metodos Publicos

        public void CriarGraficoDebitoUf(string chartName)
        {
            List<HackatranVarrecadacaoUf> ufs = _mySqlContext.HackatranVarrecadacoesUfs.ToList();

            Grafico grafico = new Grafico
            {
                ChartName = chartName,
                Type = "bar",
                Labels = new string[ufs.Count],
                Series = new int[ufs.Count]
            };

            int idx = 0;
            foreach (var uf in ufs)
            {
                grafico.Labels[idx] = uf.UF;
                grafico.Series[idx] = (int) uf.ValorBruto;

                idx++;
            }

            Graficos.Add(grafico);
        }

        public void CriarGraficoMensal(string chartName)
        {
            List<HackatranVarrecadacaoMes> meses = _mySqlContext.HackatranVarrecadacoesMeses.ToList();

            Grafico grafico = new Grafico
            {
                ChartName = chartName,
                Type = "line",
                Labels = new string[meses.Count],
                Series = new int[meses.Count]
            };

            int idx = 0;
            foreach (var mes in meses)
            {
                grafico.Labels[idx] = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(mes.Mes).ToUpper();
                grafico.Series[idx] = (int)mes.ValorBruto;

                idx++;
            }

            Graficos.Add(grafico);
        }

        public void CriarQuantidadeOcorrencias()
        {
            HackatranVarrecadacaoGeral arrecadacao = _mySqlContext.ViewArrecadacoesGerais.FirstOrDefault();
            
            TotalOcorrencias = arrecadacao.Quantidade;
            ValorBruto = arrecadacao.ValorBruto;
            ValorFunset = arrecadacao.ValorFunset;
            ValorDetran = arrecadacao.ValorDetran;
            ValorDenatran = arrecadacao.ValorDenatran;
            ValorOrgaoAutuador = arrecadacao.ValorOrgaoAutuador;
        }

        public void CriarMapa()
        {
            MapaDash mapa;
            HackatranVarrecadacaoUf arrecadacaoTemp;
            List<HackatranUf> ufs = _mySqlContext.HackatranUfs.OrderBy(x => x.Sigla).ToList();
            List<HackatranVarrecadacaoUf> arrecadacoes = _mySqlContext.HackatranVarrecadacoesUfs.ToList();

            foreach (var uf in ufs)
            {
                arrecadacaoTemp = arrecadacoes.Where(x => x.UF == uf.Sigla).FirstOrDefault();

                mapa = new MapaDash
                {
                    Uf = uf.Nome,
                    Latitude = uf.Latitude,
                    Longitude = uf.Longitude,
                    Quantidade = arrecadacaoTemp.Quantidade,
                    ValorBruto = arrecadacaoTemp.ValorBruto,
                    ValorFunset = arrecadacaoTemp.ValorFunset,
                    ValorDenatran = arrecadacaoTemp.ValorDenatran,
                    ValorDetran = arrecadacaoTemp.ValorDetran,
                    ValorOrgaoAutuador = arrecadacaoTemp.ValorOrgaoAutuador
                };

                Mapas.Add(mapa);
            }
        }

        #endregion

        #region Metodos Privados

        private string ObterIcone(string tipo)
        {
            switch (tipo.ToUpper())
            {
                case "ATENDIMENTO":
                    return "iconeAtendimento.fw.png";
                case "CMA/GCM":
                    return "iconeCMA.fw.png";
                case "CRIMINAL":
                    return "iconeCriminal.fw.png";
                case "ORDEM PÚBLICA":
                    return "iconeOrdemPublica.fw.png";
                case "OUTROS":
                    return "iconeOutros.fw.png";
                case "TRÂNSITO":
                    return "iconeTransito.fw.png";
            }
            return "";
        }

        private string CriarLabelGraficoSemanal(int dia)
        {
            switch (dia)
            {
                case 1:
                    return "D";

                case 2:
                case 6:
                case 7:
                    return "S";

                case 3:
                    return "T";

                case 4:
                case 5:
                    return "Q";

                default:
                    return string.Empty;
            }
        }

        private string[] CriarLabelGraficoHorario()
        {
            return new string[] { "0h", "4h", "8h", "12h", "16h", "20h", "0h" };
        }

        #endregion
    }
}