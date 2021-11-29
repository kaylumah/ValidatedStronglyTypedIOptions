// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureWithValidation<TOptions>(this IServiceCollection services, IConfiguration config) where TOptions : class
        => services.ConfigureWithValidation<TOptions>(Options.Options.DefaultName, config);
    
    public static IServiceCollection ConfigureWithValidation<TOptions>(this IServiceCollection services, string name, IConfiguration config) where TOptions : class
    {
        _ = config ?? throw new ArgumentNullException(nameof(config));
        services.Configure<TOptions>(name, config);
        services.AddDataAnnotationValidatedOptions<TOptions>(name);
        return services;
    }

    public static IServiceCollection ConfigureWithValidation<TOptions>(this IServiceCollection services, Action<TOptions> configureOptions) where TOptions : class
        => services.ConfigureWithValidation<TOptions>(Options.Options.DefaultName, configureOptions);

    public static IServiceCollection ConfigureWithValidation<TOptions>(this IServiceCollection services, string name, Action<TOptions> configureOptions) where TOptions : class
    {
        services.Configure(name, configureOptions);
        services.AddDataAnnotationValidatedOptions<TOptions>(name);
        return services;
    }

    private static IServiceCollection AddDataAnnotationValidatedOptions<TOptions>(this IServiceCollection services, string name) where TOptions : class
    {
        services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<TOptions>>(new DataAnnotationValidateOptions<TOptions>(name)));
        return services;
    }
}
