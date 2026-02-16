using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace PersonalAccount.Domain.Models;

/// <summary>
/// Модель настроек загрузки данных
/// </summary>
public class LoadSettings
{
    /// <summary>
    /// Уникальный код
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    public int batch { get; set; }

}
