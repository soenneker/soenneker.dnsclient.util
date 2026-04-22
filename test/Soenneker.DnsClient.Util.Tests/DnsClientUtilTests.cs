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
}
