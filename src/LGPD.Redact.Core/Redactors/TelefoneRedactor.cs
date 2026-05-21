using Microsoft.Extensions.Options;

namespace LGPD.Redact.Core.Redactors;

public class TelefoneRedactor : LGPDRedactor
{
    public TelefoneRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal TelefoneRedactor() : base() { }
    
    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);
        
        var sourceStr = source.ToString();
        
        if (string.IsNullOrEmpty(sourceStr))
            return source.Length;
        
        var match = Patterns.Telefone().Match(sourceStr);
        
        if (match.Success)
        {
            int total = 0;
            
            foreach (var c in destination) 
                if (char.IsDigit(c)) 
                    total++;
            
            int cur = 0;
            
            for (int i = 0; i < destination.Length; i++)
            {
                if (char.IsDigit(destination[i]))
                {
                    cur++;
                    
                    if (cur > 3 && cur <= (total - 4)) 
                        destination[i] = MaskChar;
                }
            }
        }
        
        return source.Length;
    }
}