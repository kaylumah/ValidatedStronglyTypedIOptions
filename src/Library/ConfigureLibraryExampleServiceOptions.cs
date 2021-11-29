// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kaylumah.ValidatedStronglyTypedIOptions.Library;

public class ConfigureLibraryExampleServiceOptions : IConfigureOptions<LibraryExampleServiceOptions>, IPostConfigureOptions<LibraryExampleServiceOptions>, IValidateOptions<LibraryExampleServiceOptions>
{
    private readonly ILogger _logger;

    public ConfigureLibraryExampleServiceOptions(ILogger<ConfigureLibraryExampleServiceOptions> logger)
    {
        _logger = logger;
    }
    
    public void Configure(LibraryExampleServiceOptions options)
    {
        _logger.LogInformation("ConfigureExampleServiceOptions Configure");
    }

    public void PostConfigure(string name, LibraryExampleServiceOptions options)
    {
        _logger.LogInformation("ConfigureExampleServiceOptions PostConfigure");
    }

    public ValidateOptionsResult Validate(string name, LibraryExampleServiceOptions options)
    {
        _logger.LogInformation("ConfigureExampleServiceOptions ValidateOptionsResult");
        return ValidateOptionsResult.Skip;
    }
}
