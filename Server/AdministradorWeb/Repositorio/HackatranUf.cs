using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AdministradorWeb.Repositorio
{
    [Table("uf"), DataContract()]
    public partial class HackatranUf
    {
        [DataMember]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public long Id { get; set; }


        [DataMember]
        [Column("SIGLA")]
        public string Sigla { get; set; }


        [DataMember]
        [Column("NOME")]
        public string Nome { get; set; }


        [DataMember]
        [Column("LATITUDE")]
        public string Latitude { get; set; }


        [DataMember]
        [Column("LONGITUDE")]
        public string Longitude { get; set; }
    }
}