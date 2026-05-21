using LGPD.Redact.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Redaction;

namespace LGPD.Redact.Core;

public class LGPDRedactService : ILGPDRedactService
{
    private readonly IRedactorProvider _provider;

    public LGPDRedactService(IRedactorProvider provider) => _provider = provider;

    public string Redact(DadoPessoal tipo, string input)
    {
        var taxonomy = LGPDTaxonomy.FromDadoPessoal(tipo);
        var redactor = _provider.GetRedactor(taxonomy);
        return redactor.Redact(input.AsSpan());
    }
}
