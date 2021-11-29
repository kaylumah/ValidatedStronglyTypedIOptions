// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Kaylumah.ValidatedStronglyTypedIOptions.Utilities.Validation;

public class ValidationResult
{
    public string? ErrorMessage { get; set; }

    public string[] MemberNames { get; set; } = Array.Empty<string>();

    public ValidationResult[] ValidationResults { get; set; } = Array.Empty<ValidationResult>();

    public override string ToString()
    {
        var memberNames = string.Join(";", MemberNames);
        return $"{memberNames} => {ErrorMessage}";
    }
}
