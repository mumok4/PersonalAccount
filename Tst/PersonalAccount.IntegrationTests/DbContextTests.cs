using System;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PersonalAccount.Data;

namespace PersonalAccount.IntegrationTests;

/*
Имя проверяемого метода
Сценарий, в котором тестируется метод
Ожидаемое поведение при вызове сценария
*/

/// <summary>
/// Набор тестов для проверки работы контекста базы данных.
/// </summary>
public class DbContextTests
{
    /// <summary>
    /// Проверить выборку данных. Получить список всех организаций.
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task FetchCompanies_PersonalAccountContext_Any()
    {
        // Подготовка
        var context = new PersonalAccountContext();

        // Действие
        var result = await context.Companies.ToListAsync(CancellationToken.None);

        // Проверка
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Any());
    }
}
