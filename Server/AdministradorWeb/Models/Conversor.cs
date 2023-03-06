namespace AdministradorWeb.Models
{
    public static class Conversor
    {
        public static string TipoOCorrencia(string value)
        {
            switch (value)
            {
                case "1":
                    return "Atendimento";

                case "2":
                    return "CMA/GCM";

                case "3":
                    return "Criminal";

                case "4":
                    return "Ordem Pública";

                case "5":
                    return "Trânsito";

                case "6":
                    return "Outros";

                default:
                    return "";
            }
        }

        public static string SubTipoOCorrencia(string value)
        {
            switch (value)
            {
                case "1":
                    return "192 SAMU";

                case "2":
                    return "193 Bombeiro Combatente";

                case "3":
                    return "193 Bombeiro Socorrista";

                case "4":
                    return "199 Defesa Civil";

                case "5":
                    return "Outros";

                case "6":
                    return "Combate a Incêncio";

                case "7":
                    return "Fiscalização Ambiental";

                case "8":
                    return "Resgate de Animais";

                case "9":
                    return "Ameaça";

                case "10":
                    return "Cercamento";

                case "11":
                    return "Conduta Incoveniente";

                case "12":
                    return "Furto";

                case "13":
                    return "Homicídio";

                case "14":
                    return "Lesão Corporal";

                case "15":
                    return "Roubo";

                case "16":
                    return "Apoio Social";

                case "17":
                    return "Controle Urbano";

                case "18":
                    return "Posturas";

                case "19":
                    return "Acidente com Vítima";

                case "20":
                    return "Acidente sem Vítima";

                case "21":
                    return "Fiscalização";

                case "22":
                    return "Semafórica";

                case "23":
                    return "Elogios";

                case "24":
                    return "Críticas";

                case "25":
                    return "Sugestões";

                case "26":
                    return "Suporte / Chamados";

                default:
                    return "";
            }
        }
    }
}
