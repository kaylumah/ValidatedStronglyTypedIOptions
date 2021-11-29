// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.ComponentModel.DataAnnotations;

namespace Kaylumah.ValidatedStronglyTypedIOptions.Utilities.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class ValidateObjectAttribute : ValidationAttribute
{
    protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null && validationContext != null)
        {
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var context = new ValidationContext(value, null, null);

            System.ComponentModel.DataAnnotations.Validator.TryValidateObject(value, context, results, true);

            if (results.Count != 0)
            {
                var compositeValidationResult = new CompositeValidationResult($"Validation for {validationContext.DisplayName} failed.", new[] { validationContext.MemberName });
                results.ForEach(compositeValidationResult.AddResult);

                return compositeValidationResult;
            }
        }

        return System.ComponentModel.DataAnnotations.ValidationResult.Success;
    }
}
