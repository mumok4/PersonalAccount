using System;
using System.Reflection;
using System.Text.RegularExpressions;
using NUnit.Framework;
using PersonalAccount.Domain;
using PersonalAccount.Domain.Core;
using PersonalAccount.Domain.Models;

namespace PersonalAccount.UnitTests;


/*
Имя проверяемого метода
Сценарий, в котором тестируется метод
Ожидаемое поведение при вызове сценария
*/


/// <summary>
/// Набор модульных тестов в рамках приложения.
/// </summary>
public class ApplicationTests
{
    /// <summary>
    /// Проверить получение версии сборки приложения.
    /// </summary>
    [Test]
    public void CurrentVersion_Show_Any()
    {
        // Подготовка
        var version = CurrentApplication.CurrentVersion();

        // Действие

        // Проверка
        Assert.That(!string.IsNullOrEmpty(version));
    }

    /// <summary>
    /// Проверяем создание категории
    /// </summary>
    [Test]
    public void Create_Category_CheckNullName()
    {
        // Подготовка
        var domain = new Category();

        // Действие

        // Проверка
        Assert.That(domain.Name is not null);
    }

    /// <summary>
    /// Проверяем наличие атрибутов
    /// </summary>
    [Test]
    public void Create_Category_ExistsAttributes()
    {
        // Подготовка
        var type = typeof(Category);

        // Действие
        var properties = type.GetProperties().Where(x => x.GetCustomAttributes(true).Any());
        
        // Проверка
        Assert.That(properties.Any());
    }

    /// <summary>
    /// Проверяем наличие аттрибута телефона
    /// </summary>
    [Test]
    public void Create_Employee_ExistsPhoneTemplateAttribute()
    {
        // Подготовка
        var org = new Organization 
        { 
            Id = Guid.NewGuid(),
            Name = "Test Org",
            Inn = "1234567890",
            Address = "Россия, Москва, ул. Ленина, д.1, кв.1"
        };
         
        var domain = new Employee
        {
            Id = Guid.NewGuid(),
            Name = "Tester",
            Organization = org,
            Phone = "+79001234567"
        };

        // Действие
        var properties = domain.GetType().GetProperties().Where(x => x.GetCustomAttribute<PhoneTemplateAttribute>(true) is not null);
        var attribute = properties.First().GetCustomAttribute<PhoneTemplateAttribute>();
        var match = new Regex(attribute!.Template);

        // Проверка
        Assert.That(properties.Any());
        Assert.That(!string.IsNullOrEmpty(attribute!.Template));
        Assert.That(match.IsMatch(domain.Phone!));


    
    }
}
