using Microsoft.Extensions.Compliance.Redaction;

namespace LGPD.Redact.Core.Redactors;

public class ContaBancariaRedactor : Redactor
{
    public override int GetRedactedLength(ReadOnlySpan<char> input) => input.Length;

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
                    destination[i] = '*';
        }

        return source.Length;
    }
}
