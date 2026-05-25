using System;
using EZ.Redact.Lgpd.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;

namespace EZ.Redact.Lgpd.Core.Attributes;

public class EmailDataAttribute : DataClassificationAttribute 
{ 
    public EmailDataAttribute() : base(LGPDTaxonomy.Email) { } 
}
