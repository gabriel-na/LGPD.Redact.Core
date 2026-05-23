using EZ.Redact.Lgpd.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace EZ.Redact.Lgpd.Core.Attributes;

public class MacAddressDataAttribute : DataClassificationAttribute
{
    public MacAddressDataAttribute() : base(LGPDTaxonomy.MacAddress) { }
}
