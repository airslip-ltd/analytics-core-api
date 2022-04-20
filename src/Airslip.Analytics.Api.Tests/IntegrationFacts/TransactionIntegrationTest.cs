using Airslip.Common.Monitoring.Models;
using Airslip.Common.Utilities;
using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Airslip.Analytics.Api.Tests.IntegrationFacts;

public class TransactionIntegrationTest : IntegrationTestBase
{
    // TODO: Create authenticated integration test
    // [Fact]
    // public async Task Can_get_commerce_account_transactions()
    // {
    //     HttpResponseMessage createRequest = await GetAuthorizedHttpClient()
    //         .GetAsync(
    //             new Uri(BaseUri, TransactionEndpoint + "/commerce/accounts"));
    //
    //     createRequest.StatusCode.Should().Be(HttpStatusCode.OK);
    // }
}