using Microsoft.Extensions.Compliance.Redaction;
using Microsoft.Extensions.Options;

namespace LGPD.Redact.Core.Redactors;

public abstract class LGPDRedactor : Redactor
{
    private readonly LGPDRedactOptions _options;

    protected LGPDRedactor(IOptions<LGPDRedactOptions> options)
    {
        _options = options?.Value ?? new();
    }

    internal LGPDRedactor() : this(Microsoft.Extensions.Options.Options.Create(new LGPDRedactOptions())) { }

    public override int GetRedactedLength(ReadOnlySpan<char> input) => input.Length;

    /// <summary>
    /// Caractere utilizado para mascarar os dados. Configurável via <see cref="LGPDRedactOptions.MaskChar"/>.
    /// </summary>
    protected char MaskChar => _options.MaskChar;

    /// <summary>
    /// Instância completa de <see cref="LGPDRedactOptions"/> para acesso a opções específicas por redator.
    /// </summary>
    protected LGPDRedactOptions Options => _options;
}
