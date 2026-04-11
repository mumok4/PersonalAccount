using PersonalAccount.Data.Models;

namespace PersonalAccount.Data.Logics;

/// <summary>
/// Реализация репозитория.
/// </summary>
public class JournalRowRepository : IJournalRowRepository
{
    private readonly PersonalAccountContext _context;

    public JournalRowRepository(PersonalAccountContext context)
    {
        _context = context;
    }

    public async Task SaveRowsAsync(IEnumerable<JournalRow> rows, CancellationToken token)
    {
        foreach (var row in rows)
        {
            await _context.JournalRows.AddAsync(row, token);
        }
        
        await _context.SaveChangesAsync(token);
    }
}