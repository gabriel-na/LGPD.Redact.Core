using EZ.Redact.Lgpd.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace EZ.Redact.Lgpd.Core.Attributes;

public class EnderecoIPDataAttribute : DataClassificationAttribute
{
    public EnderecoIPDataAttribute() : base(LGPDTaxonomy.EnderecoIP) { }
}
