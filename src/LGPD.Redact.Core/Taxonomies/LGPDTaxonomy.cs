using System;
using Microsoft.Extensions.Compliance.Classification;

namespace LGPD.Redact.Core.Taxonomies;

public static class LGPDTaxonomy
{
    public static DataClassification CPF => new("LGPD", "CPF");
    public static DataClassification CNPJ => new("LGPD", "CNPJ");
    public static DataClassification Nome => new("LGPD", "Nome");
    public static DataClassification Endereco => new("LGPD", "Endereco");
    public static DataClassification Telefone => new("LGPD", "Telefone");
    public static DataClassification Email => new("LGPD", "Email");
    public static DataClassification CartaoCredito => new("LGPD", "CartaoCredito");
    public static DataClassification CEP => new("LGPD", "CEP");
    public static DataClassification Guid => new("LGPD", "Guid");
    public static DataClassification Pix => new("LGPD", "PIX");
    public static DataClassification EnderecoIP => new("LGPD", "EnderecoIP");
    public static DataClassification MacAddress => new("LGPD", "MacAddress");
    public static DataClassification Geolocalizacao => new("LGPD", "Geolocalizacao");
    public static DataClassification CNH => new("LGPD", "CNH");
    public static DataClassification TituloEleitor => new("LGPD", "TituloEleitor");
    public static DataClassification Placa => new("LGPD", "Placa");
    public static DataClassification Renavam => new("LGPD", "Renavam");
    public static DataClassification PIS => new("LGPD", "PIS");
    public static DataClassification CNS => new("LGPD", "CNS");
    public static DataClassification CTPS => new("LGPD", "CTPS");
    public static DataClassification Certidao => new("LGPD", "Certidao");
    public static DataClassification DataGenerica => new("LGPD", "DataGenerica");
    public static DataClassification ContaBancaria => new("LGPD", "ContaBancaria");
    public static DataClassification Passaporte => new("LGPD", "Passaporte");
    public static DataClassification RNE => new("LGPD", "RNE");
}
