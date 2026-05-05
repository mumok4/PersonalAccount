using Microsoft.EntityFrameworkCore;
using PersonalAccount.Common.Core;
using PersonalAccount.Data.Models;
using PersonalAccount.Domain.Models;
using PersonalAccount.Domain.Models.Dto;

namespace PersonalAccount.Api.Logics;

/// <summary>
/// Реализация интерфейса <see cref="ILoadingService"/>
/// </summary>
public class LoadingService : ILoadingService
{
    // Репозиторий для работы с настройками загрузки данных
    private readonly  ICompanySettingsRepository _settingReposity;

    // Репозиторий для скоростной записи данных в журнал
    private readonly IServerRepository<JournalRowDto> _writerRepository;

    // Контекст для работы с базой данных
    private readonly PersonalAccountContext _context;

    /// <summary>
    /// Конструктор сервиса для загрузки
    /// </summary>
    /// <param name="context"></param>
    /// <param name="settingsRepository"></param>
    /// <param name="writerRepository"></param>
    public LoadingService(
            PersonalAccountContext context,
            ICompanySettingsRepository settingsRepository,
            IServerRepository<JournalRowDto> writerRepository)
    {
        _context = context;
        _settingReposity = settingsRepository;
        _writerRepository = writerRepository;
    }


    /// <InhericDoc/>
    private bool Push(CompanyModel company, IEnumerable<JournalRowDto> transactions)
    {
        // 1 Получаем настройки
        var settings = _settingReposity.Load( company)
                        ?? new LoadingSettingsModel()
                        {
                            Owner = company, StartPosition = 1, BatchSize = 1000
                        };

        var firstTransaction = transactions.FirstOrDefault();
        if(firstTransaction is null) return false;

        // Отбрасываем лишние
        var innerTransactions = transactions.Where(x => x.Code >= settings.StartPosition);

        // Сохраняем
        var connect = _context.Database.GetDbConnection();
        var task = _writerRepository.SaveRows(connect, innerTransactions, settings );

        // Обновляем настройки
        var lastCode = innerTransactions.OrderByDescending(x => x.Code).First().Code;
        settings.StartPosition = lastCode;
        _settingReposity.Save( settings );

        Task.WaitAll( task );
        return true;
    }

    /// <InhericDoc/>
    public bool Push(Guid companyId, IEnumerable<JournalRowDto> transactions)
    {
        var company = _context.Companies.FirstOrDefault( x => x.Id == companyId) ?? throw new InvalidOperationException($"Невозможно получить карточку организации по коду {companyId}!");
        return Push(new CompanyModel() { Id = companyId}, transactions);
    }

    /// <InhericDoc/>
    public async Task<bool> PushAsync(Guid companyId, IEnumerable<JournalRowDto> transactions, CancellationToken token)
        => await Task.Run( () => Push( companyId, transactions), token);

    /// <InhericDoc/>
    public LoadingSettingsModel GetSettings(Guid companyId)
    {
        var company = _context.Companies.FirstOrDefault( x => x.Id == companyId ) ?? throw new InvalidOperationException($"Невозможно получить карточку организации по коду {companyId}!");

        // Конвертируем в модель
        var companyModel = new CompanyModel()
        {
            Id = companyId,
            Name = company.Name ?? string.Empty,
            Address = company.Address ?? string.Empty,
            INN = company.Inn ?? string.Empty
        };

        var settings = _settingReposity.Load( companyModel ) ;

         // Сформируем новый набор настроек и сохраним их.
        if (settings is null)
        {

            settings = new LoadingSettingsModel()
                        {
                            Owner = companyModel, StartPosition = 1, BatchSize = 1000
                        };
            _settingReposity.Save( settings );
        }

        return settings;
    }

    /// <InhericDoc/>
    public async Task<LoadingSettingsModel> GetSettingsAsync(Guid companyId, CancellationToken token)
        => await Task.Run( () => GetSettings(companyId), token);
}
