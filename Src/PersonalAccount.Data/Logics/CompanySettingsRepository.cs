using System.Text.Json;
using PersonalAccount.Common.Core;
using PersonalAccount.Data.Models;
using PersonalAccount.Domain.Models;

namespace PersonalAccount.Data.Logics;

/// <summary>
/// Реализация интерфейса <see cref="ICompanySettingsRepository"/>
/// </summary>
public class CompanySettingsRepository : ICompanySettingsRepository
{
    // Контекст для работы с базой данных
    private readonly PersonalAccountContext _context;

    /// <summary>
    /// Создать обхект типа <see cref="CompanySettingsRepository"/>
    /// </summary>
    /// <param name="context"> Контекст для работы с базой данных </param>
    public CompanySettingsRepository(PersonalAccountContext context) => _context = context;

    /// <summary>
    /// Загрузить настройки
    /// </summary>
    /// <param name="company"> Организация. </param>
    /// <returns></returns>
    public LoadingSettingsModel Load(CompanyModel company)
    {
        var item = _context.Companies.FirstOrDefault(x => x.Id == company.Id)
            ?? throw new InvalidDataException($"Не найдена организация по коду {company.Id}!");
        var json = !string.IsNullOrEmpty( item.LoadOptions ) ? item.LoadOptions
            :  throw new InvalidDataException($"Организация по коду {company.Id} содержит некорретные данные по настройкам!");

        var result = JsonSerializer.Deserialize< LoadingSettingsModel >(json)
            ?? throw new InvalidDataException($"Организация по коду {company.Id} содержит некорретные данные по настройкам!");
        return result;
    }


    /// <summary>
    /// Загрузить настройки
    /// </summary>
    /// <param name="company"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<LoadingSettingsModel> LoadAsync(CompanyModel company, CancellationToken token)
        => await Task.Run( () => Load( company ), token);

    /// <summary>
    /// Сохранить настройки
    /// </summary>
    /// <param name="setting"> Настройки </param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task SaveAsync(LoadingSettingsModel setting, CancellationToken token)
        => await Task.Run( () => Save(setting), token);

    /// <summary>
    /// Сохранить настройки
    /// </summary>
    /// <param name="setting"></param>
    public void Save(LoadingSettingsModel setting)
    {
        var companyId = setting.Owner?.Id ?? throw new InvalidDataException("Невозможно сохранить настройки т.к. нет информации об организации!");
        var company = _context.Companies.FirstOrDefault(x => x.Id == companyId)
            ?? throw new InvalidDataException($"Не найдена организация по коду {companyId}!");

        var text =     JsonSerializer.Serialize(setting);
        company.LoadOptions = text;
        _context.SaveChanges();
    }
}
