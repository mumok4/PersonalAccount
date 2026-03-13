using System.ComponentModel.DataAnnotations;
using PersonalAccount.Domain.Core;

namespace PersonalAccount.Domain.Models;

/// <summary>
/// Модель сотрудника.
/// </summary>
public class EmploeeModel : DomainModel
{
    /// <summary>
    /// Наименование сотрудника.
    /// </summary>
    [Required]
    [StringLength(255)]
    public string Name {get;set;} = string.Empty;

    /// <summary>
    /// Контактный телефон.
    /// </summary>
    [Template(@"^\+7\d{10}$")]
    public string? Phone {get;set;}

    /// <summary>
    /// Организация владелец категории.
    /// </summary>
    [Required]
    public CompanyModel Owner {get;set;} = null!;
 
}
