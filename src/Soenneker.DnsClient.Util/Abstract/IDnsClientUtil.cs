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
    /// Gets the value.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<LookupClient> Get(LookupClientOptions? options = null, CancellationToken cancellationToken = default);
}
