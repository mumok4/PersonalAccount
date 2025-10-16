using System.ComponentModel.DataAnnotations;
using PersonalAccount.Domain.Core;

namespace PersonalAccount.Domain.Models;

/// <summary>
/// Модель номенклатуры
/// </summary>
public class Nomenclature {
    /// <summary>
    /// Идентификатор номенклатуры
    /// </summary>
    [Required]
    public long Id { get; set; }

    /// <summary>
    /// Наименование номенклатуры
    /// </summary>
    [Required]
    [StringLength(255)]
    public required string Name { get; set; }

    /// <summary>
    /// Категория товара
    /// </summary>
    [Required]
    public required Category Category { get; set; }
}