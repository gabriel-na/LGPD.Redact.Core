using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;

namespace LGPD.Redact.Core.Redactors;

public class GeolocalizacaoRedactor : LGPDRedactor
{
    public GeolocalizacaoRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal GeolocalizacaoRedactor() : base() { }

    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);

        var sourceStr = source.ToString();
        var matches = Patterns.Geolocalizacao().Matches(sourceStr);

        foreach (Match match in matches)
        {
            for (int i = match.Index; i < match.Index + match.Length; i++)
            {
                if (destination[i] == '.')
                {
                    for (int j = i + 1; j < match.Index + match.Length && char.IsDigit(destination[j]); j++)
                        destination[j] = MaskChar;
                }
            }
        }

        return source.Length;
    }
}
