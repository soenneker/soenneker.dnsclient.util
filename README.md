[![](https://img.shields.io/nuget/v/soenneker.dnsclient.util.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.dnsclient.util/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.dnsclient.util/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.dnsclient.util/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.dnsclient.util.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.dnsclient.util/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.dnsclient.util/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.dnsclient.util/actions/workflows/codeql.yml)

# Soenneker.DnsClient.Util

An async thread-safe singleton for DnsClient.NET.

## Install

```bash
dotnet add package Soenneker.DnsClient.Util
```

## Quick start

```csharp
using Soenneker.DnsClient.Util.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddDnsClientUtilAsSingleton();
```

Adds `IDnsClientUtil` as a singleton service.

## What you get

- `IDnsClientUtil` — An async thread-safe singleton for DnsClient.NET.
- `DnsClientUtilRegistrar` — An async thread-safe singleton for DnsClient.NET.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `DnsClientUtilRegistrar.AddDnsClientUtilAsSingleton(services)` | Adds `IDnsClientUtil` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `DnsClientUtilRegistrar.AddDnsClientUtilAsScoped(services)` | Adds `IDnsClientUtil` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Reuse the registered client instead of constructing one per operation.
- Calls that return a cached or singleton value reuse the same instance until the owning service is disposed.
- Dispose instances you own when their scope ends so held resources can be released.
