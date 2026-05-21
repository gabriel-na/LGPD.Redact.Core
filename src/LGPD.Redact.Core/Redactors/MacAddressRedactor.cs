using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;

namespace LGPD.Redact.Core.Redactors;

public class MacAddressRedactor : LGPDRedactor
{
    public MacAddressRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal MacAddressRedactor() : base() { }

    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);

        var sourceStr = source.ToString();
        var matches = Patterns.MacAddress().Matches(sourceStr);

        foreach (Match match in matches)
        {
            int separatorsFound = 0;
            for (int i = match.Index + match.Length - 1; i >= match.Index; i--)
            {
                if (destination[i] == ':' || destination[i] == '-')
                {
                    separatorsFound++;
                    if (separatorsFound == 3) break;
                }
                if (char.IsLetterOrDigit(destination[i]))
                    destination[i] = MaskChar;
            }
        }

        return source.Length;
    }
}
