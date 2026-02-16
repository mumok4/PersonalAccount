using System.ComponentModel.DataAnnotations;

namespace PersonalAccount.Domain.Models;

/// <summary>
/// Модель организации
/// </summary>
public class Organization
{
    /// <summary>
    /// Уникальный код
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование сотрудника
    /// </summary>
    [Required]
    [StringLength(255)]
    public string Name { get; set;} = string.Empty;

    [Required]
    public Employee Employee { get; set; }
}
