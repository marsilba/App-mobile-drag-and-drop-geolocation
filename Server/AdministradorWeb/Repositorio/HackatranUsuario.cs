using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AdministradorWeb.Repositorio
{
    [Table("usuario")]
    [DataContract()]
    public class HackatranUsuario
    {
        [DataMember]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [DataMember]
        [Column("login")]
        public string Login { get; set; }

        [DataMember]
        [Column("senha")]
        public string Senha { get; set; }

        [DataMember]
        [Column("nome")]
        public string Nome { get; set; }

        [DataMember]
        [Column("matricula")]
        public string Matricula { get; set; }

        [DataMember]
        [Column("perfil")]
        public string Perfil { get; set; }

        [DataMember]
        [Column("id_ambulante")]
        public long? IdAmbulante { get; set; }

        [DataMember]
        [Column("dt_registro")]
        public DateTime DtRegistro { get; set; }
    }
}