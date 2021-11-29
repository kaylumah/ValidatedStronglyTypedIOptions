// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Test.Unit;

[Collection(TestProjectCollection.Name)]
public class TestStronglyTypedOptions
{
    [Fact]
    public void Test1()
    {
        var serviceProvider = new ServiceCollection()
            .Configure<StronglyTypedOptions>(builder => {
                builder.Name = "TestStronglyTypedOptions";
            })
            .AddSingleton(sp => sp.GetRequiredService<IOptions<StronglyTypedOptions>>().Value)
            .BuildServiceProvider();
        var options = serviceProvider.GetRequiredService<IOptions<StronglyTypedOptions>>().Value;
        var typedOptions = serviceProvider.GetRequiredService<StronglyTypedOptions>();
        typedOptions.Name.Should().Be(options.Name);

        var createdOptions = Options.Create(new StronglyTypedOptions {
            Name = "TestStronglyTypedOptions"
        });
        typedOptions.Name.Should().Be(createdOptions.Value.Name);
    }
}
 public class StronglyTypedOptions
 {
     public string? Name { get;set; }
 }
