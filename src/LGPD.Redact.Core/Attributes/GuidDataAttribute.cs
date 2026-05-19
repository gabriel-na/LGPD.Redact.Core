using LGPD.Redact.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace LGPD.Redact.Core.Attributes;

public class GuidDataAttribute : DataClassificationAttribute
{
    public GuidDataAttribute() : base(LGPDTaxonomy.Guid) { }
}
