using Microsoft.Extensions.Options;

namespace LGPD.Redact.Core.Redactors;

public class NomeRedactor : IniciaisRedactor
{
    public NomeRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal NomeRedactor() : base() { }
}
