using Microsoft.Extensions.Options;

namespace EZ.Redact.Lgpd.Core.Redactors;

public class CEPRedactor : LGPDRedactor
{
    public CEPRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal CEPRedactor() : base() { }

    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);
        
        var match = Patterns.CEP().Match(source.ToString());
        
        if (match.Success)
        {
            int d = 0;

            for (int i = destination.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(destination[i]))
                {
                    destination[i] = MaskChar;
                    d++;
                }
                
                if (d == 3) 
                    break;
            }
        }
        
        return source.Length;
    }
}