using PersonalAccount.Common.Core;
using PersonalAccount.Data.Logics;
using PersonalAccount.Domain.Models;
using PersonalAccount.Domain.Models.Dto;

namespace PersonalAccount.Api.Logics;

public class LoadingService : ILoadingService
{
    private readonly ICompanySettingsRepository _settingReposity;
    private readonly IJournalRowRepository _journalRepository;

    public LoadingService(ICompanySettingsRepository settingsRepository, IJournalRowRepository journalRepository)
    {
        _settingReposity = settingsRepository;
        _journalRepository = journalRepository;
    }

    public bool Push(CompanyModel company, IEnumerable<JournalRowDto> transactions, CancellationToken token)
    {
        return PushAsync(company, transactions, token).Result; 
    }

    public async Task<bool> PushAsync(CompanyModel company, IEnumerable<JournalRowDto> transactions, CancellationToken token)
    {
        var settings = await _settingReposity.LoadAsync(company, token);
        if (settings == null) 
        {
            settings = new LoadingSettingsModel { Owner = company, StartPosition = 1, BatchSize = 1000 };
        }

        var innerTransactions = transactions.Where(x => x.Code >= settings.StartPosition).ToList();

        if (innerTransactions.Count() == 0) return true; 

        var entities = innerTransactions.Select(x => new Data.Models.JournalRow
        {
            Code = x.Code,
            TypeCode = x.TypeCode,
            ReceiptNumber = x.ReceiptNumber,
            Period = DateTime.SpecifyKind(x.Period, DateTimeKind.Utc), 
            Quantity = x.Quantity,
            Price = x.Price,
            Discount = x.Discount,
            EmploeeName = x.EmploeeName,
            CategoryName = x.CategoryName,
            NomenclatureName = x.NomenclatureName
        }).ToList();

        var lastCode = innerTransactions.Max(x => x.Code);
        settings.StartPosition = lastCode + 1;

        await _journalRepository.SaveRowsAsync(entities, token);
        await _settingReposity.SaveAsync(settings, token);

        return true;
    }
}
