using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;

namespace LGPD.Redact.Core.Redactors;

public class EnderecoIPRedactor : LGPDRedactor
{
    public EnderecoIPRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal EnderecoIPRedactor() : base() { }

    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);

        var sourceStr = source.ToString();

        var ipv4Matches = Patterns.Ipv4().Matches(sourceStr);
        foreach (Match match in ipv4Matches)
        {
            int dotsFound = 0;
            for (int i = match.Index; i < match.Index + match.Length; i++)
            {
                if (destination[i] == '.')
                {
                    dotsFound++;
                    continue;
                }
                if (dotsFound >= 2)
                    destination[i] = MaskChar;
            }
        }

        var ipv6Matches = Patterns.Ipv6().Matches(sourceStr);
        foreach (Match match in ipv6Matches)
        {
            int colonsFound = 0;
            for (int i = match.Index; i < match.Index + match.Length; i++)
            {
                if (destination[i] == ':')
                {
                    colonsFound++;
                    continue;
                }
                if (colonsFound >= 5 && destination[i] != '.')
                    destination[i] = MaskChar;
            }
        }

        return source.Length;
    }
}
