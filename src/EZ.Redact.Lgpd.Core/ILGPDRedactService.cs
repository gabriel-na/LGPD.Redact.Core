namespace EZ.Redact.Lgpd.Core;

public interface ILGPDRedactService
{
    string Redact(DadoPessoal tipo, string input);
}
