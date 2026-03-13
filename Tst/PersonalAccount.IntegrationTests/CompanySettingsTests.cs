using System;
using NUnit.Framework;
using PersonalAccount.Data.Logics;
using PersonalAccount.Domain.Models;

namespace PersonalAccount.IntegrationTests;


/*
Имя проверяемого метода
Сценарий, в котором тестируется метод
Ожидаемое поведение при вызове сценария
*/


public class CompanySettingsTests
{
    /// <summary>
    /// Прверить работу метода Load класса <see cref="CompanySettingsRepository"/>
    /// </summary>
    /// <returns></returns>
    [Test]
    [TestCase("14e54725-0efc-42b8-a27d-a84f9a7257c5")]
    public async Task  Load_CompanySettingsRepository_NotThrow(string companyId)
    {
        // Подготова
        var repo = new CompanySettingsRepository();
        var company = new CompanyModel()
        {
            Id = new Guid( companyId )
        };

        // Проверки и действие
        Assert.DoesNotThrowAsync( async() =>
        {
            var result = await repo.Load(company, CancellationToken.None);
            Assert.That(result is not null);
        });
    }

    /// <summary>
    /// Проверить работу метода Save класса <see cref="CompanySettingsRepository"/>
    /// </summary>
    /// <returns></returns>
    [Test]
      [TestCase("14e54725-0efc-42b8-a27d-a84f9a7257c5")]
    public async Task Save_CompanySettingsRepository_NotThrow(string companyId)
    {
        // Подготовка
        var repo = new CompanySettingsRepository();
          var company = new CompanyModel()
        {
            Id = new Guid( companyId )
        };
        var setting = new LoadingSettingsModel()
        {
            Owner = company, BatchSize = 10, StartPosition = 0
        };

        // Действие и проверка
        Assert.DoesNotThrowAsync(async () =>
        {
            await repo.Save(setting, CancellationToken.None);
            var result = await repo.Load(company, CancellationToken.None);

            Assert.That(result.StartPosition == setting.StartPosition, Is.True);
        });
    }
}
