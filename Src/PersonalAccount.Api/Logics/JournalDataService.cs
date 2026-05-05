using Microsoft.EntityFrameworkCore;
using PersonalAccount.Common.Core;
using PersonalAccount.Data.Models;
using PersonalAccount.Domain.Models;
using PersonalAccount.Domain.Models.Dto;

namespace PersonalAccount.Api.Logics;

/// <summary>
/// Сервис для получения данных с журнала
/// </summary>
public class JournalDataService : IJournalDataService
{
    private readonly PersonalAccountContext _context;
    private readonly ICompanySettingsRepository _settingReposity;


    /// <summary>
    /// Конструктор сервиса для работы с журналом
    /// </summary>
    /// <param name="context"></param>
    /// <param name="settingReposity"></param>
    public JournalDataService(PersonalAccountContext context, ICompanySettingsRepository settingReposity)
    {
        _context = context;
        _settingReposity = settingReposity;
    }

    /// <summary>
    /// Получение необработанных строк из журнала
    /// </summary>
    public async Task<IEnumerable<JournalRowDto>> GetUnprocessedRowsAsync(
        BranchModel branch, int batchSize, CancellationToken token)
    {
        var settings = await _settingReposity.LoadAsync(branch.Owner, token);
        var position = settings?.StartPosition ?? 0;

        var rows = await _context.Journals
            .Where(j => j.BranchId == branch.Id && j.Transnumber > position)
            .OrderBy(j => j.Transnumber)
            .Take(batchSize)
            .ToListAsync(token);

        return rows.Select(MapToDto);
    }

    private static JournalRowDto MapToDto(Journal j) => new()
    {
        Code          = j.Transnumber     ?? 0,
        TypeCode      = j.Transtype       ?? 0,
        ReceiptNumber = j.Receiptn        ?? 0,
        ProductCode   = j.Productid,
        ProductName   = j.ProductName,
        CategoryCode  = j.Categoryid,
        CategoryName  = j.CategoryName,
        EmploeeCode   = j.Emploeeid,
        EmploeeName   = j.EmploeeName,
        Period        = j.Dater           ?? DateTime.MinValue,
        Quantity      = j.Quantity        ?? 0,
        Price         = j.Price           ?? 0,
        Discount      = j.Discountamount  ?? 0,
    };
}
