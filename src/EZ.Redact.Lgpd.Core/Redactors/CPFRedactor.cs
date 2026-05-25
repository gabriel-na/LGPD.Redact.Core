using Microsoft.Extensions.Options;

namespace EZ.Redact.Lgpd.Core.Redactors;

public class CPFRedactor : LGPDRedactor
{
    public CPFRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal CPFRedactor() : base() { }

    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);
        
        if (Patterns.CPF().IsMatch(source))
        {
            int d = 0;
            for (int i = 0; i < destination.Length; i++)
            {
                if (char.IsDigit(destination[i])) 
                { 
                    d++; 
                    if (d > 3 && d <= 9) 
                        destination[i] = MaskChar; 
                }
            }
        }

        return source.Length;
    }
}