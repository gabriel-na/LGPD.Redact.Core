using LGPD.Redact.Core.Redactors;

namespace LGPD.Redact.Core;

/// <summary>
/// Opções de configuração para redação de dados pessoais.
/// </summary>
public class LGPDRedactOptions
{
    /// <summary>
    /// Caractere utilizado para mascarar os dados. Padrão: <c>'*'</c>.
    /// </summary>
    public char MaskChar { get; set; } = '*';

    /// <summary>
    /// Opções específicas para redação de GUIDs/UUIDs.
    /// </summary>
    public GuidOptions Guid { get; set; } = new();

    /// <summary>
    /// Chave HMAC em Base64 usada pelo redator HMAC.
    /// Obrigatória quando <see cref="HmacFor"/> não estiver vazio.
    /// </summary>
    public string? HmacKey { get; set; }

    /// <summary>
    /// Identificador da chave HMAC. Padrão: <c>1</c>.
    /// </summary>
    public int HmacKeyId { get; set; } = 1;

    /// <summary>
    /// Conjunto de tipos de dados pessoais que devem usar redação HMAC
    /// (hash determinístico) em vez de mascaramento.
    /// </summary>
    public HashSet<DadoPessoal> HmacFor { get; set; } = new();
}
