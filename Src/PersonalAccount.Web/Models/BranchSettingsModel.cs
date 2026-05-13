using PersonalAccount.Domain.Models;

namespace PersonalAccount.Web.Models;

/// <summary>
/// Модель данных для формы настроек.
/// </summary>
public class BranchSettingsModel
{
    /// <summary>
    /// Список филиалов.
    /// </summary>
    public List<BranchModel> Branches { get; set; } = null!;

    #region  Данные формы

    /// <summary>
    /// Уникальный код филиала
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Описание настройки
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Уникальный код транзакции для начала загрузки.
    /// </summary>
    public long StartPosition { get; set; }

    /// <summary>
    /// Размер пачки
    /// </summary>
    public long BatchSize { get; set; } = 1000;

    #endregion
}
