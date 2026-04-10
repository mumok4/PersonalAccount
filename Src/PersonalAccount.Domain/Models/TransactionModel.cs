using System;
using System.ComponentModel.DataAnnotations;
using PersonalAccount.Domain.Core;

namespace PersonalAccount.Domain.Models;

/// <summary>
/// Модель транзакции.
/// </summary>
public class TransactionModel : DomainModel
{
    /// <summary>
    /// Тип транзакции.
    /// </summary>
    [Required]
    public TransactionType Type {get;set;}

    /// <summary>
    /// Уникалоьный номер чека.
    /// </summary>
    public string TicketNumber {get; set;} = string.Empty;

    /// <summary>
    /// Организация владелец категории.
    /// </summary>
    [Required]
    public CompanyModel Owner {get;set;} = null!;

    /// <summary>
    /// Период операции.
    /// </summary>
    [Required]
    public DateTimeOffset Period {get;set;}

    /// <summary>
    /// Номенклатура.
    /// </summary>
    [Required]
    public NomenclatureModel Nomenclature {get;set;} = null!;

    /// <summary>
    /// Сотрудник который выполнил операцию.
    /// </summary>
    [Required]
    public EmploeeModel Emploee {get;set;} = null!;

    /// <summary>
    /// Цена.
    /// </summary>
    public double Price {get;set;}

    /// <summary>
    /// Количество.
    /// </summary>
    public double Quantuty {get;set;}

    /// <summary>
    /// Сумма скидки.
    /// </summary>
    public double Discount {get;set;}
}
