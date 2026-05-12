using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Common.Core;
using PersonalAccount.Domain.Models;
using PersonalAccount.Web.Models;

namespace PersonalAccount.Web.Controllers;

public class HomeController(IBranchRepository branchRepository, ISettingsRepository settingsRepository) : Controller
{
    private readonly IBranchRepository _branchRepository = branchRepository;
    private readonly ISettingsRepository _settingsRepository = settingsRepository;

    /// <summary>
    /// Настройки. Загрузка по выбранному филиалу.
    /// </summary>
    public IActionResult Index(Guid? branchId = null)
    {
        var branches = _branchRepository.GetBranches().ToList();
        var branch = branchId.HasValue
            ? branches.FirstOrDefault(x => x.Id == branchId.Value) ?? branches.First()
            : branches.First();

        var viewModel = new BranchSettingsModel
        {
            Branches = branches,
            BranchId = branch.Id,
            Name = branch.Name,
            Description = branch.Settings?.Description ?? string.Empty,
            StartPosition = branch.Settings?.StartPosition ?? 0,
            BatchSize = branch.Settings?.BatchSize ?? 1000
        };
        return View(viewModel);
    }

    public IActionResult SallingReport() => View();

    public IActionResult RevenueReport() => View();

    public IActionResult WorkScheduleReport() => View();

    /// <summary>
    /// Сохранить настройки с валидацией
    /// </summary>
    [HttpPost]
    public IActionResult SaveSettings(BranchSettingsModel model)
    {
        var branch = _branchRepository.GetBranch(model.BranchId);
        var settings = branch.Settings;
        settings.Description = model.Description ?? string.Empty;
        settings.StartPosition = model.StartPosition;
        settings.BatchSize = model.BatchSize;
        settings.Branch = new BranchModel { Id = branch.Id, Name = branch.Name };

        if (!settings.Validate())
        {
            model.Branches = _branchRepository.GetBranches().ToList();
            ViewData["Error"] = settings.ErrorText;
            return View("Index", model);
        }

        _settingsRepository.Save(settings);
        return RedirectToAction("Index", new { branchId = model.BranchId });
    }
}
