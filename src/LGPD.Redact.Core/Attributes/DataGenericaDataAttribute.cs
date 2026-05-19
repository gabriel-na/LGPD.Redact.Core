using LGPD.Redact.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace LGPD.Redact.Core.Attributes;

public class DataGenericaDataAttribute : DataClassificationAttribute
{
    public DataGenericaDataAttribute() : base(LGPDTaxonomy.DataGenerica) { }
}
