using LGPD.Redact.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace LGPD.Redact.Core.Attributes;

public class CTPSDataAttribute : DataClassificationAttribute
{
    public CTPSDataAttribute() : base(LGPDTaxonomy.CTPS) { }
}
