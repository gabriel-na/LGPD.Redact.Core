using LGPD.Redact.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace LGPD.Redact.Core.Attributes;

public class PlacaDataAttribute : DataClassificationAttribute
{
    public PlacaDataAttribute() : base(LGPDTaxonomy.Placa) { }
}
