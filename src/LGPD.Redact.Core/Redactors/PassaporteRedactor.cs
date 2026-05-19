using Microsoft.Extensions.Compliance.Redaction;

namespace LGPD.Redact.Core.Redactors;

public class PassaporteRedactor : Redactor
{
    public override int GetRedactedLength(ReadOnlySpan<char> input) => input.Length;

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
                        destination[i] = '*';
                }
            }
        }

        return source.Length;
    }
}
