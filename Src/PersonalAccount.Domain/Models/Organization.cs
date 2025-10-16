using System.ComponentModel.DataAnnotations;
using PersonalAccount.Domain.Core;
using PersonalAccount.Domain.Core.Attributes;

namespace PersonalAccount.Domain.Models;

/// <summary>
/// Модель организации
/// </summary>
public class Organization : IId
{
    /// <summary>
    /// Идентификатор организации
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование организации
    /// </summary>
    [Required]
    [StringLength(255)]
    public required string Name { get; set; }

    /// <summary>
    /// ИНН организации
    /// </summary>
    [Required]
    public required string Inn { get; set; }

    /// <summary>
    /// Адрес в формате КЛАДР
    /// </summary>
    [Required]
    [Address]
    public required string Address { get; set; }
}