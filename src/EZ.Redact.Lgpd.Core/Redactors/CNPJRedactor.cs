using Microsoft.Extensions.Options;

namespace EZ.Redact.Lgpd.Core.Redactors;

public class CNPJRedactor : LGPDRedactor
{
    public CNPJRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal CNPJRedactor() : base() { }
    
    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);
        
        if (Patterns.CNPJ().IsMatch(source))
        {
            int d = 0;
            
            for (int i = 0; i < destination.Length; i++)
            {
                if (char.IsLetterOrDigit(destination[i]))
                {
                    d++;
                    if (d > 2 && d <= 8) 
                        destination[i] = MaskChar;
                }
            }
        }
        
        return source.Length;
    }
}