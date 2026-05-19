using LGPD.Redact.Core;
using LGPD.Redact.Core.Redactors;
using LGPD.Redact.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Redaction;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adiciona mascaramento de dados.
    /// </summary>
    /// <param name="services"><inheritdoc cref="IServiceCollection"/></param>
    /// <param name="usarRedact">Se false então os dados serão exibidos.</param>
    /// <returns></returns>
    public static IServiceCollection AddLGPDRedaction(this IServiceCollection services, bool usarRedact = true)
    {
        services.AddRedaction(builder =>
        {
            if (usarRedact)
            {
                builder.SetRedactor<CartaoCreditoRedactor>(LGPDTaxonomy.CartaoCredito);
                builder.SetRedactor<CEPRedactor>(LGPDTaxonomy.CEP);
                builder.SetRedactor<CNPJRedactor>(LGPDTaxonomy.CNPJ);
                builder.SetRedactor<CPFRedactor>(LGPDTaxonomy.CPF);
                builder.SetRedactor<EmailRedactor>(LGPDTaxonomy.Email);
                builder.SetRedactor<EnderecoRedactor>(LGPDTaxonomy.Endereco);
                builder.SetRedactor<NomeRedactor>(LGPDTaxonomy.Nome);
                builder.SetRedactor<GuidRedactor>(LGPDTaxonomy.Guid);
                builder.SetRedactor<PixRedactor>(LGPDTaxonomy.Pix);
                builder.SetRedactor<TelefoneRedactor>(LGPDTaxonomy.Telefone);
                builder.SetRedactor<EnderecoIPRedactor>(LGPDTaxonomy.EnderecoIP);
                builder.SetRedactor<MacAddressRedactor>(LGPDTaxonomy.MacAddress);
                builder.SetRedactor<GeolocalizacaoRedactor>(LGPDTaxonomy.Geolocalizacao);
                builder.SetRedactor<CNHRedactor>(LGPDTaxonomy.CNH);
                builder.SetRedactor<TituloEleitorRedactor>(LGPDTaxonomy.TituloEleitor);
                builder.SetRedactor<PlacaRedactor>(LGPDTaxonomy.Placa);
                builder.SetRedactor<RenavamRedactor>(LGPDTaxonomy.Renavam);
                builder.SetRedactor<PISRedactor>(LGPDTaxonomy.PIS);
                builder.SetRedactor<CNSRedactor>(LGPDTaxonomy.CNS);
                builder.SetRedactor<CTPSRedactor>(LGPDTaxonomy.CTPS);
                builder.SetRedactor<CertidaoRedactor>(LGPDTaxonomy.Certidao);
                builder.SetRedactor<DataGenericaRedactor>(LGPDTaxonomy.DataGenerica);
                builder.SetRedactor<ContaBancariaRedactor>(LGPDTaxonomy.ContaBancaria);
                builder.SetRedactor<PassaporteRedactor>(LGPDTaxonomy.Passaporte);
                builder.SetRedactor<RNERedactor>(LGPDTaxonomy.RNE);
                builder.SetFallbackRedactor<ErasingRedactor>();
            }
            else
            {
                builder.SetFallbackRedactor<NullRedactor>();
            }
        });

        services.TryAddSingleton<CartaoCreditoRedactor>();
        services.TryAddSingleton<CEPRedactor>();
        services.TryAddSingleton<CNPJRedactor>();
        services.TryAddSingleton<CPFRedactor>();
        services.TryAddSingleton<EmailRedactor>();
        services.TryAddSingleton<EnderecoRedactor>();
        services.TryAddSingleton<NomeRedactor>();
        services.TryAddSingleton<PixRedactor>();
        services.TryAddSingleton<GuidRedactor>();
        services.TryAddSingleton<TelefoneRedactor>();
        services.TryAddSingleton<EnderecoIPRedactor>();
        services.TryAddSingleton<MacAddressRedactor>();
        services.TryAddSingleton<GeolocalizacaoRedactor>();
        services.TryAddSingleton<CNHRedactor>();
        services.TryAddSingleton<TituloEleitorRedactor>();
        services.TryAddSingleton<PlacaRedactor>();
        services.TryAddSingleton<RenavamRedactor>();
        services.TryAddSingleton<PISRedactor>();
        services.TryAddSingleton<CNSRedactor>();
        services.TryAddSingleton<CTPSRedactor>();
        services.TryAddSingleton<CertidaoRedactor>();
        services.TryAddSingleton<DataGenericaRedactor>();
        services.TryAddSingleton<ContaBancariaRedactor>();
        services.TryAddSingleton<PassaporteRedactor>();
        services.TryAddSingleton<RNERedactor>();

        services.TryAddSingleton<ILGPDRedactService, LGPDRedactService>();

        return services;
    }
}
