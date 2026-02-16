using System.ComponentModel.DataAnnotations;
using PersonalAccount.Domain.Core;

namespace PersonalAccount.Domain.Models;

/// <summary>
/// Модель номенклатуры
/// </summary>
public class Nomenclature : IId
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
}
