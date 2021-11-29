// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Kaylumah.ValidatedStronglyTypedIOptions.ApiClient.Models;
using Xunit;
using Xunit.Abstractions;

namespace Test.Unit;

[Collection(TestProjectCollection.Name)]
public class TestApiModelValidation : IClassFixture<ApplicationFactory>
{
    private readonly ApplicationFactory _applicationFactoryFixture;
    private readonly ITestOutputHelper _testOutputHelper;

    public TestApiModelValidation(ApplicationFactory applicationFactoryFixture, ITestOutputHelper testOutputHelper)
    {
        _applicationFactoryFixture = applicationFactoryFixture;
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task Test1()
    {
        /*
        {
            "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            "title": "One or more validation errors occurred.",
            "status": 400,
            "traceId": "00-8bbadb59647b27fce4ae88dd2720321d-78d3008d282c7e55-00",
            "errors": {
                "Name": ["The Name field is required."],
                "EmailAddresses": ["The EmailAddresses field is required."]
            }
        }
        */
        var dto = new Profile();
        await ExecuteProfileScenario(dto);
        
    }

    [Fact]
    public async Task Test2()
    {
        /*
        {
            "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            "title": "One or more validation errors occurred.",
            "status": 400,
            "traceId": "00-1f11b692beb8989c6508deea5000f5eb-4d86d42cb487d1f4-00",
            "errors": {
                "Name": ["The Name field is required."]
            }
        }
        */
        var dto = new Profile()
        {
            EmailAddresses = Array.Empty<EmailDetails>()
        };
        await ExecuteProfileScenario(dto);
        
    }

    [Fact]
    public async Task Test3()
    {
        /*
        {
            "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            "title": "One or more validation errors occurred.",
            "status": 400,
            "traceId": "00-50f5816f844377e66f37688f297dfd29-ab771434a82ee290-00",
            "errors": {
                "Name": ["The Name field is required."],
                "EmailAddresses[0].Label": ["The Label field is required."],
                "EmailAddresses[0].Address": ["The Address field is required."]
            }
        }
        */
        var dto = new Profile()
        {
            EmailAddresses = new EmailDetails[] {
                new EmailDetails() {}
            }
        };
        await ExecuteProfileScenario(dto);
        
    }

    [Fact]
    public async Task Test4()
    {
        /*
        {
            "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            "title": "One or more validation errors occurred.",
            "status": 400,
            "traceId": "00-9fa23846f7788678375519385e2e0f97-2662748159deef2f-00",
            "errors": {
                "Name": ["The Name field is required."],
                "EmailAddresses[0].Label": ["The Label field is required."],
                "EmailAddresses[0].Address": ["The Address field is not a valid e-mail address."]
            }
        }
        */
        var dto = new Profile()
        {
            EmailAddresses = new EmailDetails[] {
                new EmailDetails() {
                    Address = "not-an-email"
                }
            }
        };
        await ExecuteProfileScenario(dto);
        
    }

    private async Task ExecuteProfileScenario(Profile profile)
    {
        var client = _applicationFactoryFixture.CreateClient();
        var content = JsonContent.Create(profile);
        var response = await client.PostAsync("/Profile", content).ConfigureAwait(false);
        var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        responseBody.Should().NotBeNull();
        _testOutputHelper.WriteLine(responseBody);
    }
}
