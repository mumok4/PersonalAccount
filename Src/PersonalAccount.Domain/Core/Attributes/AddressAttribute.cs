using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PersonalAccount.Domain.Core.Attributes;

/// <summary>
/// Атрибут валидации адреса по формату КЛАДР
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class AddressAttribute : ValidationAttribute
{
    /// <summary>
    /// Проверка валидности формата КЛАДР
    /// </summary>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var address = value as string;

        if (string.IsNullOrWhiteSpace(address))
        {
            return new ValidationResult("Адрес обязателен для заполнения");
        }

        int commaCount = address.Count(x => x == ',');

        if (commaCount != 5 && commaCount != 6)
        {
            return new ValidationResult("Адрес должен соответствовать формату КЛАДР (содержать 5 или 6 запятых)");
        }

        return ValidationResult.Success;
    }
}