using PersonalAccount.Domain.Core;
using PersonalAccount.Domain.Models;

namespace PersonalAccount.UnitTests;

/// <summary>
/// Генератор данных для нагрузочных тестовы
/// </summary>
public class PerformanceDataCreator
{
    public List<TransactionModel> Transactions { get; private set; } = new();

    public void Build(int count)
    {
        Transactions.Clear();

        var company      = new CompanyModel      { Id = Guid.NewGuid(), Name = "ООО Тест" };
        var category     = new CategoryModel     { Id = Guid.NewGuid(), Name = "Напитки", Owner = company };
        var nomenclature = new NomenclatureModel { Id = Guid.NewGuid(), Name = "Кофе", Category = category };
        var employee     = new EmploeeModel      { Id = Guid.NewGuid(), Name = "Тестовый сотрудник", Owner = company };

        var baseDate = new DateTimeOffset(new DateTime(2024, 1, 1), TimeSpan.Zero);

        for (int i = 0; i < count; i++)
        {
            var date = baseDate.AddDays(i % 30);

            switch (i % 3)
            {
                case 0: AddTicketWithDiscount(i, date, company, nomenclature, employee); break;
                case 1: AddTicketWithChange  (i, date, company, nomenclature, employee); break;
                case 2: AddTicketWithBank    (i, date, company, nomenclature, employee); break;
            }
        }
    }

    // Чек со скидкой(оплата наличными)
    private void AddTicketWithDiscount(
        int i, DateTimeOffset date,
        CompanyModel company, NomenclatureModel nomenclature, EmploeeModel employee)
    {
        Transactions.Add(new TransactionModel
        {
            Id = Guid.NewGuid(), TicketNumber = i.ToString(), Period = date,
            Owner = company, Emploee = employee, Nomenclature = nomenclature,
            Type = TransactionType.Sale, Price = 100, Quantuty = 1, Discount = 10
        });
        Transactions.Add(new TransactionModel
        {
            Id = Guid.NewGuid(), TicketNumber = i.ToString(), Period = date,
            Owner = company, Emploee = employee, Nomenclature = nomenclature,
            Type = TransactionType.CashPayment, Price = 90, Quantuty = 1
        });
    }

    // Чек со сдачей(оплата наличными)
    private void AddTicketWithChange(
        int i, DateTimeOffset date,
        CompanyModel company, NomenclatureModel nomenclature, EmploeeModel employee)
    {
        Transactions.Add(new TransactionModel
        {
            Id = Guid.NewGuid(), TicketNumber = i.ToString(), Period = date,
            Owner = company, Emploee = employee, Nomenclature = nomenclature,
            Type = TransactionType.Sale, Price = 100, Quantuty = 1
        });
        Transactions.Add(new TransactionModel
        {
            Id = Guid.NewGuid(), TicketNumber = i.ToString(), Period = date,
            Owner = company, Emploee = employee, Nomenclature = nomenclature,
            Type = TransactionType.CashPayment, Price = 200, Quantuty = 1
        });
        Transactions.Add(new TransactionModel
        {
            Id = Guid.NewGuid(), TicketNumber = i.ToString(), Period = date,
            Owner = company, Emploee = employee, Nomenclature = nomenclature,
            Type = TransactionType.RefundPayment, Price = 100, Quantuty = 1
        });
    }

    // Чек с оплатой банком
    private void AddTicketWithBank(
        int i, DateTimeOffset date,
        CompanyModel company, NomenclatureModel nomenclature, EmploeeModel employee)
    {
        Transactions.Add(new TransactionModel
        {
            Id = Guid.NewGuid(), TicketNumber = i.ToString(), Period = date,
            Owner = company, Emploee = employee, Nomenclature = nomenclature,
            Type = TransactionType.Sale, Price = 100, Quantuty = 1
        });
        Transactions.Add(new TransactionModel
        {
            Id = Guid.NewGuid(), TicketNumber = i.ToString(), Period = date,
            Owner = company, Emploee = employee, Nomenclature = nomenclature,
            Type = TransactionType.BankPayment, Price = 100, Quantuty = 1
        });
    }
}