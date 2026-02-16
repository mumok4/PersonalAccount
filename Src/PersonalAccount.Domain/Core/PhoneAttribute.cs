using System;

namespace PersonalAccount.Domain.Core;

/// <summary>
/// Атрибут для фиксации шаблона телефона.
/// </summary>
public class PhoneTemplateAttribute : Attribute
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
}
