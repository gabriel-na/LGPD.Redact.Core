using LGPD.Redact.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace LGPD.Redact.Core.Attributes;

public class PISDataAttribute : DataClassificationAttribute
{
    public PISDataAttribute() : base(LGPDTaxonomy.PIS) { }
}
