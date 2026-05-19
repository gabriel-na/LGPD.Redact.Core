using System;
using System.Text.RegularExpressions;

namespace LGPD.Redact.Core;

public static partial class Patterns
{
    [GeneratedRegex(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$")]
    public static partial Regex CPF();

    [GeneratedRegex(@"^[A-Z0-9]{2}\.?[A-Z0-9]{3}\.?[A-Z0-9]{3}/?[A-Z0-9]{4}-?\d{2}$", RegexOptions.IgnoreCase)]
    public static partial Regex CNPJ();

    [GeneratedRegex(@"^(?:\+\d{3}\s\(\d{2}\)\s\d\s\d{4}-\d{4}|\+\d{3}\s\d{2}\s\d\s\d{8}|\+\d{3}\s\d{2}\s\d{9}|\(\d{2}\)\s\d\s\d{4}-\d{4}|\d{2}\s\d\s\d{4}-\d{4}|\d{2}\s\d\s\d{8}|\d{2}\s\d{9})$")]
    public static partial Regex Telefone();

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    public static partial Regex Email();

    [GeneratedRegex(@"^[0-9a-f]{8}-?[0-9a-f]{4}-?[0-9a-f]{4}-?[0-9a-f]{4}-?[0-9a-f]{12}$", RegexOptions.IgnoreCase)]
    public static partial Regex Guid();

    [GeneratedRegex(@"[0-9a-f]{8}-?[0-9a-f]{4}-?[0-9a-f]{4}-?[0-9a-f]{4}-?[0-9a-f]{12}", RegexOptions.IgnoreCase)]
    public static partial Regex PixChaveAleatoria();

    [GeneratedRegex(@"\b(?:\d[ -]*?){13,19}\b")]
    public static partial Regex CartaoCredito();

    [GeneratedRegex(@"\d{5}-?\d{3}")]
    public static partial Regex CEP();

    [GeneratedRegex(@"\b(?:(?:25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(?:25[0-5]|2[0-4]\d|[01]?\d\d?)\b")]
    public static partial Regex Ipv4();

    [GeneratedRegex(@"\b(?:[0-9a-fA-F]{1,4}:){7}[0-9a-fA-F]{1,4}\b")]
    public static partial Regex Ipv6();

    [GeneratedRegex(@"\b(?:[0-9a-fA-F]{2}[:-]){5}[0-9a-fA-F]{2}\b", RegexOptions.IgnoreCase)]
    public static partial Regex MacAddress();

    [GeneratedRegex(@"-?\d{1,2}\.\d{2,}\s*,\s*-?\d{1,3}\.\d{2,}")]
    public static partial Regex Geolocalizacao();

    [GeneratedRegex(@"^\d{11}$")]
    public static partial Regex CNH();

    [GeneratedRegex(@"^\d{4}[\.\s]?\d{4}[\.\s]?\d{4}$")]
    public static partial Regex TituloEleitor();

    [GeneratedRegex(@"^[A-Za-z]{3}-?\d{4}$")]
    public static partial Regex PlacaAntiga();

    [GeneratedRegex(@"^[A-Za-z]{3}\d[A-Za-z]\d{2}$")]
    public static partial Regex PlacaMercosul();

    [GeneratedRegex(@"^\d{11}$")]
    public static partial Regex Renavam();

    [GeneratedRegex(@"^\d{3}\.?\d{5}\.?\d{2}-?\d{1}$")]
    public static partial Regex PIS();

    [GeneratedRegex(@"^\d{3}\s?\d{4}\s?\d{4}\s?\d{4}$")]
    public static partial Regex CNS();

    [GeneratedRegex(@"^\d{7,14}$")]
    public static partial Regex CTPS();

    [GeneratedRegex(@"^\d{6}\.?\d{2}\.?\d{4}\.?\d{1}\.?\d{1}\.?\d{4}\.?\d{1}\.?\d{5}-?\d{2}$")]
    public static partial Regex Certidao();

    [GeneratedRegex(@"^\d{1,2}/\d{2}/\d{2,4}$")]
    public static partial Regex DataBrasil();

    [GeneratedRegex(@"^\d{4}-\d{2}-\d{2}$")]
    public static partial Regex DataIso();

    [GeneratedRegex(@"^(\d{2,3}\.)?\d{5,12}-[0-9A-Za-z]{1}$")]
    public static partial Regex ContaBancaria();

    [GeneratedRegex(@"^[A-Z]{0,3}[-\s]?\d{6,9}$")]
    public static partial Regex Passaporte();

    [GeneratedRegex(@"^[A-Z]?\d{6,7}-\d{1}$")]
    public static partial Regex RNE();
}
