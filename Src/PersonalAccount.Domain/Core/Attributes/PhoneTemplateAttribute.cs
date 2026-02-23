using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PersonalAccount.Domain.Core;

/// <summary>
/// Атрибут для фиксации шаблона телефона.
/// </summary>
public class PhoneTemplateAttribute : ValidationAttribute
{
    /// <summary>
    /// Габлон для проверки телефонного номера.
    /// </summary>
    public string Template { get; set; }

    /// <summary>
    /// Создать инстанс класса <see cref="PhoneAttribute"/>
    /// </summary>
    /// <param name="template"> Шаблон для регшулярного выражения. </param>
    public PhoneTemplateAttribute(string template)
    {
        Template = template ?? throw new ArgumentNullException(nameof(template));
    }

    /// <summary>
    /// Проверка валидности телефонного номера
    /// </summary>
    /// <param name="value"></param>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string phone)
        {
            if (!Regex.IsMatch(phone, Template))
            {
                return new ValidationResult("Телефон не соответствует шаблону.");
            }
        }
        return ValidationResult.Success;
    }
}
