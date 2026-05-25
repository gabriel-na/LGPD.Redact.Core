using EZ.Redact.Lgpd.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace EZ.Redact.Lgpd.Core.Attributes;

public class GeolocalizacaoDataAttribute : DataClassificationAttribute
{
    public GeolocalizacaoDataAttribute() : base(LGPDTaxonomy.Geolocalizacao) { }
}
