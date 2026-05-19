using LGPD.Redact.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace LGPD.Redact.Core.Attributes;

public class PassaporteDataAttribute : DataClassificationAttribute
{
    public PassaporteDataAttribute() : base(LGPDTaxonomy.Passaporte) { }
}
