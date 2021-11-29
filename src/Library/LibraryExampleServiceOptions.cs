// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.ComponentModel.DataAnnotations;

namespace Kaylumah.ValidatedStronglyTypedIOptions.Library;

public class LibraryExampleServiceOptions
{
    public const string DefaultConfigurationSectionName = nameof(LibraryExampleServiceOptions);

    [Required, Url]
    public string? BaseUrl { get;set; }
}
