using System;
using EZ.Redact.Lgpd.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace EZ.Redact.Lgpd.Core.Attributes;

public class TelefoneDataAttribute : DataClassificationAttribute 
{ 
    public TelefoneDataAttribute() : base(LGPDTaxonomy.Telefone) { } 
}
