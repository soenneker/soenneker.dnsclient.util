using DnsClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.DnsClient.Util.Abstract;

/// <summary>
/// Provides a lazily initialized, cached DnsClient.NET lookup client.
/// </summary>
public interface IDnsClientUtil: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Returns the cached lookup client, creating it with the supplied options when first requested.
    /// </summary>
    /// <param name="options">Options used only if this call initializes the client. The first successful initialization wins.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested lookup Client.</returns>
    ValueTask<LookupClient> Get(LookupClientOptions? options = null, CancellationToken cancellationToken = default);
}
