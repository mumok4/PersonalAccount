using System;
using System.ComponentModel.DataAnnotations;
using PersonalAccount.Domain.Core;

namespace PersonalAccount.Domain.Models;

/// <summary>
/// Модель юридической организации.
/// </summary>
public class Company : DomainModel
{
    /// <summary>
    /// Наименование организации
    /// </summary>
    [Required]
    [StringLength(255)]
    public string Name {get;set;} = string.Empty;

    /// <summary>
    /// ИНН
    /// </summary>
    [Template(@"^[0-9]{10}$")]
    [Required]
    public string INN {get;set;} = string.Empty;

    /// <summary>
    /// Почтовый адрес (формат КЛАДР)
    /// </summary>
    [Template(@"^(?=.*,.*,.*,.*,.*,.*)(?=.*,.*,.*,.*,.*,.*)?[\s\S]*$")]
    [Required]
    public string Address {get; set;} = string.Empty;
}
