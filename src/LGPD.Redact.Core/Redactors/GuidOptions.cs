namespace LGPD.Redact.Core.Redactors;

/// <summary>
/// Opções de redação para identificadores GUID/UUID.
/// Quando uma propriedade não é definida (null), o valor padrão do redator é utilizado.
/// </summary>
public class GuidOptions
{
    /// <summary>
    /// Quantidade de dígitos hexadecimais a preservar no início do GUID.
    /// Padrão do <see cref="GuidRedactor"/>: 4.
    /// Padrão do <see cref="PixRedactor"/>: 4.
    /// </summary>
    public int? PrefixHexCount { get; set; }

    /// <summary>
    /// Quantidade de dígitos hexadecimais a preservar no final do GUID.
    /// Padrão do <see cref="GuidRedactor"/>: 4.
    /// Padrão do <see cref="PixRedactor"/>: 8.
    /// </summary>
    public int? SuffixHexCount { get; set; }
}
