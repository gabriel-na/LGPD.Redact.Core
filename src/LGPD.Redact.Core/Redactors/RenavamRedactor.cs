using Microsoft.Extensions.Options;

namespace LGPD.Redact.Core.Redactors;

public class RenavamRedactor : LGPDRedactor
{
    public RenavamRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal RenavamRedactor() : base() { }

    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);

        if (Patterns.Renavam().IsMatch(source))
        {
            int d = 0;
            for (int i = 0; i < destination.Length; i++)
            {
                if (char.IsDigit(destination[i]))
                {
                    d++;
                    if (d > 3 && d <= 8)
                        destination[i] = MaskChar;
                }
            }
        }

        return source.Length;
    }
}