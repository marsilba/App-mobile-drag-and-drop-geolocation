using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AdministradorWeb.Repositorio
{
    [Table("obra_arte_especial"), DataContract()]
    public partial class HackatranObraArteEspecial
    {
        [DataMember]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int? Id { get; set; }

        [DataMember]
        [Column("ID_TIPO")]
        [Required(ErrorMessage = "Informe o tipo de obra.")]
        public int? TipoId { get; set; }

        [DataMember]
        [Column("TIPO")]
        public string Tipo { get; set; }


        [DataMember]
        [Column("ENDERECO")]
        [Required(ErrorMessage = "Clique no mapa para selecionar o endereço.")]
        public string Endereco { get; set; }


        [DataMember]
        [Column("LATITUDE")]
        [Required(ErrorMessage = "Clique no mapa para selecionar a latitude.")]
        public string Latitude { get; set; }


        [DataMember]
        [Column("LONGITUDE")]
        [Required(ErrorMessage = "Clique no mapa para selecionar a longitude.")]
        public string Longitude { get; set; }


        [DataMember]
        [Column("LARGURA")]
        public Nullable<decimal> Largura { get; set; }


        [DataMember]
        [Column("ALTURA")]
        public Nullable<decimal> Altura { get; set; }


        [DataMember]
        [Column("PESO")]
        public Nullable<decimal> Peso { get; set; }
    }
}