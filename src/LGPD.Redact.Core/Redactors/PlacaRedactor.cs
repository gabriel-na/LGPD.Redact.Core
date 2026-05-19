using System;
using Microsoft.Extensions.Compliance.Redaction;

namespace LGPD.Redact.Core.Redactors;

public class PlacaRedactor : Redactor
{
    public override int GetRedactedLength(ReadOnlySpan<char> input) => input.Length;

    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);

        if (Patterns.PlacaAntiga().IsMatch(source))
        {
            for (int i = 0; i < destination.Length; i++)
                if (char.IsDigit(destination[i]))
                    destination[i] = '*';
        }
        else if (Patterns.PlacaMercosul().IsMatch(source))
        {
            for (int i = 3; i < destination.Length; i++)
                if (char.IsLetterOrDigit(destination[i]))
                    destination[i] = '*';
        }

        return source.Length;
    }
}
