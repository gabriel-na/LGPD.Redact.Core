using Microsoft.Extensions.Options;

namespace LGPD.Redact.Core.Redactors;

public class TituloEleitorRedactor : LGPDRedactor
{
    public TituloEleitorRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal TituloEleitorRedactor() : base() { }

    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);

        if (Patterns.TituloEleitor().IsMatch(source))
        {
            int d = 0;
            for (int i = 0; i < destination.Length; i++)
            {
                if (char.IsDigit(destination[i]))
                {
                    d++;
                    if (d > 4 && d <= 8)
                        destination[i] = MaskChar;
                }
            }
        }

        return source.Length;
    }
}