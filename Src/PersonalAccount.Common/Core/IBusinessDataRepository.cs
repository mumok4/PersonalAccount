using System;
using PersonalAccount.Domain.Models;
using PersonalAccount.Domain.Models.Dto;

namespace PersonalAccount.Common.Core;

/// <summary>
/// Репозиторий для работы с новыми категориями, номенклатурами и сотрудниками из журнала
/// </summary>
public interface IBusinessDataRepository
{
    /// <summary>
    /// Асинхронно сохраняет новые категории
    /// </summary>
    /// <param name="categories"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task SaveCategoriesAsync(IEnumerable<CategoryModel> categories, CancellationToken token);

    /// <summary>
    /// Асинхронно сохраняет новые номенклатуры
    /// </summary>
    /// <param name="nomenclature"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task SaveNomenclatureAsync(IEnumerable<NomenclatureModel> nomenclature, CancellationToken token);

    /// <summary>
    /// Асинхронно сохраняет новых сотрудников
    /// </summary>
    /// <param name="emploees"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task SaveEmploeeAsync(IEnumerable<EmploeeModel> emploees, CancellationToken token);

    /// <summary>
    /// Асинхронно сохраняет новые транзакции
    /// </summary>
    /// <param name="transactions"></param>
    /// <param name="settings"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task SaveTransactionsAsync(IEnumerable<JournalRowDto> transactions, LoadingSettingsModel settings, CancellationToken token);
}
