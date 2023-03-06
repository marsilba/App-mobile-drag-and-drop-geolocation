using AdministradorWeb.Repositorio;
using BoletoNetCore;
using System;
using System.Linq;

namespace AdministradorWeb.Models
{
    public class BoletoDenatran
    {
        private readonly string BeneficiarioCodigo = "1700000";
        private readonly string BeneficiarioCodigoAgencia = "9999-9";
        private readonly string BeneficiarioNumeroConta = "09999-9";
        private readonly string BeneficiarioCpfCnpj = "05.465.986/0001-99";
        private readonly string BeneficiarioNome = "Departamento Nacional de Trânsito";
        private readonly string BeneficiarioEndereco = "Esplanada dos Ministérios";
        private readonly string BeneficiarioEnderecoNum = "S/Nº";
        private readonly string BeneficiarioEnderecoUf = "DF";
        private readonly string BeneficiarioEnderecoBairro = "Asa Sul";
        private readonly string BeneficiarioEnderecoCidade = "Brasília";
        private readonly string BeneficiarioEnderecoCep = "70297-400";
        private readonly string BeneficiarioCarteiraPadrao = "17";
        private readonly string NossoNumero = "17000000878765264";

        public string BoletoTeste(HackatranBoleto _pagador)
        {
            var agencia = BeneficiarioCodigoAgencia.Split('-');
            var conta = BeneficiarioNumeroConta.Split('-');
            var bancoEnum = Bancos.BancoDoBrasil;

            var contaBancaria = new ContaBancaria
            {
                Agencia = agencia.FirstOrDefault(),
                DigitoAgencia = agencia.Length > 1 ? agencia.Last() : string.Empty,
                Conta = conta.First(),
                DigitoConta = conta.Last(),
                CarteiraPadrao = BeneficiarioCarteiraPadrao,
                TipoCarteiraPadrao = TipoCarteira.CarteiraCobrancaSimples,
                TipoFormaCadastramento = TipoFormaCadastramento.ComRegistro,
                TipoImpressaoBoleto = TipoImpressaoBoleto.Banco
            };

            if (bancoEnum == Bancos.BancoDoBrasil)
            {
                contaBancaria.VariacaoCarteiraPadrao = "019";
            }

            var banco = Banco.Instancia(bancoEnum);

            banco.Beneficiario = new Beneficiario
            {
                CPFCNPJ = BeneficiarioCpfCnpj,
                Codigo = BeneficiarioCodigo,
                Nome = BeneficiarioNome,
                Endereco = new BoletoNetCore.Endereco
                {
                    LogradouroEndereco = BeneficiarioEndereco,
                    LogradouroNumero = BeneficiarioEnderecoNum,
                    UF = BeneficiarioEnderecoUf,
                    Cidade = BeneficiarioEnderecoCidade,
                    Bairro = BeneficiarioEnderecoBairro,
                    CEP = BeneficiarioEnderecoCep
                },

                ContaBancaria = contaBancaria
            };

            banco.FormataBeneficiario();

            var boleto = new Boleto(banco)
            {
                Pagador = new Pagador
                {
                    CPFCNPJ = _pagador.CpfCnpj,
                    Nome = _pagador.Nome,
                    Endereco = new BoletoNetCore.Endereco
                    {
                        LogradouroEndereco = _pagador.Logradouro,
                        LogradouroNumero = _pagador.LogradouroNumero,
                        Bairro = _pagador.Bairro,
                        Cidade = _pagador.Cidade,
                        UF = _pagador.Uf,
                        CEP = _pagador.Cep,
                    }
                },

                NumeroDocumento = _pagador.NumeroDocumento,
                NossoNumero = NossoNumero,
                DataEmissao = DateTime.Today,
                DataVencimento = DateTime.Today.AddDays(10)
            };

            boleto.ValorTitulo = _pagador.Valor;
            boleto.EspecieDocumento = TipoEspecieDocumento.DM;

            boleto.ValidarDados();

            var boletoBancario = new BoletoBancario
            {
                Boleto = boleto,
            };

            var html = boletoBancario.MontaHtmlEmbedded();
            return html;
        }
    }
}