using Microsoft.Extensions.Compliance.Redaction;

namespace LGPD.Redact.Core.Redactors;

public class CTPSRedactor : Redactor
{
    public override int GetRedactedLength(ReadOnlySpan<char> input) => input.Length;

    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);

        if (Patterns.CTPS().IsMatch(source))
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
                    if (d > 3 && d <= totalDigits - 3)
                        destination[i] = '*';
                }
            }
        }

        return source.Length;
    }
}
