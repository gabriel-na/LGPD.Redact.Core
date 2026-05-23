using Microsoft.Extensions.Options;

namespace EZ.Redact.Lgpd.Core.Redactors;

public class EnderecoRedactor : IniciaisRedactor
{
    public EnderecoRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal EnderecoRedactor() : base() { }
}
