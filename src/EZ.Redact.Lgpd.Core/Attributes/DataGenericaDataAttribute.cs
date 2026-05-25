using EZ.Redact.Lgpd.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace EZ.Redact.Lgpd.Core.Attributes;

public class DataGenericaDataAttribute : DataClassificationAttribute
{
    public DataGenericaDataAttribute() : base(LGPDTaxonomy.DataGenerica) { }
}
