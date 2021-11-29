// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.ComponentModel.DataAnnotations;

namespace Kaylumah.ValidatedStronglyTypedIOptions.ApiClient.Models;

public class Profile
{
    [Required]
    public string? Name { get;set; }

    [Required]
    public EmailDetails[]? EmailAddresses { get;set; }// = Array.Empty<EmailDetails>();
}

public class EmailDetails
{
    [Required]
    public string? Label { get;set;}

    [Required, EmailAddress]
    public string? Address { get;set; }
}
