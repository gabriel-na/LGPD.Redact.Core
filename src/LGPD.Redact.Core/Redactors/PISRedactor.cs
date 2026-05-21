using Microsoft.Extensions.Options;

namespace LGPD.Redact.Core.Redactors;

public class PISRedactor : LGPDRedactor
{
    public PISRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal PISRedactor() : base() { }

    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);

        if (Patterns.PIS().IsMatch(source))
        {
            int d = 0;
            for (int i = 0; i < destination.Length; i++)
            {
                if (char.IsDigit(destination[i]))
                {
                    d++;
                    if (d > 3 && d <= 10)
                        destination[i] = MaskChar;
                }
            }
        }

        return source.Length;
    }
}