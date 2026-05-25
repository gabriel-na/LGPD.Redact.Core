using Microsoft.Extensions.DependencyInjection;

namespace EZ.Redact.Lgpd.Core;

internal sealed class LGPDRedactionBuilder : ILGPDRedactionBuilder
{
    public IServiceCollection Services { get; }

    public LGPDRedactionBuilder(IServiceCollection services)
    {
        Services = services;
    }
}
