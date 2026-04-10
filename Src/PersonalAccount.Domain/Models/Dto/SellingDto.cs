using System;
using PersonalAccount.Domain.Core;

namespace PersonalAccount.Domain.Models.Dto;

// | Код группы | Название группы | Код номенклатуры | Название номенклатуры | Количество | Сумма | Сумма скидки | Код организации |

/// <summary>
/// Запись в отчете "Продажа"
/// </summary>
public class SellingDto : IDto
{
    /// <summary>
    /// Дата
    /// </summary>
    public DateTime Period {get;set;}

    /// <summary>
    /// Код группы номенклатуры.
    /// </summary>
    public Guid CategoryId { get;set; }

    /// <summary>
    /// Наименование группы
    /// </summary>
    public string CategoryName { get; set; } = null!;

    /// <summary>
    /// Код номерклатуры.
    /// </summary>
    public Guid NomenclatureId { get; set; }

    /// <summary>
    /// Наименование номенклатуры.
    /// </summary>
    public string NomenclatureName { get; set; } = null!;

    /// <summary>
    /// Количество.
    /// </summary>
    public double Quantuty { get; set; }

    /// <summary>
    /// Сумма.
    /// </summary>
    public double Amount { get; set; }

    /// <summary>
    /// Сумма скидки.
    /// </summary>
    public double DiscountAmount { get; set; }

    /// <summary>
    /// Код организации.
    /// </summary>
    public Guid CompanyId { get; set; }
}
