using Microsoft.Extensions.Options;

namespace EZ.Redact.Lgpd.Core.Redactors;

public class GuidRedactor : LGPDRedactor
{
    public GuidRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal GuidRedactor() : base() { }

    protected virtual int PrefixHexCount => 4;
    protected virtual int SuffixHexCount => 4;

    private int ResolvePrefix() => Options.Guid.PrefixHexCount ?? PrefixHexCount;
    private int ResolveSuffix() => Options.Guid.SuffixHexCount ?? SuffixHexCount;

    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);

        if (Patterns.Guid().IsMatch(source))
        {
            int totalHex = 0;
            for (int i = 0; i < source.Length; i++)
                if (source[i] != '-')
                    totalHex++;

            int hexIdx = 0;
            for (int i = 0; i < destination.Length; i++)
            {
                if (destination[i] == '-') continue;
                hexIdx++;
                if (hexIdx > ResolvePrefix() && hexIdx <= totalHex - ResolveSuffix())
                    destination[i] = MaskChar;
            }
        }

        return source.Length;
    }
}
