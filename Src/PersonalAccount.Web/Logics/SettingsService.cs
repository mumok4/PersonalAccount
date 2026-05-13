using System.ComponentModel.DataAnnotations;
using PersonalAccount.Common.Core;
using PersonalAccount.Domain.Models;
using PersonalAccount.Web.Models;

namespace PersonalAccount.Web.Logics;

public class SettingsService(IBranchRepository branchRepository, ISettingsRepository settingsRepository) : ISettingsService
{
    private readonly IBranchRepository _branchRepository = branchRepository;
    private readonly ISettingsRepository _settingsRepository = settingsRepository;

    /// <summary>
    /// Сохранить настройки филиала
    /// </summary>
    /// <param name="model"></param>
    /// <exception cref="ValidationException"></exception>
    public void SaveSettings(BranchSettingsModel model)
    {
        var branch = _branchRepository.GetBranch(model.BranchId);
        var settings = branch.Settings;
        settings.Description = model.Description ?? string.Empty;
        settings.StartPosition = model.StartPosition;
        settings.BatchSize = model.BatchSize;
        settings.Branch = new BranchModel { Id = branch.Id, Name = branch.Name };

        if (!settings.Validate())
            throw new ValidationException(settings.ErrorText);

        _settingsRepository.Save(settings);
    }
}
