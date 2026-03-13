using System;

namespace PersonalAccount.Console.Models;

/// <summary>
/// Настройки консольного приложения.
/// </summary>
public class ConsoleOptions
{
    /// <summary>
    /// Строка подключения MS SQL
    /// </summary>
    public required string ConnectionString {get;set;} = string.Empty;
}
