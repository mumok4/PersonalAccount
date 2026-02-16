using System.ComponentModel.DataAnnotations;
using PersonalAccount.Domain.Core;

namespace PersonalAccount.Domain.Models;

/// <summary>
/// Модель сотрудника
/// </summary>
public class Employee : IId
{
    /// <summary>
    /// Идентификатор сотрудника
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// ФИО сотрудника
    /// </summary>
    [Required]
    [StringLength(255)]
    public required string Name { get; set; }

    /// <summary>
    /// Контактный телефон
    /// </summary>
    [PhoneTemplate(@"^(\+7|8)[\s\-]?\(?\d{3}\)?[\s\-]?\d{3}[\s\-]?\d{2}[\s\-]?\d{2}$")]
    public string? Phone { get; set; }

    /// <summary>
    /// Организация
    /// </summary>
    [Required]
    public required Organization Organization { get; set; }
}