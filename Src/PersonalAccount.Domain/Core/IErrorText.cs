using System;

namespace PersonalAccount.Domain.Core;

/// <summary>
/// Отдельный интерейс для обработки ошибок.
/// </summary>
public interface IErrorText
{
    /// <summary>
    /// Флаг. Наличие ошибки.
    /// </summary>
    public bool IsError {get;}

    /// <summary>
    /// Текст сообщения об ошибке.
    /// </summary>
    public string ErrorText {get; set;}
}
