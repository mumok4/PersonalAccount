using PersonalAccount.Web.Models;

namespace PersonalAccount.Web.Logics;

public interface ISettingsService
{
    /// <summary>
    /// Сохранить настройки филиала
    /// </summary>
    /// <param name="model"></param>
    /// <exception cref="ValidationException"></exception>
    void SaveSettings(BranchSettingsModel model);
}
