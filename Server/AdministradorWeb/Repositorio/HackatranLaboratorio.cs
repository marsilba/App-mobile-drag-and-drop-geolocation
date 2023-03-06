using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AdministradorWeb.Repositorio
{
    [Table("laboratorio"), DataContract()]
    public partial class HackatranLaboratorio
    {
        [DataMember]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public long? Id { get; set; }


        [DataMember]
        [Column("NOME")]
        public string Nome { get; set; }


        [DataMember]
        [Column("ENDERECO")]
        public string Endereco { get; set; }


        [DataMember]
        [Column("HORARIO")]
        public string Horario { get; set; }


        [DataMember]
        [Column("LATITUDE")]
        public string Latitude { get; set; }


        [DataMember]
        [Column("LONGITUDE")]
        public string Longitude { get; set; }


        [NotMapped, JsonIgnore]
        public List<SelectListItem> Laboratorios { get; set; }

    }
}