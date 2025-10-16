using System;
using System.ComponentModel.DataAnnotations;

namespace PersonalAccount.Domain.Models;

/// <summary>
/// Запись в журнале
/// </summary>
public class JournalDto
{
    /// <summary>
    /// Идентификатор записи
    /// </summary>
    [Required]
    public long Id { get; set; }

    /// <summary>
    /// Идентификатор чека
    /// </summary>
    [Required]
    [StringLength(20)]
    public required string CheckNumber { get; set; }

    /// <summary>
    /// Идентификатор сотрудника
    /// </summary>
    public long? EmployeeCode { get; set; }

    /// <summary>
    /// Идентификатор номенклатуры
    /// </summary>
    public long? NomenclatureCode { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    [StringLength(255)]
    public string? Description { get; set; }

    /// <summary>
    /// Идентификатор категории
    /// </summary>
    public int? CategoryCode { get; set; }

    /// <summary>
    /// ИДентификатор операции
    /// </summary>
    [Required]
    public long TransactionCode { get; set; }

    /// <summary>
    /// Дата транзакции
    /// </summary>
    [Required]
    public DateTimeOffset TransactionDate { get; set; }

    /// <summary>
    /// Количество
    /// </summary>
    [Required]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Сумма
    /// </summary>
    [Required]
    public decimal Amount { get; set; }

    /// <summary>
    /// Сумма скидки
    /// </summary>
    [Range(0, double.MaxValue)]
    public decimal DiscountAmount { get; set; }
}