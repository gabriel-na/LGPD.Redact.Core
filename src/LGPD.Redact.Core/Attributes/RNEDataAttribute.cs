using LGPD.Redact.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace LGPD.Redact.Core.Attributes;

public class RNEDataAttribute : DataClassificationAttribute
{
    public RNEDataAttribute() : base(LGPDTaxonomy.RNE) { }
}
