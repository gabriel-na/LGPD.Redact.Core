using EZ.Redact.Lgpd.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace EZ.Redact.Lgpd.Core.Attributes;

public class CNHDataAttribute : DataClassificationAttribute
{
    public CNHDataAttribute() : base(LGPDTaxonomy.CNH) { }
}
