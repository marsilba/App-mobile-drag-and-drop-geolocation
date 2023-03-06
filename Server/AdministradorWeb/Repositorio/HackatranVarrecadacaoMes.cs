using AdministradorWeb.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AdministradorWeb.Repositorio
{
    [Table("v_arrecadacao_mes")]
    [DataContract()]
    public class HackatranVarrecadacaoMes
    {
        [DataMember]
        [Key]
        [Column("MES")]
        public int Mes { get; set; }

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
