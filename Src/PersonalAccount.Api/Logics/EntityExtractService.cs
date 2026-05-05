using Microsoft.EntityFrameworkCore;
using PersonalAccount.Common.Core;
using PersonalAccount.Data.Models;
using PersonalAccount.Domain.Models;
using PersonalAccount.Domain.Models.Dto;

namespace PersonalAccount.Api.Logics;

/// <summary>
/// Сервис для выделения отдельных сущностей из записей
/// </summary>
public class EntityExtractService(PersonalAccountContext context) : IEntityExtractService
{
    private readonly PersonalAccountContext _context = context;

    /// <summary>
    /// Извлечение новых категорий
    /// </summary>
    public async Task<IEnumerable<CategoryModel>> ExtractNewCategoriesAsync(
        IEnumerable<JournalRowDto> rows, CancellationToken token)
    {
        var incoming = rows
            .Where(r => r.CategoryCode.HasValue && r.CategoryName is not null)
            .GroupBy(r => r.CategoryCode!.Value)
            .Select(g => (Code: g.Key, Name: g.First().CategoryName!))
            .ToList();

        if (incoming.Count == 0) return [];

        var codes = incoming.Select(x => x.Code).ToList();

        var existingCodes = await _context.Categories
            .Where(c => c.Code != null && codes.Contains(c.Code.Value))
            .Select(c => c.Code!.Value)
            .ToListAsync(token);

        var existing = existingCodes.ToHashSet();

        return incoming
            .Where(x => !existing.Contains(x.Code))
            .Select(x => new CategoryModel { Code = x.Code, Name = x.Name });
    }

    /// <summary>
    /// Извлечение новых номенклатур
    /// </summary>
    public async Task<IEnumerable<NomenclatureModel>> ExtractNewNomenclatureAsync(
        IEnumerable<JournalRowDto> rows, CancellationToken token)
    {
        var incoming = rows
            .Where(r => r.ProductCode.HasValue && r.ProductName is not null)
            .GroupBy(r => r.ProductCode!.Value)
            .Select(g => g.First())
            .ToList();

        if (incoming.Count == 0) return [];

        var codes = incoming.Select(r => r.ProductCode!.Value).ToList();

        var existingCodes = await _context.Nomenclatures
            .Where(n => n.Code != null && codes.Contains(n.Code.Value))
            .Select(n => n.Code!.Value)
            .ToListAsync(token);

        var existing = existingCodes.ToHashSet();
        var newRows = incoming.Where(r => !existing.Contains(r.ProductCode!.Value)).ToList();

        if (newRows.Count == 0) return [];

        var categoryCodes = newRows
            .Where(r => r.CategoryCode.HasValue)
            .Select(r => r.CategoryCode!.Value)
            .Distinct()
            .ToList();

        var categories = await _context.Categories
            .Where(c => c.Code != null && categoryCodes.Contains(c.Code.Value))
            .ToDictionaryAsync(
                c => c.Code!.Value,
                c => new CategoryModel { Id = c.Id, Name = c.Name ?? string.Empty, Code = c.Code!.Value },
                token);

        return newRows.Select(r => new NomenclatureModel
        {
            Code = r.ProductCode!.Value,
            Name = r.ProductName!,
            Category = r.CategoryCode.HasValue && categories.TryGetValue(r.CategoryCode.Value, out var cat)
                ? cat
                : new CategoryModel()
        });
    }

    /// <summary>
    /// Извлечение новых сотрудников
    /// </summary>
    public async Task<IEnumerable<EmploeeModel>> ExtractNewEmployeesAsync(
        IEnumerable<JournalRowDto> rows, CancellationToken token)
    {
        var incoming = rows
            .Where(r => r.EmploeeCode.HasValue && r.EmploeeName is not null)
            .GroupBy(r => r.EmploeeCode!.Value)
            .Select(g => (Code: g.Key, Name: g.First().EmploeeName!))
            .ToList();

        if (incoming.Count == 0) return [];

        var codes = incoming.Select(x => x.Code).ToList();

        var existingCodes = await _context.Emploees
            .Where(e => e.Code != null && codes.Contains(e.Code.Value))
            .Select(e => e.Code!.Value)
            .ToListAsync(token);

        var existing = existingCodes.ToHashSet();

        return incoming
            .Where(x => !existing.Contains(x.Code))
            .Select(x => new EmploeeModel { Code = x.Code, Name = x.Name });
    }
}
