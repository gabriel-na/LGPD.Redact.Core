using System;
using Microsoft.Extensions.Compliance.Redaction;

namespace LGPD.Redact.Core.Redactors;

public class RenavamRedactor : Redactor
{
    public override int GetRedactedLength(ReadOnlySpan<char> input) => input.Length;

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
                        destination[i] = '*';
                }
            }
        }

        return source.Length;
    }
}
