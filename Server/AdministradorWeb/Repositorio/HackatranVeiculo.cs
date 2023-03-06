using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AdministradorWeb.Repositorio
{
    [Table("veiculo"), DataContract()]
    public partial class HackatranVeiculo
    {
        [DataMember]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int? Id { get; set; }


        [DataMember]
        [Column("MOTORISTA")]
        public string Motorista { get; set; }


        [DataMember]
        [Column("CNH")]
        public string Cnh { get; set; }


        [DataMember]
        [Column("PLACA")]
        public string Placa { get; set; }


        [DataMember]
        [Column("COMPRIMENTO")]
        public decimal? Comprimento { get; set; }



        [DataMember]
        [Column("LARGURA")]
        public decimal? Largura { get; set; }


        [DataMember]
        [Column("ALTURA")]
        public decimal? Altura { get; set; }


        [DataMember]
        [Column("PESO")]
        public decimal? Peso { get; set; }

        [DataMember]
        [Column("FOTO")]
        public string UrlFoto { get; set; }


        [NotMapped, JsonIgnore]
        public IFormFile Foto { get; set; }
    }
}