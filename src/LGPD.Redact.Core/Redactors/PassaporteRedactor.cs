using Microsoft.Extensions.Options;

namespace LGPD.Redact.Core.Redactors;

public class PassaporteRedactor : LGPDRedactor
{
    public PassaporteRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal PassaporteRedactor() : base() { }

    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);

        if (Patterns.Passaporte().IsMatch(source))
        {
            int digitStart = 0;
            while (digitStart < source.Length && !char.IsDigit(source[digitStart]))
                digitStart++;

            int total = 0;
            foreach (char c in source)
                if (char.IsDigit(c))
                    total++;

            int d = 0;
            for (int i = digitStart; i < destination.Length; i++)
            {
                if (char.IsDigit(destination[i]))
                {
                    d++;
                    if (d <= total - 2)
                        destination[i] = MaskChar;
                }
            }
        }

        return source.Length;
    }
}