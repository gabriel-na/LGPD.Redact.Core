using Microsoft.Extensions.Options;

namespace EZ.Redact.Lgpd.Core.Redactors;

public class ContaBancariaRedactor : LGPDRedactor
{
    public ContaBancariaRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal ContaBancariaRedactor() : base() { }

    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);

        if (Patterns.ContaBancaria().IsMatch(source))
        {
            int dashPos = source.IndexOf('-');
            int start = 0;

            int dotPos = source.IndexOf('.');
            if (dotPos >= 0)
                start = dotPos + 1;

            for (int i = start; i < dashPos; i++)
                if (char.IsDigit(destination[i]))
                    destination[i] = MaskChar;
        }

        return source.Length;
    }
}
