using EZ.Redact.Lgpd.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace EZ.Redact.Lgpd.Core.Attributes;

public class ContaBancariaDataAttribute : DataClassificationAttribute
{
    public ContaBancariaDataAttribute() : base(LGPDTaxonomy.ContaBancaria) { }
}
