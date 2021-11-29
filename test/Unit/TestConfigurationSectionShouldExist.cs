// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using FluentAssertions;
using Kaylumah.ValidatedStronglyTypedIOptions.Library;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Test.Unit;

[Collection(TestProjectCollection.Name)]
public class TestConfigurationSectionShouldExist
{
    [Fact]
    public void Test1()
    {
        var configuration = new ConfigurationBuilder().Build();
        var action = () =>  new ServiceCollection()
            .AddExampleLibrary(configuration);
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Test2()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>() {
                [string.Join(":", LibraryExampleServiceOptions.DefaultConfigurationSectionName, nameof(LibraryExampleServiceOptions.BaseUrl))] = "Test2"
            })
            .Build();
        var action = () =>  new ServiceCollection()
            .AddExampleLibrary(configuration);
        action.Should().NotThrow();
    }
}
