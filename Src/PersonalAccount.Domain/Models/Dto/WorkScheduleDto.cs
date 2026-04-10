using System;
using PersonalAccount.Domain.Core;

namespace PersonalAccount.Domain.Models.Dto;

// | Код сотрудника | ФИО | Дата время начала | Дата время окончания | Код организации |

/// <summary>
/// Запись в отчете "График работы"
/// </summary>
public class WorkScheduleDto : IDto
{
    /// <summary>
    /// Уникальный код сотрудника.
    /// </summary>
    public Guid EmploeeId { get; set; }

    /// <summary>
    /// Наименование сотрудника.
    /// </summary>
    public string EmploeeName { get; set; } = null!;

    /// <summary>
    /// Начало смены.
    /// </summary>
    public DateTimeOffset Start { get; set; } 

    /// <summary>
    /// Окончание смены.
    /// </summary>
    public DateTimeOffset Stop { get; set; }

    /// <summary>
    /// Код организации
    /// </summary>
    public Guid CompanyId { get; set; }
}
