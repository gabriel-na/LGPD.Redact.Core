using Microsoft.Extensions.Options;

namespace LGPD.Redact.Core.Redactors;

public class CartaoCreditoRedactor : LGPDRedactor
{
    public CartaoCreditoRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal CartaoCreditoRedactor() : base() { }

    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);
        
        if (Patterns.CartaoCredito().IsMatch(source))
        {
            int total = 0;
            
            foreach (var c in destination) 
                if (char.IsDigit(c)) total++;
            
            int cur = 0;
            
            for (int i = 0; i < destination.Length; i++)
            {
                if (char.IsDigit(destination[i])) 
                { 
                    cur++; 
                    
                    if (cur > 4 && cur <= (total - 4)) 
                        destination[i] = MaskChar; 
                }
            }
        }
        
        return source.Length;
    }
}