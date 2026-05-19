using LGPD.Redact.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace LGPD.Redact.Core.Attributes;

public class RenavamDataAttribute : DataClassificationAttribute
{
    public RenavamDataAttribute() : base(LGPDTaxonomy.Renavam) { }
}
