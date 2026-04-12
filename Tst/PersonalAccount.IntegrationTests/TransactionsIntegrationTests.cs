using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PersonalAccount.Api.Logics;
using PersonalAccount.Common.Core;
using PersonalAccount.Data;
using PersonalAccount.Data.Extensions;
using PersonalAccount.Domain.Models;
using PersonalAccount.Domain.Models.Dto;

namespace PersonalAccount.IntegrationTests;

public class TransactionsIntegrationTests
{
    private IServiceProvider _provider;

    [SetUp]
    public void Setup()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("testsettings.json");

        var configuration = builder.Build();
        var services = new ServiceCollection();
        
        services.RegistryPersonalAccountData(configuration);
        services.AddScoped<ILoadingService, LoadingService>();

        _provider = services.BuildServiceProvider();
    }

    [Test]
    public async Task PushAsync_ShouldSaveDataSuccessfully()
    {
        // Подготовка
        var loadingService = _provider.GetRequiredService<ILoadingService>();
        var context = _provider.GetRequiredService<PersonalAccountContext>();
        var company = new CompanyModel { Id = Guid.Parse("14e54725-0efc-42b8-a27d-a84f9a7257c5") };

        var testData = new List<JournalRowDto>
        {
            new JournalRowDto 
            { 
                Code = 99999, 
                Period = DateTime.Now, 
                Price = 100, 
                Quantity = 1, 
                EmploeeName = "Тестовый Сотрудник" 
            }
        };

        // Действие
        var result = await loadingService.PushAsync(company, testData, CancellationToken.None);

        // Проверка
        Assert.That(result, Is.True);

        var saved = await context.JournalRows.FirstOrDefaultAsync(x => x.Code == 99999);
        Assert.That(saved, Is.Not.Null);
    }
}