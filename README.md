# LGPD.Redact

[![NuGet Version](https://img.shields.io/badge/nuget-v1.2.0-blue.svg)](https://www.nuget.org/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET 8.0+](https://img.shields.io/badge/.NET-8.0%2B%20|%209.0%2B%20|%2010.0%2B-512bd4.svg)](https://dotnet.microsoft.com/download)

**LGPD.Redact** é uma biblioteca de alta performance para redação de dados sensíveis (PII) em conformidade com a LGPD. Construída sobre o framework oficial de conformidade da Microsoft, ela utiliza `Span<char>` para garantir alocação zero e máxima velocidade durante o processamento de logs, telemetria, e APIs REST.

Oferece dois modos de redação:

| Modo | Descrição | Uso típico |
| :--- | :--- | :--- |
| **Masking** (padrão) | Substitui caracteres por um caractere de máscara, preservando formato e parte dos dados | Respostas de API, telas, relatórios |
| **HMAC** | Hash determinístico com chave secreta — irreversível, mas mesmo input produz mesmo output | Logs de auditoria, analytics, correlação |

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

## Exemplo rápido

Veja a diferença entre masking e HMAC para o mesmo CPF:

```csharp
var cpf = redact.Redact(DadoPessoal.CPF, "123.456.789-01");
// Modo masking: "123.***.***-01"
// Modo HMAC:    "1:a1b2c3d4e5f6g7h8i9j0k1l2m3n4o5p=="
```

## Configuração

### `LGPDRedactOptions`

| Propriedade | Padrão | Descrição |
| :--- | :--- | :--- |
| `MaskChar` | `'*'` | Caractere usado no mascaramento |
| `Guid` | `new()` | Opções de redação de GUID (ver abaixo) |
| `HmacKey` | `null` | Chave HMAC em Base64 (obrigatória se `HmacFor` não estiver vazio) |
| `HmacKeyId` | `1` | Identificador da chave para rotação |
| `HmacFor` | `HashSet<>` vazio | Tipos de dado que devem usar HMAC em vez de masking |

### `GuidOptions`

| Propriedade | Padrão | Descrição |
| :--- | :--- | :--- |
| `PrefixHexCount` | `4` | Quantidade de hex digits preservados no prefixo |
| `SuffixHexCount` | `4` | Quantidade de hex digits preservados no sufixo |

### Três formas de configurar

**1. Em código (`Action<LGPDRedactOptions>`)**
```csharp
builder.Services.AddLGPDRedaction(options =>
{
    options.MaskChar = '#';
    options.Guid.PrefixHexCount = 6;
    options.HmacKey = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    options.HmacFor.Add(DadoPessoal.CPF);
});
```

**2. Via `IConfiguration` (appsettings.json + env vars)**
```csharp
// appsettings.json com seção "LGPD"
builder.Services.AddLGPDRedaction(builder.Configuration);
```

```json
{
  "LGPD": {
    "MaskChar": "#",
    "Guid": { "PrefixHexCount": 6 },
    "HmacFor": ["CPF"],
    "HmacKeyId": 1
  }
}
```

A `HmacKey` **não deve** ficar no `appsettings.json` (evite vazar a chave). Use variável de ambiente ou User Secrets:

```bash
# Environment variable (Linux/macOS)
export LGPD__HmacKey="suachavebase64aqui=="

# Windows CMD
set LGPD__HmacKey=suachavebase64aqui==
```

Ela também pode vir de qualquer outra fonte da cadeia de configuração: `appsettings.Development.json`, Azure Key Vault, ou gerada programaticamente.

**3. Combinando ambas**
```csharp
// Lê defaults do config, depois sobrescreve programaticamente
builder.Services.AddLGPDRedaction(options =>
{
    options.HmacKey = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
});
builder.Services.PostConfigure<LGPDRedactOptions>(opts =>
{
    opts.HmacFor.Add(DadoPessoal.CPF);
});
```

---

## HMAC Redaction

Ao ativar HMAC para um tipo (ex.: CPF), o valor é substituído por um hash determinístico formatado como `{KeyId}:{Base64}`:

| Tipo | Exemplo |
| :--- | :--- |
| Mascarado | `123.***.***-01` |
| HMAC | `1:8fLm3q...==` |

### Vantagens do HMAC

| Característica | Descrição |
| :--- | :--- |
| **Determinístico** | Mesmo CPF sempre produz o mesmo hash — permite correlação entre sistemas |
| **Irreversível** | Sem a chave secreta ninguém recupera o original |
| **Key rotation** | Altere o `HmacKeyId` para trocar a chave; hashes antigos continuam válidos |

### Comportamento

O mesmo input sempre produz o mesmo hash (dentro do mesmo `HmacKeyId`):

```csharp
redact.Redact(DadoPessoal.CPF, "123.456.789-01"); // 1:8fLm3q...==
redact.Redact(DadoPessoal.CPF, "123.456.789-01"); // 1:8fLm3q...== (mesmo valor)
redact.Redact(DadoPessoal.CPF, "987.654.321-00"); // 1:XyZ9a...==  (outro hash)
```

---

## Atributos Suportados

### Identificação Pessoal

| Atributo | O que faz? | Exemplo Original | Exemplo Redigido |
| :--- | :--- | :--- | :--- |
| `[NomeData]` | Mantem apenas as iniciais de cada palavra | `Maria da Silva` | `M**** d* S****` |
| `[CPFData]` | Preserva 3 primeiros e 2 ultimos digitos | `123.456.789-01` | `123.***.***-01` |
| `[CNPJData]` | Preserva raiz (2 caracteres) e radical (6 ultimos) | `12.345.678/0001-90` | `12.***.***/0001-90` |
| &nbsp; | &nbsp; | `AB.CDE.FGH/0001-90` | `AB.***.***/0001-90` |
| &nbsp; | &nbsp; | `12ABC34501DE90` (alfanumérico) | `12******01DE90` |
| `[EmailData]` | Preserva inicial e dominio | `felipe.siqueira@gmail.com` | `f**************@gmail.com` |
| `[TelefoneData]` | Preserva DDD, 1 digito apos DDD e 4 ultimos | `(11) 98888-4444` | `(11) 9****-4444` |
| `[EnderecoData]` | Mantem apenas as iniciais, oculta numeros | `Avenida Paulista, 1000` | `A****** P*******, ****` |
| `[DataGenericaData]` | Preserva ano, mascara dia/mes | `15/03/1990` | `**/**/1990` |
| &nbsp; | &nbsp; | `1990-03-15` | `1990-**-**` |

### Documentos Oficiais

| Atributo | O que faz? | Exemplo Original | Exemplo Redigido |
| :--- | :--- | :--- | :--- |
| `[CNHData]` | Preserva 3 primeiros e 2 ultimos digitos | `12345678901` | `123******01` |
| `[TituloEleitorData]` | Preserva 4 primeiros e 4 ultimos digitos | `1234.5678.9012` | `1234.****.9012` |
| `[PISData]` | Preserva 3 primeiros e digito verificador | `123.45678.90-1` | `123.*****.**-1` |
| &nbsp; | &nbsp; | `12345678901` | `123*******1` |
| `[CNSData]` | Preserva 3 primeiros e 4 ultimos | `123 4567 8901 2345` | `123 **** **** 2345` |
| &nbsp; | &nbsp; | `123456789012345` | `123********2345` |
| `[CTPSData]` | Preserva 3 primeiros e 3 ultimos | `1234567890` | `123****890` |
| `[CertidaoData]` | Preserva 6 primeiros e 2 verificadores | `123456.78.1234.5.6.7890.1.12345-67` | `123456.**.****.*.*.****.*.*****-67` |
| `[PassaporteData]` | Preserva prefixo letras e 2 ultimos digitos | `AB123456` | `AB****56` |
| &nbsp; | &nbsp; | `123456789` | `*******89` |
| `[RNEData]` | Preserva letra prefixo e digito verificador | `V1234567-8` | `V*******-8` |

### Financeiro

| Atributo | O que faz? | Exemplo Original | Exemplo Redigido |
| :--- | :--- | :--- | :--- |
| `[CartaoCreditoData]` | Preserva 4 primeiros e 4 ultimos digitos | `4532 1178 9012 3456` | `4532 **** **** 3456` |
| `[ContaBancariaData]` | Preserva operacao e digito, mascara conta | `013.123456-7` | `013.******-7` |
| &nbsp; | &nbsp; | `12345-6` | `*****-6` |
| `[PixData]` | Mascara chave aleatoria mantendo 4 primeiros e 8 ultimos | `e8d26618-2e11-4b22-8d26-66182e114b22` | `e8d2****-****-****-****-****2e114b22` |

### Redes e Localização

| Atributo | O que faz? | Exemplo Original | Exemplo Redigido |
| :--- | :--- | :--- | :--- |
| `[EnderecoIPData]` | Mascara os 2 ultimos octetos (IPv4) e os ultimos 3 grupos (IPv6) | `192.168.1.100` | `192.168.*.***` |
| &nbsp; | &nbsp; | `2001:0db8:85a3:0000:0000:8a2e:0370:7334` | `2001:0db8:85a3:0000:0000:****:****:****` |
| `[MacAddressData]` | Preserva prefixo OUI (3 primeiros bytes) | `00:1A:2B:3C:4D:5E` | `00:1A:2B:**:**:**` |
| `[CEPData]` | Mascara os 3 ultimos digitos | `01310-900` | `01310-***` |
| `[GeolocalizacaoData]` | Mascara parte decimal de latitude e longitude | `-23.5505, -46.6333` | `-23.****, -46.****` |

### Veículo

| Atributo | O que faz? | Exemplo Original | Exemplo Redigido |
| :--- | :--- | :--- | :--- |
| `[PlacaData]` | Mascara numeros (padrao antigo) e caracteres apos prefixo (Mercosul) | `ABC-1234` | `ABC-****` |
| &nbsp; | &nbsp; | `ABC1D23` | `ABC****` |
| `[RenavamData]` | Preserva 3 primeiros e 3 ultimos digitos | `12345678901` | `123*****901` |

### Técnico

| Atributo | O que faz? | Exemplo Original | Exemplo Redigido |
| :--- | :--- | :--- | :--- |
| `[GuidData]` | Mascara GUID mantendo 4 primeiros e 4 ultimos hex digitos | `e8d26618-2e11-4b22-8d26-66182e114b22` | `e8d2****-****-****-****-*******4b22` |

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

| Categoria | Valor | Descricao |
| :--- | :--- | :--- |
| **Identificação Pessoal** | `Nome` | Nome completo |
| | `CPF` | Cadastro de Pessoas Físicas |
| | `CNPJ` | Cadastro Nacional da Pessoa Jurídica |
| | `Email` | Endereco de e-mail |
| | `Telefone` | Telefone fixo ou celular |
| | `Endereco` | Endereco completo |
| | `DataGenerica` | Data no formato brasileiro ou ISO |
| **Documentos Oficiais** | `CNH` | Carteira Nacional de Habilitacao |
| | `TituloEleitor` | Titulo de Eleitor |
| | `PIS` | PIS/PASEP/NIT |
| | `CNS` | Cartao Nacional de Saude (SUS) |
| | `CTPS` | Carteira de Trabalho |
| | `Certidao` | Certidao de nascimento/casamento/obito (CNJ) |
| | `Passaporte` | Passaporte (nacional ou estrangeiro) |
| | `RNE` | Registro Nacional de Estrangeiros / RNM |
| **Financeiro** | `CartaoCredito` | Número do cartão de crédito |
| | `ContaBancaria` | Conta bancaria (operacao + conta + digito) |
| | `Pix` | Chave aleatória Pix |
| **Redes e Localização** | `EnderecoIP` | Endereco IPv4 ou IPv6 |
| | `MacAddress` | Endereco MAC |
| | `CEP` | Código de Enderecamento Postal |
| | `Geolocalizacao` | Coordenadas de latitude e longitude |
| **Veículo** | `Placa` | Placa de veiculo (antiga ou Mercosul) |
| | `Renavam` | Renavam do veiculo |
| **Técnico** | `Guid` | Identificador GUID/UUID |

---

## Projetos Relacionados

| Projeto | Descrição |
| :--- | :--- |
| [LGPD.Redact.Serialization](https://github.com/gabriel-na/LGPD.Redact.Serialization) | Extensão para redação de dados em serialização JSON |

---

## Licenca

Distribuido sob a licenca MIT.
