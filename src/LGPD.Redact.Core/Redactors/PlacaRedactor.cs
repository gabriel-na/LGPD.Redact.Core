using Microsoft.Extensions.Options;

namespace LGPD.Redact.Core.Redactors;

public class PlacaRedactor : LGPDRedactor
{
    public PlacaRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal PlacaRedactor() : base() { }

    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);

        if (Patterns.PlacaAntiga().IsMatch(source))
        {
            for (int i = 0; i < destination.Length; i++)
                if (char.IsDigit(destination[i]))
                    destination[i] = MaskChar;
        }
        else if (Patterns.PlacaMercosul().IsMatch(source))
        {
            for (int i = 3; i < destination.Length; i++)
                if (char.IsLetterOrDigit(destination[i]))
                    destination[i] = MaskChar;
        }

        return source.Length;
    }
}
