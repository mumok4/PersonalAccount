using System;
using PersonalAccount.Domain.Models;
using PersonalAccount.Domain.Models.Dto;

namespace PersonalAccount.Common.Core;

/// <summary>
/// Интерфейс для сервиса получения новых объектов в бд
/// </summary>
public interface INewEntitiesService
{
    /// <summary>
    /// Асинхронно получает новые категории в пачке транзакций
    /// </summary>
    /// <param name="transactions"> транзакции</param>
    /// <param name="company">компания</param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<IEnumerable<CategoryModel>> GetNewCategoriesAsync(IEnumerable<JournalRowDto> transactions, CompanyModel company, CancellationToken token);

    /// <summary>
    /// Асинхронно получает новые номенклатуры в пачке транзакций
    /// </summary>
    /// <param name="transactions">транзакции</param>
    /// <param name="company">компания</param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<IEnumerable<NomenclatureModel>> GetNewNomenclatureAsync(IEnumerable<JournalRowDto> transactions, CompanyModel company, CancellationToken token);

    /// <summary>
    /// Асинхронно получает новых сотрудников в пачке транзакций
    /// </summary>
    /// <param name="transactions">транзакции</param>
    /// <param name="company">компания</param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<IEnumerable<EmploeeModel>> GetNewEmploeeAsync(IEnumerable<JournalRowDto> transactions, CompanyModel company, CancellationToken token);
}
