using DnsClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.DnsClient.Util.Abstract;

/// <summary>
/// An async thread-safe singleton for DnsClient.NET
/// </summary>
public interface IDnsClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<LookupClient> Get(LookupClientOptions? options = null, CancellationToken cancellationToken = default);
}
