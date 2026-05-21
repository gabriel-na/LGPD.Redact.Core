using Microsoft.Extensions.Options;

namespace LGPD.Redact.Core.Redactors;

public class PixRedactor : GuidRedactor
{
    public PixRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal PixRedactor() : base() { }

    protected override int SuffixHexCount => 8;
}
