using EZ.Redact.Lgpd.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace EZ.Redact.Lgpd.Core.Attributes;

public class PassaporteDataAttribute : DataClassificationAttribute
{
    public PassaporteDataAttribute() : base(LGPDTaxonomy.Passaporte) { }
}
