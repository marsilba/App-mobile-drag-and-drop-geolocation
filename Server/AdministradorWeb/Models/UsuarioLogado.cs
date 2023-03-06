namespace AdministradorWeb.Models
{
    public static class UsuarioLogado
    {
        public static int Id { get; set; }
        public static string Login { get; set; }
        public static string Senha { get; set; }
        public static string Nome { get; set; }
        public static string Matricula { get; set; }
        public static string Perfil { get; set; }
        public static string Imagem { get; set; }
        public static int TipoLogin { get; set; }
    }
}