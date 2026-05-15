using LGPD.Redact.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;
using Microsoft.Extensions.Compliance.Redaction;

namespace LGPD.Redact.Core;

public class LGPDRedactService : ILGPDRedactService
{
    private static readonly Dictionary<DadoPessoal, DataClassification> _map = new()
    {
        [DadoPessoal.CPF] = LGPDTaxonomy.CPF,
        [DadoPessoal.CNPJ] = LGPDTaxonomy.CNPJ,
        [DadoPessoal.Nome] = LGPDTaxonomy.Nome,
        [DadoPessoal.Endereco] = LGPDTaxonomy.Endereco,
        [DadoPessoal.Telefone] = LGPDTaxonomy.Telefone,
        [DadoPessoal.Email] = LGPDTaxonomy.Email,
        [DadoPessoal.CartaoCredito] = LGPDTaxonomy.CartaoCredito,
        [DadoPessoal.CEP] = LGPDTaxonomy.CEP,
        [DadoPessoal.Pix] = LGPDTaxonomy.Pix,
        [DadoPessoal.EnderecoIP] = LGPDTaxonomy.EnderecoIP,
        [DadoPessoal.MacAddress] = LGPDTaxonomy.MacAddress,
        [DadoPessoal.Geolocalizacao] = LGPDTaxonomy.Geolocalizacao,
        [DadoPessoal.CNH] = LGPDTaxonomy.CNH,
        [DadoPessoal.TituloEleitor] = LGPDTaxonomy.TituloEleitor,
    };

    private readonly IRedactorProvider _provider;

    public LGPDRedactService(IRedactorProvider provider) => _provider = provider;

    public string Redact(DadoPessoal tipo, string input)
    {
        var redactor = _provider.GetRedactor(_map[tipo]);
        return redactor.Redact(input.AsSpan());
    }
}
