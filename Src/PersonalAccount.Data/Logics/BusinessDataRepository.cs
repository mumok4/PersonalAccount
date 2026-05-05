using Microsoft.EntityFrameworkCore;
using PersonalAccount.Common.Core;
using PersonalAccount.Data.Models;
using PersonalAccount.Domain.Models;

namespace PersonalAccount.Data.Logics;

/// <summary>
/// Репозиторий для работы с бизнес-данными
/// </summary>
public class BusinessDataRepository(PersonalAccountContext context) : IBusinessDataRepository
{
    private readonly PersonalAccountContext _context = context;

    /// <summary>
    /// Массовое сохранение новых категорий номенклатуры.
    /// </summary>
    public async Task SaveCategoriesAsync(IEnumerable<CategoryModel> categories, CancellationToken token)
    {
        var entities = categories.Select(m => new Category
        {
            Id        = Guid.NewGuid(),
            Code      = m.Code,
            Name      = m.Name,
            CompanyId = m.Owner?.Id,
        });

        await _context.Categories.AddRangeAsync(entities, token);
        await _context.SaveChangesAsync(token);
    }

    /// <summary>
    /// Массовое сохранение новых позиций номенклатуры.
    /// </summary>
    public async Task SaveNomenclatureAsync(IEnumerable<NomenclatureModel> nomenclature, CancellationToken token)
    {
        var categoryCodes = nomenclature
            .Select(m => m.Category.Code)
            .Distinct()
            .ToList();

        var categoryIds = await _context.Categories
            .Where(c => c.Code != null && categoryCodes.Contains(c.Code.Value))
            .ToDictionaryAsync(c => c.Code!.Value, c => c.Id, token);

        var entities = nomenclature.Select(m => new Nomenclature
        {
            Id         = Guid.NewGuid(),
            Code       = m.Code,
            Name       = m.Name,
            CategoryId = categoryIds.GetValueOrDefault(m.Category.Code),
        });

        await _context.Nomenclatures.AddRangeAsync(entities, token);
        await _context.SaveChangesAsync(token);
    }

    /// <summary>
    /// Массовое сохранение новых сотрудников.
    /// </summary>
    public async Task SaveEmployeesAsync(IEnumerable<EmploeeModel> employees, CancellationToken token)
    {
        var entities = employees.Select(m => new Emploee
        {
            Id        = Guid.NewGuid(),
            Code      = m.Code,
            Name      = m.Name,
            Phone     = m.Phone,
            CompanyId = m.Owner?.Id,
        });

        await _context.Emploees.AddRangeAsync(entities, token);
        await _context.SaveChangesAsync(token);
    }

    /// <summary>
    /// Массовое сохранение транзакций в основную бизнес-таблицу.
    /// </summary>
    public async Task SaveTransactionsAsync(IEnumerable<TransactionModel> transactions, CancellationToken token)
    {
        var nomenclatureCodes = transactions.Select(t => t.Nomenclature.Code).Distinct().ToList();
        var emploeeCodes      = transactions.Select(t => t.Emploee.Code).Distinct().ToList();

        var nomenclatureIds = await _context.Nomenclatures
            .Where(n => n.Code != null && nomenclatureCodes.Contains(n.Code.Value))
            .ToDictionaryAsync(n => n.Code!.Value, n => n.Id, token);

        var emploeeIds = await _context.Emploees
            .Where(e => e.Code != null && emploeeCodes.Contains(e.Code.Value))
            .ToDictionaryAsync(e => e.Code!.Value, e => e.Id, token);

        var entities = transactions.Select(m => new Transaction
        {
            Id              = Guid.NewGuid(),
            TransactionType = (int)m.Type,
            ChangePeriod    = m.Period.DateTime,
            Price           = (decimal)m.Price,
            Quantity        = (decimal)m.Quantuty,
            Discount        = (decimal)m.Discount,
            BranchId        = m.Branch.Id,
            NomenclatureId  = nomenclatureIds.GetValueOrDefault(m.Nomenclature.Code),
            EmloeeId        = emploeeIds.GetValueOrDefault(m.Emploee.Code),
        });

        await _context.Transactions.AddRangeAsync(entities, token);
        await _context.SaveChangesAsync(token);
    }
}
