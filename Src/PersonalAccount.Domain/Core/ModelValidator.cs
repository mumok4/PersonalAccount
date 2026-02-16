using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonalAccount.Domain.Core;

/// <summary>
/// Класс для валидации моделей
/// </summary>
public static class ModelValidator
{
    /// <summary>
    /// Проверяет соответствие атрибутам у модели
    /// </summary>
    public static List<ValidationResult> Validate(object model)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(model);
        Validator.TryValidateObject(model, context, results, true);
        return results;
    }
}