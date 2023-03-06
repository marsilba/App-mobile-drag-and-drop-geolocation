using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AdministradorWeb.Repositorio
{
    [Table("polilinha"), DataContract()]
    public partial class HackatranPolilinha
    {
        [DataMember]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }


        [DataMember]
        [Column("ROTA")]
        public string Rota { get; set; }


        [DataMember]
        [Column("HTML")]
        public string Html { get; set; }


        [DataMember]
        [Column("PLACA")]
        public string Placa { get; set; }
    }
}