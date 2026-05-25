using EZ.Redact.Lgpd.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace EZ.Redact.Lgpd.Core.Attributes;

public class CNSDataAttribute : DataClassificationAttribute
{
    public CNSDataAttribute() : base(LGPDTaxonomy.CNS) { }
}
