using Microsoft.Extensions.Options;

namespace EZ.Redact.Lgpd.Core.Redactors;

public class NomeRedactor : IniciaisRedactor
{
    public NomeRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal NomeRedactor() : base() { }
}
