using System.Threading.Tasks;
using DnsClient;
using AwesomeAssertions;
using Soenneker.DnsClient.Util.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.DnsClient.Util.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class DnsClientUtilTests : HostedUnitTest
{
    private readonly IDnsClientUtil _util;

    public DnsClientUtilTests(Host host) : base(host)
    {
        _util = Resolve<IDnsClientUtil>(true);
    }

    [Test]
    public async Task GetAddress_should_get_address()
    {
        LookupClient client = await _util.Get();

        IDnsQueryResponse? result = await client.QueryAsync("google.com", QueryType.A);
        result.Should().NotBeNull();
    }

    [Test]
    public async Task Get_should_reuse_client_initialized_by_first_call()
    {
        await using var util = new DnsClientUtil();
        var options = new LookupClientOptions {UseCache = false};

        LookupClient first = await util.Get(options);
        LookupClient second = await util.Get(new LookupClientOptions {UseCache = true});

        second.Should().BeSameAs(first);
        first.Settings.UseCache.Should().BeFalse();
    }
}
