using Microsoft.Extensions.Compliance.Redaction;

namespace LGPD.Redact.Core.Redactors;

public class GuidRedactor : Redactor
{
    protected virtual int PrefixHexCount => 4;
    protected virtual int SuffixHexCount => 4;

    public override int GetRedactedLength(ReadOnlySpan<char> input) => input.Length;

    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);

        if (Patterns.Guid().IsMatch(source))
        {
            int totalHex = 0;
            for (int i = 0; i < source.Length; i++)
                if (source[i] != '-')
                    totalHex++;

            int hexIdx = 0;
            for (int i = 0; i < destination.Length; i++)
            {
                if (destination[i] == '-') continue;
                hexIdx++;
                if (hexIdx > PrefixHexCount && hexIdx <= totalHex - SuffixHexCount)
                    destination[i] = '*';
            }
        }

        return source.Length;
    }
}
