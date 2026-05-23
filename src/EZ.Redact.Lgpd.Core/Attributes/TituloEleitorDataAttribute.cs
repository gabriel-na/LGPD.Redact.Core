using EZ.Redact.Lgpd.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace EZ.Redact.Lgpd.Core.Attributes;

public class TituloEleitorDataAttribute : DataClassificationAttribute
{
    public TituloEleitorDataAttribute() : base(LGPDTaxonomy.TituloEleitor) { }
}
