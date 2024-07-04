using DnsClient;
using Soenneker.DnsClient.Util.Abstract;
using Soenneker.Utils.AsyncSingleton;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.DnsClient.Util;

/// <inheritdoc cref="IDnsClientUtil"/>
public class DnsClientUtil : IDnsClientUtil
{
    private readonly AsyncSingleton<LookupClient> _client;

    private LookupClientOptions? _options;

    public DnsClientUtil()
    {
        _client = new AsyncSingleton<LookupClient>(() =>
        {
            if (_options == null)
                return new LookupClient();

            return new LookupClient(_options);
        });
    }

    public ValueTask<LookupClient> Get(LookupClientOptions? options = null, CancellationToken cancellationToken = default)
    {
        _options = options;

        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return _client.DisposeAsync();
    }
}