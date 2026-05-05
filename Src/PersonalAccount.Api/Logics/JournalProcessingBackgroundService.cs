using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PersonalAccount.Common.Core;
using PersonalAccount.Data.Models;
using PersonalAccount.Domain.Core;
using PersonalAccount.Domain.Models;

namespace PersonalAccount.Api.Logics;

/// <summary>
/// Фоновый процесс: читает необработанные строки журнала и раскладывает их по бизнес-таблицам.
/// </summary>
public class JournalProcessingBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<JournalProcessingBackgroundService> _logger;

    public JournalProcessingBackgroundService(
        IServiceScopeFactory scopeFactory,
        ILogger<JournalProcessingBackgroundService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ProcessAllBranchesAsync(stoppingToken);
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в цикле фонового процесса обработки журнала");
            }

            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken).ConfigureAwait(false);
        }
    }

    private async Task ProcessAllBranchesAsync(CancellationToken token)
    {
        using var scope = _scopeFactory.CreateScope();
        var context  = scope.ServiceProvider.GetRequiredService<PersonalAccountContext>();
        var journal  = scope.ServiceProvider.GetRequiredService<IJournalDataService>();
        var extract  = scope.ServiceProvider.GetRequiredService<IEntityExtractService>();
        var dataRepo = scope.ServiceProvider.GetRequiredService<IBusinessDataRepository>();
        var settings = scope.ServiceProvider.GetRequiredService<ICompanySettingsRepository>();

        var branches = await context.Branches.ToListAsync(token);

        foreach (var branch in branches)
        {
            try
            {
                await ProcessBranchAsync(branch, journal, extract, dataRepo, settings, token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка обработки филиала {BranchId}", branch.Id);
            }
        }
    }

    private static async Task ProcessBranchAsync(
        Branch branch,
        IJournalDataService journal,
        IEntityExtractService extract,
        IBusinessDataRepository dataRepo,
        ICompanySettingsRepository settingsRepo,
        CancellationToken token)
    {
        var company     = new CompanyModel { Id = branch.CompanyId };
        var branchModel = new BranchModel  { Id = branch.Id, Name = branch.Name ?? string.Empty, Owner = company };

        LoadingSettingsModel loadSettings;
        try { loadSettings = settingsRepo.Load(company); }
        catch { return; }

        var rows = (await journal.GetUnprocessedRowsAsync(branchModel, (int)loadSettings.BatchSize, token)).ToList();
        if (rows.Count == 0) return;

        var newCats = (await extract.ExtractNewCategoriesAsync(rows, token)).ToList();
        if (newCats.Count > 0)
            await dataRepo.SaveCategoriesAsync(newCats, token);

        var newNom = (await extract.ExtractNewNomenclatureAsync(rows, token)).ToList();
        if (newNom.Count > 0)
            await dataRepo.SaveNomenclatureAsync(newNom, token);

        var newEmp = (await extract.ExtractNewEmployeesAsync(rows, token)).ToList();
        if (newEmp.Count > 0)
            await dataRepo.SaveEmployeesAsync(newEmp, token);

        var transactions = rows.Select(r => new TransactionModel
        {
            Id           = Guid.NewGuid(),
            Type         = (TransactionType)r.TypeCode,
            TicketNumber = r.ReceiptNumber.ToString(),
            Owner        = company,
            Branch       = branchModel,
            Period       = new DateTimeOffset(r.Period, TimeSpan.Zero),
            Nomenclature = new NomenclatureModel
            {
                Code     = r.ProductCode ?? 0,
                Name     = r.ProductName ?? string.Empty,
                Category = new CategoryModel(),
            },
            Emploee = new EmploeeModel
            {
                Code  = r.EmploeeCode  ?? 0,
                Name  = r.EmploeeName  ?? string.Empty,
                Owner = company,
            },
            Price    = r.Price,
            Quantuty = r.Quantity,
            Discount = r.Discount,
        });
        await dataRepo.SaveTransactionsAsync(transactions, token);

        loadSettings.StartPosition = rows.Last().Code;
        settingsRepo.Save(loadSettings);

    }
}
