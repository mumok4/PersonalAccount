using System;
using System.ComponentModel.DataAnnotations;
using PersonalAccount.Domain.Core;

namespace PersonalAccount.Domain.Models;

/// <summary>
/// Модель - сотрудник.
/// </summary>
public class Emploee : IId
{
    /// <summary>
    /// Уникальный код.
    /// </summary>
    public Guid Id {get;set;}

    /// <summary>
    /// Наименование сотрудника.
    /// </summary>
    [Required]
    [StringLength(255)]
    public string Name {get;set;} = string.Empty;

    /// <summary>
    /// Контактный телефон.
    /// </summary>
    [PhoneTemplate("УРА!")]
    public string? Phone {get;set;}
}
