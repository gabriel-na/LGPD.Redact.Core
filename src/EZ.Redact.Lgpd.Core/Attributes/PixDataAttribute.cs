using System;
using EZ.Redact.Lgpd.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace EZ.Redact.Lgpd.Core.Attributes;

public class PixDataAttribute : DataClassificationAttribute 
{ 
    public PixDataAttribute() : base(LGPDTaxonomy.Pix) { } 
}
