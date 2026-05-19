using LGPD.Redact.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace LGPD.Redact.Core.Attributes;

public class ContaBancariaDataAttribute : DataClassificationAttribute
{
    public ContaBancariaDataAttribute() : base(LGPDTaxonomy.ContaBancaria) { }
}
