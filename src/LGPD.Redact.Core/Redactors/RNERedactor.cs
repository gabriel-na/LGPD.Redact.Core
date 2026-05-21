using Microsoft.Extensions.Options;

namespace LGPD.Redact.Core.Redactors;

public class RNERedactor : LGPDRedactor
{
    public RNERedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal RNERedactor() : base() { }

    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);

        if (Patterns.RNE().IsMatch(source))
        {
            int start = char.IsLetter(source[0]) ? 1 : 0;
            int dashPos = source.IndexOf('-');

            for (int i = start; i < dashPos; i++)
                if (char.IsDigit(destination[i]))
                    destination[i] = MaskChar;
        }

        return source.Length;
    }
}
