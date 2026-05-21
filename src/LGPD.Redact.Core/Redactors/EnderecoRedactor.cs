using Microsoft.Extensions.Options;

namespace LGPD.Redact.Core.Redactors;

public class EnderecoRedactor : IniciaisRedactor
{
    public EnderecoRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal EnderecoRedactor() : base() { }
}
