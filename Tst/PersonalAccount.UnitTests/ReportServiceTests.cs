using NUnit.Framework;
using PersonalAccount.Api.Logics;

namespace PersonalAccount.UnitTests;

/*
Имя проверяемого метода
Сценарий, в котором тестируется метод
Ожидаемое поведение при вызове сценария
*/

/// <summary>
/// Набор модульных тестов для проверки работы отчетов.
/// </summary>
public class ReportServiceTests
{
    /// <summary>
    /// Проверить работу метода Create класс RevenueReportService
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task Create_RevenueReportService_Check()
    {
        // Подготовка
        const double typicalResult = 820.2;
        var service = new RevenueReportService();
        var creator = new ReportDataCreator();
        creator.BuildTypicalPaymentScenario(); 


        // Действие
        var result = await service.CreateAsync( creator.Transactions, CancellationToken.None );

        // Проверка
        Assert.That(result.Any());
        Assert.That(result.First().BankAmount == typicalResult);
        Assert.That(result.First().CashAmount == typicalResult);
    }

    /// <summary>
    /// Проверить работу метода Create класс SalesReportService
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task Create_SalesReportService_Check()
    {
        // Подготовка
        const double typicalResult = 820.2;
        var service = new SalesReportService();
        var creator = new ReportDataCreator();
        creator.BuildTypicalPaymentScenario();

        // Действие
        var result = await service.CreateAsync( creator.Transactions, CancellationToken.None );

        // Проверка
        Assert.That(result.Any());
        Assert.That(result.Sum( x => x.Amount) == typicalResult * 2);
    }

    /// <summary>
    /// Проверить работу метода Create класс WorkScheduleReportService
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task Create_WorkScheduleReportService_Check()
    {
        // Подготовка
        var service = new WorkScheduleReportService();
        var creator = new ReportDataCreator();
        creator.BuildTypicalWorkSheduleScenario();

        // Действие
        var result = await service.CreateAsync( creator.Transactions, CancellationToken.None );

        // Проверка
        Assert.That(result.Any());
        Assert.That(result.Count() == 2);
    }
}
