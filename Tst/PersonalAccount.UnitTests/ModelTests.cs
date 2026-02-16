using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;
using PersonalAccount.Domain.Core;
using PersonalAccount.Domain.Models;

namespace PersonalAccount.UnitTests;

/// <summary>
/// Набор тестов для проверки моделей
/// </summary>
public class ModelTests
{
    /// <summary>
    /// Проверка 5 запятых
    /// </summary>
    [Test]
    public void Address_5Commas_Valid()
    {
        // Подготовка
        var org = new Organization
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Inn = "Test",
            Address = "Test, Test, Test, Test, Test, Test" 
        };

        // Действие
        var results = ModelValidator.Validate(org);

        // Проверка
        Assert.That(results, Is.Empty);
    }

    /// <summary>
    /// Проверка 6 запятых
    /// </summary>
    [Test]
    public void Address_6Commas_Valid()
    {
        // Подготовка
        var org = new Organization
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Inn = "Test",
            Address = "Test, Test, Test, Test, Test, Test, Test" 
        };

        // Действие
        var results = ModelValidator.Validate(org);

        // Проверка
        Assert.That(results, Is.Empty);
    }

    /// <summary>
    /// Проверка невалидных запятых
    /// </summary>
    [Test]
    public void Address_Fail()
    {
        // Подготовка
        var org = new Organization
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Inn = "Test",
            Address = "Test"
        };

        // Действие
        var results = ModelValidator.Validate(org);

        // Проверка
        Assert.That(results.Any(x => x.ErrorMessage!.Contains("КЛАДР")), Is.True);
    }

    /// <summary>
    /// Проверка отсутствия обязательного поля
    /// </summary>
    [Test]
    public void Organization_RequiredName_IsNull_Fail()
    {
        // Подготовка
        var org = new Organization
        {
            Id = Guid.NewGuid(),
            Name = null,
            Inn = "Test",
            Address = "Test, Test, Test, Test, Test, Test"
        };

        // Действие
        var results = ModelValidator.Validate(org);

        // Проверка
        Assert.That(results.Any(x => x.MemberNames.Contains(nameof(Organization.Name))), Is.True);
    }

    /// <summary>
    /// Проверка валидного телефона сотрудника
    /// </summary>
    [Test]
    public void Phone_Valid()
    {
        // Подготовка
        var org = new Organization { 
            Id = Guid.NewGuid(), Name = "Test", Inn = "Test", Address = "Test, Test, Test, Test, Test, Test" 
        };
        
        var emp = new Employee
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Organization = org,
            Phone = "+79990001122"
        };

        // Действие
        var results = ModelValidator.Validate(emp);

        // Проверка
        Assert.That(results, Is.Empty);
    }

    /// <summary>
    /// Проверка невалидного телефона сотрудника
    /// </summary>
    [Test]
    public void Phone_Fail()
    {
        // Подготовка
        var org = new Organization { 
            Id = Guid.NewGuid(), Name = "Test", Inn = "Test", Address = "Test, Test, Test, Test, Test, Test" 
        };
        
        var emp = new Employee
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Organization = org,
            Phone = "Test" 
        };

        // Действие
        var results = ModelValidator.Validate(emp);

        // Проверка
        Assert.That(results, Is.Not.Empty);
    }

    /// <summary>
    /// Проверка отрицательной суммы
    /// </summary>
    [Test]
    public void NegativeAmount_Fail()
    {
        // Подготовка
        var org = new Organization { Id = Guid.NewGuid(), Name = "Test", Inn = "Test", Address = "Test, Test, Test, Test, Test, Test" };
        var cat = new Category { Id = Guid.NewGuid(), Name = "Test" };
        var nom = new Nomenclature { Id = Guid.NewGuid(), Name = "Test", Category = cat };

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Date = DateTimeOffset.Now,
            Organization = org,
            Nomenclature = nom,
            Type = OperationType.SALE,
            Quantity = 10,
            Amount = -100
        };

        // Действие
        var results = ModelValidator.Validate(transaction);

        // Проверка
        Assert.That(results.Any(x => x.MemberNames.Contains(nameof(Transaction.Amount))), Is.True);
    }

    /// <summary>
    /// Проверка длины номера чека
    /// </summary>
    [Test]
    public void Check_TooLong_Fail()
    {
        // Подготовка
        var dto = new JournalDto
        {
            Id = Guid.NewGuid(),
            CheckNumber = "TestTestTestTestTestTest", // 24 символа
            TransactionCode = 1,
            TransactionDate = DateTimeOffset.Now,
            Quantity = 1,
            Amount = 100,
            EmployeeCode = Guid.NewGuid(),
            NomenclatureCode = Guid.NewGuid()
        };

        // Действие
        var results = ModelValidator.Validate(dto);

        // Проверка
        Assert.That(results.Any(x => x.MemberNames.Contains(nameof(JournalDto.CheckNumber))), Is.True);
    }

    /// <summary>
    /// Проверка отсутствия пачки для загрузки
    /// </summary>
    [Test]
    public void BatchSize_Zero_Fail()
    {
        // Подготовка
        var settings = new LoadSettings
        {
            Id = Guid.NewGuid(),
            BatchSize = 0
        };

        // Действие
        var results = ModelValidator.Validate(settings);

        // Проверка
        Assert.That(results.Any(x => x.MemberNames.Contains(nameof(LoadSettings.BatchSize))), Is.True);
    }

    /// <summary>
    /// Проверка слишком большой пачки для загрузки
    /// </summary>
    [Test]
    public void BatchSize_TooLarge_Fail()
    {
        // Подготовка
        var settings = new LoadSettings
        {
            Id = Guid.NewGuid(),
            BatchSize = 10001
        };

        // Действие
        var results = ModelValidator.Validate(settings);

        // Проверка
        Assert.That(results.Any(x => x.MemberNames.Contains(nameof(LoadSettings.BatchSize))), Is.True);
    }
}