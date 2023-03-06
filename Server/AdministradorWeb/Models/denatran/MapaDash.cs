namespace AdministradorWeb.Models.Denatran
{
    public class MapaDash
    {
        public string Uf { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorBruto { get; set; }
        public decimal ValorFunset { get; set; }
        public decimal ValorDetran { get; set; }
        public decimal ValorDenatran { get; set; }
        public decimal ValorOrgaoAutuador { get; set; }
    }
}
