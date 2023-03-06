using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AdministradorWeb.Repositorio
{
    [Table("v_arrecadacao_geral")]
    [DataContract()]
    public class HackatranVarrecadacaoGeral
    {
        [DataMember]
        [Key]
        [Column("ID")]
        public int Id { get; set; }

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
