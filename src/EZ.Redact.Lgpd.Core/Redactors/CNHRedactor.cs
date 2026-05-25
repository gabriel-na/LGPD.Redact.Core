using Microsoft.Extensions.Options;

namespace EZ.Redact.Lgpd.Core.Redactors;

public class CNHRedactor : LGPDRedactor
{
    public CNHRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal CNHRedactor() : base() { }

    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);

        if (Patterns.CNH().IsMatch(source))
        {
            int d = 0;
            for (int i = 0; i < destination.Length; i++)
            {
                if (char.IsDigit(destination[i]))
                {
                    d++;
                    if (d > 3 && d <= 9)
                        destination[i] = MaskChar;
                }
            }
        }

        return source.Length;
    }
}