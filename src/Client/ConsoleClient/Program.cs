// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Kaylumah.ValidatedStronglyTypedIOptions.Library;

var configuration = new ConfigurationBuilder()
    .AddInMemoryCollection(new Dictionary<string, string>() {
        [string.Join(":", LibraryExampleServiceOptions.DefaultConfigurationSectionName, nameof(LibraryExampleServiceOptions.BaseUrl))] = "non-an-url"
    })
    .Build();
var serviceProvider = new ServiceCollection()
    .AddLogging(builder => builder.AddConsole())
    .AddExampleLibrary(configuration)
    .BuildServiceProvider();

var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Before retrieving IOptions");
var options = serviceProvider.GetRequiredService<IOptions<LibraryExampleServiceOptions>>();
logger.LogInformation("After retrieving IOptions; before IOptions.Value");
var optionsValue = options.Value;
logger.LogInformation("After IOptions.Value");

Console.ReadLine();

partial class Program {}
