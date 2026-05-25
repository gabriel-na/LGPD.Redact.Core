using EZ.Redact.Lgpd.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace EZ.Redact.Lgpd.Core.Attributes;

public class RNEDataAttribute : DataClassificationAttribute
{
    public RNEDataAttribute() : base(LGPDTaxonomy.RNE) { }
}
