using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PersonalAccount.Common.Core;
using PersonalAccount.Web.Logics;
using PersonalAccount.Web.Models;

namespace PersonalAccount.Web.Controllers;

public class HomeController(IBranchRepository branchRepository, ISettingsService settingsService) : Controller
{
    private readonly IBranchRepository _branchRepository = branchRepository;
    private readonly ISettingsService _settingsService = settingsService;

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
        try
        {
            _settingsService.SaveSettings(model);
            return RedirectToAction("Index", new { branchId = model.BranchId });
        }
        catch (ValidationException ex)
        {
            model.Branches = _branchRepository.GetBranches().ToList();
            ViewData["Error"] = ex.Message;
            return View("Index", model);
        }
    }
}
