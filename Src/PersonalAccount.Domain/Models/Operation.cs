using System.ComponentModel.DataAnnotations;

namespace PersonalAccount.Domain.Models;

/// <summary>
/// Модель операции
/// </summary>
public class Operation
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
