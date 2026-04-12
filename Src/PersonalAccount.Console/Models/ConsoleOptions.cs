namespace PersonalAccount.Console.Models;

/// <summary>
/// Настройки консольного приложения.
/// </summary>
public class ConsoleOptions
{
    /// <summary>
    /// Строка подключения к MS SQL
    /// </summary>
    public string MsSqlConnection { get; set; } = string.Empty;

    /// <summary>
    /// URL адрес API для отправки данных
    /// </summary>
    public string ApiUrl { get; set; } = string.Empty;

    /// <summary>
    /// Уникальный код организации
    /// </summary>
    public Guid CompanyId { get; set; }
}