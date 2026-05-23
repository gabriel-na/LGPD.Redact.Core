using Microsoft.Extensions.DependencyInjection;

namespace EZ.Redact.Lgpd.Core;

/// <summary>
/// Builder para configuração de redação LGPD.
/// Serve como ponto de partida para extensões de outros pacotes
/// (ex: <c>EZ.Redact.Lgpd.Serialization</c>, <c>EZ.Redact.Lgpd.EntityFramework</c>).
/// </summary>
public interface ILGPDRedactionBuilder
{
    /// <summary>
    /// O <see cref="IServiceCollection"/> onde os serviços são registrados.
    /// </summary>
    IServiceCollection Services { get; }
}
