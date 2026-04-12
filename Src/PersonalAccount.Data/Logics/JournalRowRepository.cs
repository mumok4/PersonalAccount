using PersonalAccount.Data.Models;

namespace PersonalAccount.Data.Logics;

/// <summary>
/// Реализация репозитория для работы с плоской таблицей транзакций
/// </summary>
public class JournalRowRepository : IJournalRowRepository
{
    private const int _chunkSize = 1000;

    private readonly PersonalAccountContext _context;

    /// <summary>
    /// Создать экземпляр репозитория
    /// </summary>
    /// <param name="context"> Контекст базы данных </param>
    public JournalRowRepository(PersonalAccountContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _context = context;
    }

    /// <summary>
    /// Сохранить набор строк журнала батчами
    /// </summary>
    /// <param name="rows"> Набор строк для сохранения </param>
    /// <param name="token"> Токен отмены операции </param>
    public async Task SaveRowsAsync(IEnumerable<JournalRow> rows, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(rows);

        var batch = rows.ToList();
        if (batch.Count == 0) return;

        for (int i = 0; i < batch.Count; i += _chunkSize)
        {
            var chunk = batch.GetRange(i, Math.Min(_chunkSize, batch.Count - i));
            await _context.JournalRows.AddRangeAsync(chunk, token);
            await _context.SaveChangesAsync(token);

            _context.ChangeTracker.Clear();
        }
    }
}