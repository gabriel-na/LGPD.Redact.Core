using EZ.Redact.Lgpd.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace EZ.Redact.Lgpd.Core.Attributes;

public class CTPSDataAttribute : DataClassificationAttribute
{
    public CTPSDataAttribute() : base(LGPDTaxonomy.CTPS) { }
}
