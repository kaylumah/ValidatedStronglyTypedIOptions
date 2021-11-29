// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Kaylumah.ValidatedStronglyTypedIOptions.Utilities.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class ValidateCollectionAttribute : ValidationAttribute
{
    protected override System.ComponentModel.DataAnnotations.ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        CompositeValidationResult? collectionCompositeValidationResult = null;

        if (value is IEnumerable collection && validationContext != null)
        {
            var index = 0;
            foreach (var obj in collection)
            {
                var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                var context = new ValidationContext(obj, null, null);

                System.ComponentModel.DataAnnotations.Validator.TryValidateObject(obj, context, results, true);

                if (results.Count != 0)
                {
                    var compositeValidationResult = new CompositeValidationResult($"Validation for {validationContext.MemberName}[{index}] failed.", new[] { $"{validationContext.MemberName}[{index}]" });
                    results.ForEach(compositeValidationResult.AddResult);

                    if (collectionCompositeValidationResult == null)
                    {
                        collectionCompositeValidationResult = new CompositeValidationResult($"Validation for {validationContext.MemberName} failed.", new[] { validationContext.MemberName });
                    }

                    collectionCompositeValidationResult.AddResult(compositeValidationResult);
                }

                index++;
            }

            if (collectionCompositeValidationResult != null)
            {
                return collectionCompositeValidationResult;
            }
        }

        return System.ComponentModel.DataAnnotations.ValidationResult.Success;
    }
}
