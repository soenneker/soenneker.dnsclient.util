using System.Threading.Tasks;
using DnsClient;
using AwesomeAssertions;
using Soenneker.DnsClient.Util.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;


namespace Soenneker.DnsClient.Util.Tests;

[Collection("Collection")]
public class DnsClientUtilTests : FixturedUnitTest
{
    private readonly IDnsClientUtil _util;

    public DnsClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IDnsClientUtil>(true);
    }

    [Fact]
    public async Task GetAddress_should_get_address()
    {
        LookupClient client = await _util.Get();

        IDnsQueryResponse? result = await client.QueryAsync("google.com", QueryType.A);
        result.Should().NotBeNull();
    }
}
