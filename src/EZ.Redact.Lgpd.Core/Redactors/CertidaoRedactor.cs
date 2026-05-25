using Microsoft.Extensions.Options;

namespace EZ.Redact.Lgpd.Core.Redactors;

public class CertidaoRedactor : LGPDRedactor
{
    public CertidaoRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal CertidaoRedactor() : base() { }

    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);

        if (Patterns.Certidao().IsMatch(source))
        {
            int totalDigits = 0;
            foreach (char c in source)
                if (char.IsDigit(c))
                    totalDigits++;

            int d = 0;
            for (int i = 0; i < destination.Length; i++)
            {
                if (char.IsDigit(destination[i]))
                {
                    d++;
                    if (d > 6 && d <= totalDigits - 2)
                        destination[i] = MaskChar;
                }
            }
        }

        return source.Length;
    }
}