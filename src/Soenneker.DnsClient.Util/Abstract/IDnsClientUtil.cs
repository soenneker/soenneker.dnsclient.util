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
    /// <summary>
    /// Returns the configured lookup Client used by the dns client.
    /// </summary>
    /// <param name="options">Options to configure for the dns client.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested lookup Client.</returns>
    ValueTask<LookupClient> Get(LookupClientOptions? options = null, CancellationToken cancellationToken = default);
}
