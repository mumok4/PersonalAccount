using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PersonalAccount.Console.Logics;
using PersonalAccount.Console.Models;
using PersonalAccount.Common.Core;
using PersonalAccount.Domain.Models.Dto;
using NUnit.Framework;
using Microsoft.Data.SqlClient;

namespace PersonalAccount.IntegrationTests;

/*
Имя проверяемого метода
Сценарий, в котором тестируется метод
Ожидаемое поведение при вызове сценария
*/


/// <summary>
/// Набор интеграционных тестов для проверки работы различных репозиториев.
/// </summary>
public class RepositoryTests
{
    // Настройки текущие
    private ConsoleOptions _options;
    private IServiceProvider _provider; 

    public RepositoryTests()
    {
        var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("testsettings.json");

        var configuration = builder.Build();
        _options = configuration.Get<ConsoleOptions>()
                        ?? throw new InvalidOperationException("Unabled loading appsettings.json!");

        var services = new ServiceCollection();
        services.Configure<ConsoleOptions>(configuration.GetSection("ConsoleOptions"));
        services.AddTransient<IClientRepository<JournalRowDto>, JournalRepository>();
        
        _provider = services.BuildServiceProvider();
        _options = _provider.GetRequiredService<IOptions<ConsoleOptions>>().Value;
    }

    /// <summary>
    /// Простой тест для замера производительности.
    /// </summary>
    /// <param name="rows"></param>
    /// <returns></returns>
    [Test]
    [TestCase(100)]
    [TestCase(1000)]
    [TestCase(10000)]
    public async Task GetRows_JournalRepository_Fetch(int rows)
    {
        // Подготовка
        using var connect = new SqlConnection(_options.MsSqlConnection);

        var repo = _provider.GetRequiredService<IClientRepository<JournalRowDto>>();

        // Действие
        var result = await repo.GetRows(connect, new Domain.Models.LoadingSettingsModel() { BatchSize = rows });

        // Проверки
        Assert.That(result is not null);
        Assert.That(result!.Any());
        Assert.That(result!.Count() == rows);
    }
}
