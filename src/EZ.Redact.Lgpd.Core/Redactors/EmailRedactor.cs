using Microsoft.Extensions.Options;

namespace EZ.Redact.Lgpd.Core.Redactors;

public class EmailRedactor : LGPDRedactor
{
    public EmailRedactor(IOptions<LGPDRedactOptions> options) : base(options) { }
    internal EmailRedactor() : base() { }

    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        source.CopyTo(destination);

        if (Patterns.Email().IsMatch(source))
        {
            int atIndex = source.IndexOf('@');
            
            if (atIndex > 1)
            {
                for (int i = 1; i < atIndex; i++)
                    destination[i] = MaskChar;
            }
            else if (atIndex == 1)
            {
            }
        }
        else
        {
            destination.Fill(MaskChar);
        }

        return source.Length;
    }
}
