using System;
using PersonalAccount.Domain.Models;
using PersonalAccount.Domain.Models.Dto;

namespace PersonalAccount.Common.Core;

/// <summary>
/// Интерфейс для сервиса проверки новых транзакций из журнала
/// </summary>
public interface INewTransactionsService
{
    /// <summary>
    /// Асинхронно получает новые транзакции из журнала
    /// </summary>
    /// <param name="transactions">Транзацкции</param>
    /// <param name="settings">Настройки загрузки</param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<IEnumerable<JournalRowDto>> GetNewTransactionsAsync(IEnumerable<JournalRowDto> transactions, LoadingSettingsModel settings, CancellationToken token);
}
