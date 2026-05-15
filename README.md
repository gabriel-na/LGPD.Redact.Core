# LGPD.Redact

[![NuGet Version](https://img.shields.io/badge/nuget-v1.0.0-blue.svg)](https://www.nuget.org/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET 8.0+](https://img.shields.io/badge/.NET-8.0%2B%20|%209.0%2B%20|%2010.0%2B-512bd4.svg)](https://dotnet.microsoft.com/download)

**LGPD.Redact** é uma biblioteca de alta performance para redação (mascaramento) de dados sensíveis (PII) em conformidade com a LGPD. Construída sobre o framework oficial de conformidade da Microsoft, ela utiliza `Span<char>` para garantir alocação zero e máxima velocidade durante o processamento de logs e telemetria.

---

## Instalação

```bash
dotnet add package LGPD.Redact.Core
```

Registre os servicos no DI com `AddLGPDRedaction()`:

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLGPDRedaction();
builder.Logging.EnableRedaction(options => options.ApplyDiscriminator = false);
```

> **Importante**: O nome do parâmetro do método deve corresponder exatamente ao nome do placeholder na mensagem do `[LoggerMessage]`. O source generator do Microsoft.Extensions.Logging valida essa correspondência em tempo de build.

```csharp
// ✅ Correto - nomes correspondentes
[LoggerMessage(Message = "Meu nome é {Nome}")]
public static partial void LogNome(this ILogger logger, [NomeData] string nome);

// ❌ Erro de build - nomes não correspondentes
[LoggerMessage(Message = "Meu nome é {PessoaNome}")]
public static partial void LogNome(this ILogger logger, [NomeData] string nome);
```

---

## Atributos Suportados

| Atributo | O que faz? | Exemplo Original | Exemplo Redigido |
| :--- | :--- | :--- | :--- |
| `[CPFData]` | Preserva 3 primeiros e 2 ultimos digitos | `123.456.789-01` | `123.***.***-01` |
| `[CNPJData]` | Preserva raiz (2 digitos) e radical (6 ultimos) | `12.345.678/0001-90` | `12.***.***/0001-90` |
| `[EmailData]` | Preserva inicial e dominio | `felipe.siqueira@gmail.com` | `f**************@gmail.com` |
| `[TelefoneData]` | Preserva DDD, 1 digito apos DDD e 4 ultimos | `(11) 98888-4444` | `(11) 9****-4444` |
| `[CEPData]` | Mascara os 3 ultimos digitos | `01310-900` | `01310-***` |
| `[NomeData]` | Mantem apenas as iniciais de cada palavra | `Maria da Silva` | `M**** d* S****` |
| `[EnderecoData]` | Mantem apenas as iniciais, oculta numeros | `Avenida Paulista, 1000` | `A****** P*******, ****` |
| `[PixData]` | Mascara chave aleatoria mantendo 4 primeiros e 8 ultimos | `e8d26618-2e11-4b22-8d26-66182e114b22` | `e8d2****-****-****-****-****2e114b22` |
| `[CartaoCreditoData]` | Preserva 4 primeiros e 4 ultimos digitos | `4532 1178 9012 3456` | `4532 **** **** 3456` |
| `[EnderecoIPData]` | Mascara os 2 ultimos octetos (IPv4) e os ultimos 3 grupos (IPv6) | `192.168.1.100` | `192.168.*.***` |
| &nbsp; | &nbsp; | `2001:0db8:85a3:0000:0000:8a2e:0370:7334` | `2001:0db8:85a3:0000:0000:****:****:****` |
| `[MacAddressData]` | Preserva prefixo OUI (3 primeiros bytes) | `00:1A:2B:3C:4D:5E` | `00:1A:2B:**:**:**` |
| `[GeolocalizacaoData]` | Mascara parte decimal de latitude e longitude | `-23.5505, -46.6333` | `-23.****, -46.****` |
| `[CNHData]` | Preserva 3 primeiros e 2 ultimos digitos | `12345678901` | `123******01` |
| `[TituloEleitorData]` | Preserva 4 primeiros e 4 ultimos digitos | `1234.5678.9012` | `1234.****.9012` |

---

## Uso com `ILGPDRedactService`

Injete `ILGPDRedactService` e utilize o enum `DadoPessoal` para redigir valores avulsos sem precisar lidar com `DataClassification`:

```csharp
public class MeuServico
{
    private readonly ILGPDRedactService _redact;

    public MeuServico(ILGPDRedactService redact) => _redact = redact;

    public void Executar()
    {
        string cpf = _redact.Redact(DadoPessoal.CPF, "123.456.789-09");
        string email = _redact.Redact(DadoPessoal.Email, "usuario@example.com");
    }
}
```

### Enum `DadoPessoal`

Cada valor do enum mapeia para um tipo de dado pessoal:

| Valor | Descricao |
| :--- | :--- |
| `CPF` | Cadastro de Pessoas Físicas |
| `CNPJ` | Cadastro Nacional da Pessoa Jurídica |
| `Nome` | Nome completo |
| `Endereco` | Endereco completo |
| `Telefone` | Telefone fixo ou celular |
| `Email` | Endereco de e-mail |
| `CartaoCredito` | Número do cartão de crédito |
| `CEP` | Código de Enderecamento Postal |
| `Pix` | Chave aleatória Pix |
| `EnderecoIP` | Endereco IPv4 ou IPv6 |
| `MacAddress` | Endereco MAC |
| `Geolocalizacao` | Coordenadas de latitude e longitude |
| `CNH` | Carteira Nacional de Habilitacao |
| `TituloEleitor` | Titulo de Eleitor |

---

## Licenca

Distribuido sob a licenca MIT.
