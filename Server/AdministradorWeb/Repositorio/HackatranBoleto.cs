using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AdministradorWeb.Repositorio
{
    [Table("boleto")]
    [DataContract()]
    public class HackatranBoleto
    {
        [DataMember]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }

        [DataMember]
        [Column("CPF_CNPJ")]
        public string CpfCnpj { get; set; }

        [DataMember]
        [Column("NUM_DOCUMENTO")]
        public string NumeroDocumento { get; set; }

        [DataMember]
        [Column("NOME")]
        public string Nome { get; set; }

        [DataMember]
        [Column("ENDERECO_LOGRADOURO")]
        public string Logradouro { get; set; }

        [DataMember]
        [Column("ENDERECO_NUMERO")]
        public string LogradouroNumero { get; set; }

        [DataMember]
        [Column("ENDERECO_BAIRRO")]
        public string Bairro { get; set; }

        [DataMember]
        [Column("ENDERECO_CIDADE")]
        public string Cidade { get; set; }

        [DataMember]
        [Column("ENDERECO_UF")]
        public string Uf { get; set; }

        [DataMember]
        [Column("ENDERECO_CEP")]
        public string Cep { get; set; }

        [DataMember]
        [Column("VENCIMENTO")]
        public DateTime Vencimento { get; set; }

        [DataMember]
        [Column("VALOR")]
        public decimal Valor { get; set; }
    }
}