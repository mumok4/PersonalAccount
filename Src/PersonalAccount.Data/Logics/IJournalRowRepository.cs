using PersonalAccount.Data.Models;

namespace PersonalAccount.Data.Logics;

/// <summary>
/// Интерфейс репозитория для работы с плоской таблицей транзакций.
/// Находится здесь, так как использует сущность JournalRow из этого же проекта.
/// </summary>
public interface IJournalRowRepository
{
    Task SaveRowsAsync(IEnumerable<JournalRow> rows, CancellationToken token);
}