namespace AdministradorWeb.Models
{
    public static class Configurador
    {
        public static string NomeSistema { get; set; }

        public static string Orgao
        {
            get
            {
                return "DENATRAN - Departamento Nacional de Trânsito";
            }
        }

        public static string KeyGoogleMaps
        {
            get
            {
                return "AIzaSyDzvYG2MAc1kx8Ql72tO4UWOJMeIeVSQLw";

            }
        }

        public static string ModuloFinanceiro
        {
            get
            {
                return "Módulo Financeiro";
            }
        }

        public static string ModuloAet
        {
            get
            {
                return "Módulo AET";
            }
        }

        public static string ModuloLaboratorio
        {
            get
            {
                return "Módulo Laboratório";
            }
        }
    }
}