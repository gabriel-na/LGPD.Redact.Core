using LGPD.Redact.Core;
using LGPD.Redact.Core.Redactors;
using LGPD.Redact.Core.Taxonomies;
using Microsoft.Extensions.Compliance.Classification;
using Microsoft.Extensions.Compliance.Redaction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adiciona mascaramento de dados com opções padrão.
    /// </summary>
    /// <param name="services"><inheritdoc cref="IServiceCollection"/></param>
    /// <param name="usarRedact">Se false então os dados serão exibidos.</param>
    /// <returns></returns>
    public static IServiceCollection AddLGPDRedaction(this IServiceCollection services, bool usarRedact = true)
        => AddLGPDRedaction(services, _ => { }, usarRedact);

    /// <summary>
    /// Adiciona mascaramento de dados com opções customizadas.
    /// </summary>
    /// <param name="services"><inheritdoc cref="IServiceCollection"/></param>
    /// <param name="configure">
    /// Ação para configurar as opções de redação.
    /// Exemplo: <c>options =&gt; { options.MaskChar = '#'; options.Guid.PrefixHexCount = 6; }</c>
    /// </param>
    /// <param name="usarRedact">Se false então os dados serão exibidos.</param>
    /// <returns></returns>
    public static IServiceCollection AddLGPDRedaction(this IServiceCollection services, Action<LGPDRedactOptions> configure, bool usarRedact = true)
    {
        services.Configure(configure);

        var options = new LGPDRedactOptions();
        configure(options);

        return AddLGPDRedactionCore(services, options, usarRedact);
    }

    /// <summary>
    /// Adiciona mascaramento de dados lendo opções de uma seção do <see cref="IConfiguration"/>.
    /// A seção deve conter as propriedades de <see cref="LGPDRedactOptions"/>,
    /// incluindo a chave HMAC (<c>HmacKey</c>) quando <c>HmacFor</c> não estiver vazio.
    /// </summary>
    /// <param name="services"><inheritdoc cref="IServiceCollection"/></param>
    /// <param name="configuration">Configuração do aplicativo.</param>
    /// <param name="sectionName">Nome da seção de configuração. Padrão: <c>"LGPD"</c>.</param>
    /// <param name="usarRedact">Se false então os dados serão exibidos.</param>
    /// <returns></returns>
    public static IServiceCollection AddLGPDRedaction(this IServiceCollection services, IConfiguration configuration, string sectionName = "LGPD", bool usarRedact = true)
    {
        var section = configuration.GetSection(sectionName);
        var options = new LGPDRedactOptions();
        section.Bind(options);
        services.Configure<LGPDRedactOptions>(section);
        return AddLGPDRedactionCore(services, options, usarRedact);
    }

    private static IServiceCollection AddLGPDRedactionCore(IServiceCollection services, LGPDRedactOptions options, bool usarRedact)
    {
        services.AddRedaction(builder =>
        {
            if (usarRedact)
            {
                var hmacSets = new List<DataClassificationSet>();

                Registrar(builder, ref hmacSets, options, DadoPessoal.CartaoCredito, static b => b.SetRedactor<CartaoCreditoRedactor>(LGPDTaxonomy.CartaoCredito));
                Registrar(builder, ref hmacSets, options, DadoPessoal.CEP, static b => b.SetRedactor<CEPRedactor>(LGPDTaxonomy.CEP));
                Registrar(builder, ref hmacSets, options, DadoPessoal.CNPJ, static b => b.SetRedactor<CNPJRedactor>(LGPDTaxonomy.CNPJ));
                Registrar(builder, ref hmacSets, options, DadoPessoal.CPF, static b => b.SetRedactor<CPFRedactor>(LGPDTaxonomy.CPF));
                Registrar(builder, ref hmacSets, options, DadoPessoal.Email, static b => b.SetRedactor<EmailRedactor>(LGPDTaxonomy.Email));
                Registrar(builder, ref hmacSets, options, DadoPessoal.Endereco, static b => b.SetRedactor<EnderecoRedactor>(LGPDTaxonomy.Endereco));
                Registrar(builder, ref hmacSets, options, DadoPessoal.Nome, static b => b.SetRedactor<NomeRedactor>(LGPDTaxonomy.Nome));
                Registrar(builder, ref hmacSets, options, DadoPessoal.Guid, static b => b.SetRedactor<GuidRedactor>(LGPDTaxonomy.Guid));
                Registrar(builder, ref hmacSets, options, DadoPessoal.Pix, static b => b.SetRedactor<PixRedactor>(LGPDTaxonomy.Pix));
                Registrar(builder, ref hmacSets, options, DadoPessoal.Telefone, static b => b.SetRedactor<TelefoneRedactor>(LGPDTaxonomy.Telefone));
                Registrar(builder, ref hmacSets, options, DadoPessoal.EnderecoIP, static b => b.SetRedactor<EnderecoIPRedactor>(LGPDTaxonomy.EnderecoIP));
                Registrar(builder, ref hmacSets, options, DadoPessoal.MacAddress, static b => b.SetRedactor<MacAddressRedactor>(LGPDTaxonomy.MacAddress));
                Registrar(builder, ref hmacSets, options, DadoPessoal.Geolocalizacao, static b => b.SetRedactor<GeolocalizacaoRedactor>(LGPDTaxonomy.Geolocalizacao));
                Registrar(builder, ref hmacSets, options, DadoPessoal.CNH, static b => b.SetRedactor<CNHRedactor>(LGPDTaxonomy.CNH));
                Registrar(builder, ref hmacSets, options, DadoPessoal.TituloEleitor, static b => b.SetRedactor<TituloEleitorRedactor>(LGPDTaxonomy.TituloEleitor));
                Registrar(builder, ref hmacSets, options, DadoPessoal.Placa, static b => b.SetRedactor<PlacaRedactor>(LGPDTaxonomy.Placa));
                Registrar(builder, ref hmacSets, options, DadoPessoal.Renavam, static b => b.SetRedactor<RenavamRedactor>(LGPDTaxonomy.Renavam));
                Registrar(builder, ref hmacSets, options, DadoPessoal.PIS, static b => b.SetRedactor<PISRedactor>(LGPDTaxonomy.PIS));
                Registrar(builder, ref hmacSets, options, DadoPessoal.CNS, static b => b.SetRedactor<CNSRedactor>(LGPDTaxonomy.CNS));
                Registrar(builder, ref hmacSets, options, DadoPessoal.CTPS, static b => b.SetRedactor<CTPSRedactor>(LGPDTaxonomy.CTPS));
                Registrar(builder, ref hmacSets, options, DadoPessoal.Certidao, static b => b.SetRedactor<CertidaoRedactor>(LGPDTaxonomy.Certidao));
                Registrar(builder, ref hmacSets, options, DadoPessoal.DataGenerica, static b => b.SetRedactor<DataGenericaRedactor>(LGPDTaxonomy.DataGenerica));
                Registrar(builder, ref hmacSets, options, DadoPessoal.ContaBancaria, static b => b.SetRedactor<ContaBancariaRedactor>(LGPDTaxonomy.ContaBancaria));
                Registrar(builder, ref hmacSets, options, DadoPessoal.Passaporte, static b => b.SetRedactor<PassaporteRedactor>(LGPDTaxonomy.Passaporte));
                Registrar(builder, ref hmacSets, options, DadoPessoal.RNE, static b => b.SetRedactor<RNERedactor>(LGPDTaxonomy.RNE));

                if (hmacSets.Count > 0)
                {
                    ArgumentException.ThrowIfNullOrEmpty(options.HmacKey);

#pragma warning disable EXTEXP0002
                    builder.SetHmacRedactor(hmacOpts =>
                    {
                        hmacOpts.Key = options.HmacKey;
                        hmacOpts.KeyId = options.HmacKeyId;
                    }, [.. hmacSets]);
#pragma warning restore EXTEXP0002
                }

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

    private static void Registrar(IRedactionBuilder builder, ref List<DataClassificationSet> hmacSets, LGPDRedactOptions options, DadoPessoal tipo, Action<IRedactionBuilder> setRedator)
    {
        if (options.HmacFor.Contains(tipo))
            hmacSets.Add(new DataClassificationSet(LGPDTaxonomy.FromDadoPessoal(tipo)));
        else
            setRedator(builder);
    }
}
