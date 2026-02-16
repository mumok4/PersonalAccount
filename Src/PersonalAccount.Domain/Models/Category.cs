using System;
using System.ComponentModel.DataAnnotations;
using PersonalAccount.Domain.Core;

namespace PersonalAccount.Domain.Models;

/// <summary>
/// Модель категории номераклатуры.
/// </summary>
public class Category : IId
{
    /// <summary>
    /// Уникальный код.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование категории.
    /// </summary>
    [Required]
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;
}
