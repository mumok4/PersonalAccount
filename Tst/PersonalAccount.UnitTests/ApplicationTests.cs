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
    /// Проверяеи создание категории с наименование 1= null;
    /// </summary>
    [Test]
    public void Create_Category_CheckNullName()
    {
        // Подготовка
        var domain = new Category();

        // Действие

        // Проверка
        Assert.That (domain.Name is not null);
    }

    /// <summary>
    /// Проверяем наличие системных атрибутов.
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
    /// Проверить наличите атрибута <see cref="TemplateAttribute"/>
    /// </summary>
    [Test]
    public void Create_Emploee_ExistsPhoneTemplateAttribute()
    {
        // Подготовка
        var domain = new Emploee() { Phone = "+79041528366" };

        // Действие
        var properties = domain.GetType()
                        .GetProperties()
                        .Where(x => x.GetCustomAttribute<TemplateAttribute>(true) is not null );

        // Проверки
        Assert.That(properties.Any());        
        var attribute = properties.First().GetCustomAttribute< TemplateAttribute >();
        Assert.That(!string.IsNullOrEmpty( attribute!.Template));

        var match = new Regex(attribute!.Template);
        Assert.That(match.IsMatch(domain.Phone ?? string.Empty) == true);
    }

    /// <summary>
    /// Проверить соответствие формат адреса КЛАДР.
    /// </summary>
    /// <param name="address"></param>
    [Test]
    [TestCase(",Иркутская область, мкн. Современник,,,30,13", true)]
    [TestCase("190000, Ленинградская обл., Ломоносовский р-н, г. Ломоносов, ул. Советская, д. 12", true)]
    [TestCase("Ерунда,,,", false)]
    public void Create_Company_CheckTemplateAddress(string address, bool result )
    {
        // Подготовка
        var domain = new Company()
        {
            Name = "test", Id = Guid.NewGuid(), Address = address
        };

        // Действие
        var property = domain.GetType().GetProperty("Address");
                        
        // Проверки                
        var attribute = property!.GetCustomAttribute< TemplateAttribute >();
        Assert.That(!string.IsNullOrEmpty( attribute!.Template));
        var match = new Regex(attribute!.Template);
        Assert.That(match.IsMatch(domain.Address?? string.Empty) == result);
    }

    /// <summary>
    /// Комплектная проверка. Отрицательный сценарий.
    /// </summary>
    [Test]
    public void Create_Transaction_FalseValidate()
    {
        // Подготовка
        var transaction = new Transaction()
        {
            Emploee = new Emploee() { Name = "test"},
            Owner = new Company() { Name = "test"},
            Type = TransactionType.Sale,
            Quantuty = 1, Price = 1 , 
            Discount = 0, 
            Period = DateTimeOffset.Now,
            Nomenclature = new Nomenclature()
        };

        // Действие
        var result = transaction.Validate();

        // Проверка
        Assert.That(result == false);
        Assert.That(transaction.IsError == true);
        Console.WriteLine(transaction.ErrorText);
    }

    /// <summary>
    /// Комплексная проверка. Положительный сценарий.
    /// </summary>
    [Test]
    public void Create_Transaction_TrueValidate()
    {
          // Подготовка
        var transaction = new Transaction()
        {
            Emploee = new Emploee() 
            { 
                Name = "test" , 
                Phone = "+79041518166", 
                Owner = new Company() { Name = "test", INN = "1234567890", Address = "90000, Ленинградская обл., Ломоносовский р-н, г. Ломоносов, ул. Советская, д. 12"},
            },
            Owner = new Company() 
            { 
                Name = "test", 
                INN = "1234567890", 
                Address = "90000, Ленинградская обл., Ломоносовский р-н, г. Ломоносов, ул. Советская, д. 12"
            },
            Type = TransactionType.Sale,
            Quantuty = 1, Price = 1 , 
            Discount = 0, 
            Period = DateTimeOffset.Now,
            Nomenclature = new Nomenclature() 
            { 
                Name = "Test", 
                Category = new Category()
                { 
                    Name = "Test", 
                    Owner = new Company() { Name = "test", INN = "1234567890", Address = "90000, Ленинградская обл., Ломоносовский р-н, г. Ломоносов, ул. Советская, д. 12"},
                }}
        };

        // Действие
        var result = transaction.Validate();

        // Проверка
        Assert.That(result == true);
        Assert.That(transaction.IsError == false);
    }
}
