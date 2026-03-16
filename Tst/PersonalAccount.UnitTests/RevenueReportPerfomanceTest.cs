using System.Diagnostics;
using NUnit.Framework;
using PersonalAccount.Api.Logics;

namespace PersonalAccount.UnitTests;


/// <summary>
/// Проверка эффективности старого и нового методов
/// </summary>
[TestFixture]
public class RevenueReportPerformanceTests
{
    [Test]
    [TestCase(100)]
    [TestCase(1_000)]
    [TestCase(100_000)]
    public void Performance_RevenueReportService_Measure(int count)
    {
        var creator = new PerformanceDataCreator();
        creator.Build(count);
        var service = new RevenueReportService();

        service.Create(creator.Transactions).ToList();

        var sw = Stopwatch.StartNew();
        var result = service.Create(creator.Transactions).ToList();
        sw.Stop();

        Console.WriteLine($"[RevenueReportService] {count}, строк: {result.Count}, время: {sw.ElapsedMilliseconds} мс");
        Assert.That(result, Is.Not.Empty);
    }

    [Test]
    [TestCase(100)]
    [TestCase(1_000)]
    [TestCase(100_000)]
    public void Performance_NewRevenueReportService_Measure(int count)
    {
        var creator = new PerformanceDataCreator();
        creator.Build(count);
        var service = new NewRevenueReportService();

        service.Create(creator.Transactions).ToList();

        var sw = Stopwatch.StartNew();
        var result = service.Create(creator.Transactions).ToList();
        sw.Stop();

        Console.WriteLine($"[NewRevenueReportService] {count}, строк: {result.Count}, время: {sw.ElapsedMilliseconds} мс");
        Assert.That(result, Is.Not.Empty);
    }
}