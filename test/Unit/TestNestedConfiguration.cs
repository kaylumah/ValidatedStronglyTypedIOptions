// Copyright (c) Kaylumah, 2021. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using FluentAssertions;
using Kaylumah.ValidatedStronglyTypedIOptions.Utilities.Validation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Test.Unit;

[Collection(TestProjectCollection.Name)]
public class TestNestedConfiguration
{
    [Fact]
    public void Test1()
    {
        var serviceProvider = new ServiceCollection()
            .ConfigureWithValidation<NestedParent>(options =>
            {
                options.Name = "1";
                options.Children = new NestedChild[]
                {
                    new NestedChild()
                };
            })
            .BuildServiceProvider();
        var action = () => {
            var options = serviceProvider.GetRequiredService<IOptions<NestedParent>>().Value;
        };
        action.Should().Throw<OptionsValidationException>();
    }

    [Fact]
    public void Test2()
    {
        var serviceProvider = new ServiceCollection()
            .Configure<NestedParent>(options =>
            {
                options.Name = "1";
                options.Children = new NestedChild[]
                {
                    new NestedChild()
                };
            })
            .AddTransient<IValidateOptions<NestedParent>, CustomValidate>()
            .BuildServiceProvider();
         var action = () => {
             var options = serviceProvider.GetRequiredService<IOptions<NestedParent>>().Value;
         };
        action.Should().Throw<OptionsValidationException>();
    }
}

internal class NestedParent
{
    [Required]
    public string? Name { get; set; }

    [Required, MinLength(1), ValidateCollection]
    public NestedChild[] Children { get; set; } = Array.Empty<NestedChild>();
}

internal class NestedChild
{
    [Required]
    public string? Name { get; set; }
}

internal class CustomValidate : IValidateOptions<NestedParent>
{
    public ValidateOptionsResult Validate(string name, NestedParent options)
    {
        var validationResults = Kaylumah.ValidatedStronglyTypedIOptions.Utilities.Validation.Validator.ValidateReturnValue(options);
        if (validationResults.Any())
        {
            var builder = new StringBuilder();
            foreach (var result in validationResults)
            {
                var pretty = PrettyPrint(result, string.Empty, true);
                builder.Append(pretty);
            }
            return ValidateOptionsResult.Fail(builder.ToString());
        }

        return ValidateOptionsResult.Success;
    }

    private string PrettyPrint(Kaylumah.ValidatedStronglyTypedIOptions.Utilities.Validation.ValidationResult root, string indent, bool last)
    {
        // Based on https://stackoverflow.com/a/1649223
        var sb = new StringBuilder();
        sb.Append(indent);
        if (last)
        {
            sb.Append("|-");
            indent += "  ";
        }
        else
        {
            sb.Append("|-");
            indent += "| ";
        }

        sb.AppendLine(root.ToString());

        if (root.ValidationResults != null)
        {
            for (var i = 0; i < root.ValidationResults.Length; i++)
            {
                var child = root.ValidationResults[i];
                var pretty = PrettyPrint(child, indent, i == root.ValidationResults.Length - 1);
                sb.Append(pretty);
            }
        }

        return sb.ToString();
    }
}
