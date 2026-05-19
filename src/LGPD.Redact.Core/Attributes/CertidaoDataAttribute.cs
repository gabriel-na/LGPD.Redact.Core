using LGPD.Redact.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace LGPD.Redact.Core.Attributes;

public class CertidaoDataAttribute : DataClassificationAttribute
{
    public CertidaoDataAttribute() : base(LGPDTaxonomy.Certidao) { }
}
