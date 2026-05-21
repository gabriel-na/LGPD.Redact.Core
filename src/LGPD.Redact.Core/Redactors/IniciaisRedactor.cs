using Microsoft.Extensions.Options;

namespace LGPD.Redact.Core.Redactors;

public abstract class IniciaisRedactor : LGPDRedactor
{
    protected IniciaisRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal IniciaisRedactor() : base() { }
    
    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);
        
        bool proximoEhInicial = true;
        
        for (int i = 0; i < destination.Length; i++)
        {
            if (char.IsWhiteSpace(destination[i]) || destination[i] == ',' || destination[i] == '-')
            {
                proximoEhInicial = true;
                continue;
            }
            
            if (char.IsLetter(destination[i]) && proximoEhInicial)
                proximoEhInicial = false;
            else
                destination[i] = MaskChar;
        }
        
        return source.Length;
    }
}