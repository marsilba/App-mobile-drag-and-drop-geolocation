using AdministradorWeb.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AdministradorWeb.Repositorio
{
    [Table("exame"), DataContract()]
    public partial class HackatranExame
    {
        [DataMember]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int? Id { get; set; }


        [DataMember]
        [Column("NOME")]
        public string Nome { get; set; }


        [DataMember]
        [Column("CNH")]
        public string Cnh { get; set; }


        [DataMember]
        [Column("CPF")]
        public string Cpf { get; set; }

        [DataMember]
        [Column("DATA")]
        [JsonConverter(typeof(JsonDateFormatConverter), "dd/MM/yyyy")]
        public Nullable<DateTime> Data { get; set; }


        [DataMember]
        [Column("ID_LABORATORIO")]
        public Nullable<int> LaboratorioId { get; set; }


        [DataMember]
        [Column("LABORATORIO")]
        public string Laboratorio { get; set; }


        [DataMember]
        [Column("RESULTADO")]
        public Nullable<bool> Resultado { get; set; }


        [NotMapped]
        public bool FlagResultado { get; set; }

        [DataMember]
        [Column("FOTO")]
        public string UrlFoto { get; set; }


        [NotMapped]
        public IFormFile Foto { get; set; }

    }
}