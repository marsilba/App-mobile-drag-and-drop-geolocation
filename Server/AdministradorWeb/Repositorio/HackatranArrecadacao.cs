using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AdministradorWeb.Repositorio
{
    [Table("arrecadacao"), DataContract()]
    public class HackatranArrecadacao
    {
        [DataMember]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public long Id { get; set; }

        [DataMember]
        [Column("UF")]
        public string Uf { get; set; }

        [DataMember]
        [Column("ORGAO_AUT")]
        public string OrgaoAutuador { get; set; }

        [DataMember]
        [Column("DETRAN_ARREC")]
        public string DetranArrecadacao { get; set; }

        [DataMember]
        [Column("QUANTIDADE")]
        public int Quantidade { get; set; }

        [DataMember]
        [Column("VALOR_BRUTO")]
        public decimal ValorBruto { get; set; }

        [DataMember]
        [Column("VALOR_FUNSET")]
        public decimal ValorFunset { get; set; }

        [DataMember]
        [Column("VALOR_DETRAN")]
        public decimal ValorDetran { get; set; }

        [DataMember]
        [Column("VALOR_DENATRAN")]
        public decimal ValorDenatran { get; set; }

        [DataMember]
        [Column("VALOR_ORGAO_AUT")]
        public decimal ValorOrgaoAutuador { get; set; }
    }
}
