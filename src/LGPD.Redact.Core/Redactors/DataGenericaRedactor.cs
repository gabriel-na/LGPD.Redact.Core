using Microsoft.Extensions.Options;

namespace LGPD.Redact.Core.Redactors;

public class DataGenericaRedactor : LGPDRedactor
{
    public DataGenericaRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal DataGenericaRedactor() : base() { }

    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);

        int dashCount = 0;
        foreach (char c in source)
            if (c == '-')
                dashCount++;

        if (dashCount == 2 && Patterns.DataIso().IsMatch(source))
        {
            for (int i = 5; i < destination.Length; i++)
                if (char.IsDigit(destination[i]))
                    destination[i] = MaskChar;
        }
        else if (Patterns.DataBrasil().IsMatch(source))
        {
            int lastSlash = source.LastIndexOf('/');
            for (int i = 0; i < lastSlash; i++)
                if (char.IsDigit(destination[i]))
                    destination[i] = MaskChar;
        }

        return source.Length;
    }
}
