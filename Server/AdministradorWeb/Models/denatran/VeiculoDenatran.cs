namespace AdministradorWeb.Models
{
    public class VeiculoDenatran
    {
        public int? Id { get; set; }
        public string Motorista { get; set; }
        public string Cnh { get; set; }
        public string Placa { get; set; }
        public decimal? Comprimento { get; set; }
        public decimal? Largura { get; set; }
        public decimal? Altura { get; set; }
        public decimal? Peso { get; set; }
        public string UrlFoto { get; set; }
        public string Rota { get; set; }
        public string Html { get; set; }
        public string CodAet { get; set; }
    }
}
