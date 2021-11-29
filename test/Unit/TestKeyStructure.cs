// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.IO;
using System.Text;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace Test.Unit;

[Collection(TestProjectCollection.Name)]
public class TestKeyStructure
{
    private readonly ITestOutputHelper _testOutputHelper;

    public TestKeyStructure(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test1()
    {
        var jsonText = "{\"MySample\":{\"MyText\":\"Hello World!\",\"MyCollection\":[{\"MyOtherText\":\"Goodbye Cruel World!\"}]}}";
        var configuration = new ConfigurationBuilder()
            .AddJsonStream(GenerateStreamFromString(jsonText))
            .Build();
        var debugView = configuration.GetDebugView();
        /*
MySample:
  MyCollection:
    0:
      MyOtherText=Goodbye Cruel World! (JsonStreamConfigurationProvider)
  MyText=Hello World! (JsonStreamConfigurationProvider)
        */
        _testOutputHelper.WriteLine(debugView);
        configuration["MySample:MyText"].Should().NotBeNullOrEmpty();
        configuration["MySample:MyCollection:0:MyOtherText"].Should().NotBeNullOrEmpty();
    }

    public static MemoryStream GenerateStreamFromString(string value)
    {
        // https://stackoverflow.com/a/5238289/1936600
        return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
    }
}
