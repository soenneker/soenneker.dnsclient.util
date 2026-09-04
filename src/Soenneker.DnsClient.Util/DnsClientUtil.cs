using DnsClient;
using Soenneker.DnsClient.Util.Abstract;
using Soenneker.Utils.AsyncSingleton;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.DnsClient.Util;

/// <inheritdoc cref="IDnsClientUtil" />
public sealed class DnsClientUtil : IDnsClientUtil
{
    private readonly AsyncSingleton<LookupClient, LookupClientOptions?> _client;

    public DnsClientUtil()
    {
        _client = new AsyncSingleton<LookupClient, LookupClientOptions?>(CreateClient);
    }

    private static LookupClient CreateClient(LookupClientOptions? options)
    {
        if (options == null)
            return new LookupClient();

        return new LookupClient(options);
    }

    public ValueTask<LookupClient> Get(LookupClientOptions? options = null, CancellationToken cancellationToken = default)
    {
        return _client.Get(options, cancellationToken);
    }

    /// <summary>
    /// Releases resources used by the current instance.
    /// </summary>
    public void Dispose()
    {
        _client.Dispose();
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
