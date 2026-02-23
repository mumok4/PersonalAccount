namespace PersonalAccount.Domain.Core;

/// <summary>
/// Собственный атрибут для проверки корректности строковых данных на основе регулярного выражения.
/// </summary>
public class TemplateAttribute : Attribute
{
    /// <summary>
    /// Габлон для проверки телефонного номера.
    /// </summary>
    public string Template { get; set; }

    /// <summary>
    /// Создать инстанс класса <see cref="TemplateAttribute"/>
    /// </summary>
    /// <param name="template"> Шаблон для регшулярного выражения. </param>
    public TemplateAttribute(string template)
    {
        Template = template ?? throw new ArgumentNullException(nameof(template));
    }
}
