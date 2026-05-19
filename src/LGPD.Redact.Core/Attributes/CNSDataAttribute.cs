using LGPD.Redact.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace LGPD.Redact.Core.Attributes;

public class CNSDataAttribute : DataClassificationAttribute
{
    public CNSDataAttribute() : base(LGPDTaxonomy.CNS) { }
}
