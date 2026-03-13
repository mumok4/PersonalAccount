using System;
using System.Threading.Tasks;
using NUnit.Framework;
using PersonalAccount.Api.Logics;

namespace PersonalAccount.UnitTests;

/*
Имя проверяемого метода
Сценарий, в котором тестируется метод
Ожидаемое поведение при вызове сценария
*/

/// <summary>
/// Набор модульных тестов для проверки работы отчета "Выручка"
/// </summary>
public class RevenueReportServiceTests
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
        creator.BuildTypicalScenario();


        // Действие
        var result = service.Create( creator.Transactions );

        // Проверка
        Assert.That(result.Any());
        Assert.That(result.First().BankAmount == typicalResult);
        Assert.That(result.First().CashAmount == typicalResult);
    }
}
