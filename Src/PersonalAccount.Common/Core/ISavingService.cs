using System;
using PersonalAccount.Domain.Models.Dto;

namespace PersonalAccount.Common.Core;

/// <summary>
/// Интерфейс для записи транзакций с клиента в плоскую таблицу
/// </summary>
public interface ISavingService
{
    /// <summary>
    /// Сохранить синхронно
    /// </summary>
    /// <param name="trasactions"></param>
    /// <returns></returns>
    public bool Save( IEnumerable<JournalRowDto> trasactions );

    /// <summary>
    /// Сохранить асинхронно
    /// </summary>
    /// <param name="trasactions"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<bool> SaveAsync( IEnumerable<JournalRowDto> trasactions , CancellationToken token);
}
