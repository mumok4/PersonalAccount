using System;
using System.ComponentModel.DataAnnotations;
using PersonalAccount.Domain.Core;

namespace PersonalAccount.Domain.Models;

/// <summary>
/// Тип операции
/// </summary>
public enum OperationType
{
    RECIEVE = 1,
    SALE = 2,
    RETURN = 3,
    WRITEOFF = 4,
}

/// <summary>
/// Модель транзакции
/// </summary>
public class Transaction
{
    /// <summary>
    /// Идентификатор транзакции
    /// </summary>
    [Required]
    public long Id { get; set; }

    /// <summary>
    /// Дата транзакции
    /// </summary>
    [Required]
    public DateTimeOffset Date { get; set; }

    /// <summary>
    /// Организация
    /// </summary>
    [Required]
    public required Organization Organization { get; set; }

    /// <summary>
    /// Сотрудник
    /// </summary>
    public Employee? Employee { get; set; }

    /// <summary>
    /// Номенклатура
    /// </summary>
    [Required]
    public required Nomenclature Nomenclature { get; set; }

    /// <summary>
    /// Количество
    /// </summary>
    [Range(0, double.MaxValue)]
    public decimal Quantity { get; set; }

    /// <summary>
    /// Сумма
    /// </summary>
    [Range(0, double.MaxValue)]
    public decimal Amount { get; set; }

    /// <summary>
    /// Тип операции
    /// </summary>
    [Required]
    public OperationType Type { get; set; }
}