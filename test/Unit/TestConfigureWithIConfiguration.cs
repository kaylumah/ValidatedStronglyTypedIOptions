// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;
using Xunit.Abstractions;

namespace Test.Unit;

[Collection(TestProjectCollection.Name)]
public class TestConfigureWithIConfiguration
{
    private readonly ITestOutputHelper _testOutputHelper;

    public TestConfigureWithIConfiguration(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test1()
    {
        var configuration = new ConfigurationBuilder().Build();
        var services = new ServiceCollection()
            .AddDemo(configuration);

        foreach(var service in services)
        {
            _testOutputHelper.WriteLine($"ServiceType = '{service.ServiceType}' ImplementationType = '{service.ImplementationType}'");
        }

        var options = services.BuildServiceProvider().GetRequiredService<IOptions<DemoOptions>>();
        _testOutputHelper.WriteLine($"options value = {options.Value.Name}");
    }
}

internal class DemoOptions
{
    public const string DefaultConfigurationSectionName = nameof(DemoOptions);
    public string? Name { get;set; }
}

internal static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddDemo(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DemoOptions>(configuration.GetSection(DemoOptions.DefaultConfigurationSectionName));
        return services;
    }
}
