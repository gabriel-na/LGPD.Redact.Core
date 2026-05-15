namespace LGPD.Redact.Core;

public interface ILGPDRedactService
{
    string Redact(DadoPessoal tipo, string input);
}
