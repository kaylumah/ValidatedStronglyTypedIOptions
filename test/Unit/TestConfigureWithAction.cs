// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;
using Xunit.Abstractions;

namespace Test.Unit;

[Collection(TestProjectCollection.Name)]
public class TestConfigureWithAction
{
    private readonly ITestOutputHelper _testOutputHelper;

    public TestConfigureWithAction(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test1()
    {
        var services = new ServiceCollection()
            .AddExample(exampleOptions => {
                exampleOptions.Name = "Test1";
            });

        foreach(var service in services)
        {
            _testOutputHelper.WriteLine($"ServiceType = '{service.ServiceType}' ImplementationType = '{service.ImplementationType}'");
        }

        var options = services.BuildServiceProvider().GetRequiredService<IOptions<ExampleOptions>>();
        _testOutputHelper.WriteLine($"options value = {options.Value.Name}");
    }

    [Fact]
    public void Test2()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>() {
                [string.Join(":", ExampleOptions.DefaultConfigurationSectionName, nameof(ExampleOptions.Name))] = "Test2"
            })
            .Build();
        var services = new ServiceCollection()
            .AddExample(configuration);

        foreach(var service in services)
        {
            _testOutputHelper.WriteLine($"ServiceType = '{service.ServiceType}' ImplementationType = '{service.ImplementationType}'");
        }

        var options = services.BuildServiceProvider().GetRequiredService<IOptions<ExampleOptions>>();
        _testOutputHelper.WriteLine($"options value = {options.Value.Name}");
    }
}

internal class ExampleOptions
{
    public const string DefaultConfigurationSectionName = nameof(ExampleOptions);
    public string? Name { get;set; }
}

internal static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddExample(this IServiceCollection services, IConfiguration config)
    {
        services.AddExample(options => config.GetSection(ExampleOptions.DefaultConfigurationSectionName).Bind(options));
        return services;
    }

    public static IServiceCollection AddExample(this IServiceCollection services, Action<ExampleOptions> configureDelegate)
    {
        services.Configure(configureDelegate);
        return services;
    }
}
