using System;
using System.ComponentModel.DataAnnotations;

namespace PersonalAccount.Domain.Models;

/// <summary>
/// Модель номенклатуры.
/// </summary>
public class Nomenclature : DomainModel
{
    /// <summary>
    /// Наименование,
    /// </summary>
    [Required]
    public string Name {get;set;} = string.Empty;

    /// <summary>
    /// Категория.
    /// </summary>
    [Required]
    public Category Category {get;set;} = null!;
}
