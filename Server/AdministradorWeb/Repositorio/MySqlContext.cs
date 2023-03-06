using Microsoft.EntityFrameworkCore;
namespace AdministradorWeb.Repositorio
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options)
            : base(options)
        { }

        public MySqlContext()
            : base()
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HackatranUsuario>().ToTable("usuario");
            modelBuilder.Entity<HackatranBoleto>().ToTable("boleto");
            modelBuilder.Entity<HackatranUf>().ToTable("uf");
            modelBuilder.Entity<HackatranArrecadacao>().ToTable("arrecadacao");
            modelBuilder.Entity<HackatranVarrecadacaoUf>().ToTable("v_arrecadacao_uf");
            modelBuilder.Entity<HackatranVarrecadacaoMes>().ToTable("v_arrecadacao_mes");
            modelBuilder.Entity<HackatranVarrecadacaoGeral>().ToTable("v_arrecadacao_geral");
            modelBuilder.Entity<HackatranExame>().ToTable("exame");
            modelBuilder.Entity<HackatranPolilinha>().ToTable("polilinha");
            modelBuilder.Entity<HackatranLaboratorio>().ToTable("laboratorio");
            modelBuilder.Entity<HackatranObraArteEspecial>().ToTable("obra_arte_especial");
            modelBuilder.Entity<HackatranVeiculo>().ToTable("veiculo");
        }

        public DbSet<HackatranUsuario> HackatranUsuarios { get; set; }
        public DbSet<HackatranBoleto> HackatranBoletos { get; set; }
        public DbSet<HackatranUf> HackatranUfs { get; set; }
        public DbSet<HackatranArrecadacao> HackatranArrecadacoes { get; set; }
        public DbSet<HackatranVarrecadacaoUf> HackatranVarrecadacoesUfs { get; set; }
        public DbSet<HackatranVarrecadacaoMes> HackatranVarrecadacoesMeses { get; set; }
        public DbSet<HackatranVarrecadacaoGeral> ViewArrecadacoesGerais { get; set; }
        public DbSet<HackatranExame> HackatranExames { get; set; }
        public DbSet<HackatranPolilinha> HackatranPolilinhas { get; set; }
        public DbSet<HackatranLaboratorio> HackatranLaboratorios { get; set; }
        public DbSet<HackatranObraArteEspecial> HackatranObrasArteEspeciais { get; set; }
        public DbSet<HackatranVeiculo> HackatranVeiculos { get; set; }
    }
}