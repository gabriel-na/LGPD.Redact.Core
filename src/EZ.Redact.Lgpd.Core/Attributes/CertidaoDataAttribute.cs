using EZ.Redact.Lgpd.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace EZ.Redact.Lgpd.Core.Attributes;

public class CertidaoDataAttribute : DataClassificationAttribute
{
    public CertidaoDataAttribute() : base(LGPDTaxonomy.Certidao) { }
}
