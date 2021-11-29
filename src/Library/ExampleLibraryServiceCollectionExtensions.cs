// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Kaylumah.ValidatedStronglyTypedIOptions.Library;

public static partial class ExampleLibraryServiceCollectionExtensions
{
    public static IServiceCollection AddExampleLibrary(this IServiceCollection services, IConfiguration config)
    {
        var configurationSection = config.GetExistingSectionOrThrow(LibraryExampleServiceOptions.DefaultConfigurationSectionName);
        services.ConfigureWithValidation<LibraryExampleServiceOptions>(configurationSection);
        services.ConfigureOptions<ConfigureLibraryExampleServiceOptions>();

        services.AddHttpClient<ILibraryExampleService, LibraryExampleService>((serviceProvider, httpClient) => {
            var options = serviceProvider.GetRequiredService<IOptions<LibraryExampleServiceOptions>>().Value;
            httpClient.BaseAddress = new Uri(options.BaseUrl);
        });
        return services;
    }
}
