using Microsoft.Extensions.Options;

namespace LGPD.Redact.Core.Redactors;

public class CNSRedactor : LGPDRedactor
{
    public CNSRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal CNSRedactor() : base() { }

    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);

        if (Patterns.CNS().IsMatch(source))
        {
            int d = 0;
            for (int i = 0; i < destination.Length; i++)
            {
                if (char.IsDigit(destination[i]))
                {
                    d++;
                    if (d > 3 && d <= 11)
                        destination[i] = MaskChar;
                }
            }
        }

        return source.Length;
    }
}