using Microsoft.Extensions.Options;

namespace EZ.Redact.Lgpd.Core.Redactors;

public class PixRedactor : GuidRedactor
{
    public PixRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal PixRedactor() : base() { }

    protected override int SuffixHexCount => 8;
}
