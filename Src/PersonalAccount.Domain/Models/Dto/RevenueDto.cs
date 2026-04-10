using System;
using PersonalAccount.Domain.Core;

namespace PersonalAccount.Domain.Models.Dto;

// | Период | Сумма оплаты наличные | Сумма оплаты безналично | Сумма оплаты остальное | Сумма скидки | Флаг "Праздник" | Код организации |

/// <summary>
/// Записть в отчете "Выручка"
/// </summary>
public class RevenueDto : IDto
{
    /// <summary>
    /// Дата
    /// </summary>
    public DateTime Period {get;set;}

    /// <summary>
    /// Сумма оплат наличными
    /// </summary>
    public double CashAmount {get;set;}

    /// <summary>
    /// Сумма оплат безналично
    /// </summary>
    public double BankAmount {get;set;}

    /// <summary>
    /// Итого
    /// </summary>
    public double TotalAmount {get;set;}

    /// <summary>
    /// Сумма скидки
    /// </summary>
    public double DiscountAmount {get;set;}

    /// <summary>
    /// Флаг. Праздник
    /// </summary>
    public bool IsHollyday {get;set;} 

    /// <summary>
    /// Организация владелец
    /// </summary>
    public Guid Owner {get;set;}
}
