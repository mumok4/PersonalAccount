using System.ComponentModel.DataAnnotations;
using PersonalAccount.Domain.Core;

namespace PersonalAccount.Domain.Models;

/// <summary>
/// Модель настроек загрузки данных
/// </summary>
public class LoadSettings : IId
{
    /// <summary>
    /// Идентификатор настроек
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Размер пакета загрузки
    /// </summary>
    [Range(1, 10000)]
    public int BatchSize { get; set; } = 100;
}