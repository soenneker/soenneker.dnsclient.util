[![](https://img.shields.io/nuget/v/soenneker.dnsclient.util.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.dnsclient.util/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.dnsclient.util/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.dnsclient.util/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.dnsclient.util.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.dnsclient.util/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.dnsclient.util/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.dnsclient.util/actions/workflows/codeql.yml)

# Soenneker.DnsClient.Util

Provides a lazily initialized, cached DnsClient.NET `LookupClient` through dependency injection.

## Installation

```bash
dotnet add package Soenneker.DnsClient.Util
```

## Registration

```csharp
using Soenneker.DnsClient.Util.Registrars;

services.AddDnsClientUtilAsSingleton();
```

Use `AddDnsClientUtilAsScoped()` when each dependency-injection scope should own a separate lookup client and cache.

## Query a record

```csharp
using DnsClient;
using DnsClient.Protocol;
using Soenneker.DnsClient.Util.Abstract;

public sealed class AddressResolver(IDnsClientUtil dnsClientUtil)
{
    public async Task<IReadOnlyList<string>> Resolve(
        string host,
        CancellationToken cancellationToken)
    {
        LookupClient client = await dnsClientUtil.Get(cancellationToken: cancellationToken);
        IDnsQueryResponse response = await client.QueryAsync(
            host,
            QueryType.A,
            cancellationToken: cancellationToken);

        return response.Answers.ARecords()
                       .Select(record => record.Address.ToString())
                       .ToArray();
    }
}
```

`Get` returns the same `LookupClient` for the utility’s lifetime. The first successful call controls its options; options supplied to later calls are ignored.

Configure the client on the first call when system DNS defaults are not appropriate:

```csharp
var options = new LookupClientOptions
{
    UseCache = true,
    Timeout = TimeSpan.FromSeconds(3),
    Retries = 2
};

LookupClient client = await dnsClientUtil.Get(options, cancellationToken);
```

For singleton registration, initialize it once during startup if multiple callers might otherwise race to provide different settings. DnsClient.NET handles DNS response codes and transport failures; inspect the returned response and allow cancellation or network exceptions to propagate when the caller should decide recovery behavior.

The utility owns the cached client. Do not dispose the returned `LookupClient`; dispose the utility or its dependency-injection scope instead.
