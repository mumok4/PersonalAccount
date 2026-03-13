using PersonalAccount.Domain.Models;

namespace PersonalAccount.UnitTests;

public class ReportDataCreator
{
    /// <summary>
    /// Набор фейковых категорий.
    /// </summary>
    public List<CategoryModel> Categories { get; set; } = new List<CategoryModel>();

    /// <summary>
    /// Набор фейковых организаци.
    /// </summary>
    public List<CompanyModel> Companies {get; set; } = new List<CompanyModel>();

    /// <summary>
    /// Набор фейковых сотрудников.
    /// </summary>
    public List<EmploeeModel> Emploees {get; set;} = new List<EmploeeModel>();


    /// <summary>
    /// Набор фейковых номенклатур.
    /// </summary>
    public List<NomenclatureModel> Nomenclatures {get; set;} = new List<NomenclatureModel>();

    /// <summary>
    /// Набор фейковых транзакций
    /// </summary>
    public List<TransactionModel> Transactions {get;set;} = new List<TransactionModel>();

    /// <summary>
    /// Сформировать фейковый набор данных для модульного тестирования (простой сценарий)
    /// </summary>
    public void BuildTypicalScenario()
    {
        // Организации
        Companies.Add( new CompanyModel()
        {
            Id = Guid.NewGuid(),
            Name = "TEST"
        });

        // Категории
        var categories = Enumerable.Range( 0, 3 ) 
        .Select((position, x) => new CategoryModel()
        {
            Id = Guid.NewGuid(),
            Name = $"TEST - {position}",
            Owner = Companies.First()
        });

        Categories.AddRange( categories );

        // Сотрудники
        var emploees = Enumerable.Range( 0, 5 )
        .Select((position, x ) => new EmploeeModel()
        {
            Id = Guid.NewGuid(),
            Name = $"TEST - {position}",
            Owner = Companies.First()
        });

        Emploees.AddRange( emploees );

        // Номенклатура
        Nomenclatures.AddRange( 
            new List<NomenclatureModel>()
            {
                new NomenclatureModel()
                {
                    Id = Guid.NewGuid(),
                    Name = "Яйцо варенное",
                    Category = Categories[0]
                },
                new NomenclatureModel()
                {
                    Id = Guid.NewGuid(),
                    Name = "Салат закусочный",
                    Category = Categories[1]
                },
                new NomenclatureModel()
                {
                    Id = Guid.NewGuid(),
                    Name = "Кофе",
                    Category = Categories[2]
                }
            });
            

        // Транзакции
        Transactions.AddRange(
            new List<TransactionModel>()
            {
                // Чек # 1
                // Яйцо, продажа
                new TransactionModel()
                {
                    Id = Guid.NewGuid(),
                    TicketNumber = "1",
                    Period = DateTimeOffset.Now,
                    Owner = Companies.First(),
                    Quantuty = 2,
                    Nomenclature = Nomenclatures[0],
                    Price = 200,
                    Type = Domain.Core.TransactionType.Sale
                },

                // Салат закусочный, продажа
                new TransactionModel()
                {
                    Id = Guid.NewGuid(),
                    TicketNumber = "1",
                    Period = DateTimeOffset.Now,
                    Owner = Companies.First(),
                    Quantuty = 1,
                    Nomenclature = Nomenclatures[1],
                    Price = 300.20,
                    Type = Domain.Core.TransactionType.Sale
                },

                // Кофе, продажа
                new TransactionModel()
                {
                    Id = Guid.NewGuid(),
                    Period = DateTimeOffset.Now,
                    TicketNumber = "1",
                    Owner = Companies.First(),
                    Quantuty = 1,
                    Nomenclature = Nomenclatures[2],
                    Price = 120,
                    Type = Domain.Core.TransactionType.Sale
                },

                // Оплата банком
                new TransactionModel()
                {
                    Id = Guid.NewGuid(),
                    Period = DateTimeOffset.Now,
                    TicketNumber = "1",
                    Type = Domain.Core.TransactionType.BankPayment,
                    Quantuty = 1,
                    Price = 820.2,
                    Emploee = Emploees[1],
                },


                // Чек № 2
                // Яйцо, продажа
                new TransactionModel()
                {
                    Id = Guid.NewGuid(),
                    TicketNumber = "2",
                    Period = DateTimeOffset.Now,
                    Owner = Companies.First(),
                    Quantuty = 2,
                    Nomenclature = Nomenclatures[0],
                    Price = 200,
                    Type = Domain.Core.TransactionType.Sale
                },

                // Салат закусочный, продажа
                new TransactionModel()
                {
                    Id = Guid.NewGuid(),
                    TicketNumber = "2",
                    Period = DateTimeOffset.Now,
                    Owner = Companies.First(),
                    Quantuty = 1,
                    Nomenclature = Nomenclatures[1],
                    Price = 300.20,
                    Type = Domain.Core.TransactionType.Sale
                },

                // Кофе, продажа
                new TransactionModel()
                {
                    Id = Guid.NewGuid(),
                    Period = DateTimeOffset.Now,
                    TicketNumber = "2",
                    Owner = Companies.First(),
                    Quantuty = 1,
                    Nomenclature = Nomenclatures[2],
                    Price = 120,
                    Type = Domain.Core.TransactionType.Sale
                },

                // Оплата наличными
                new TransactionModel()
                {
                    Id = Guid.NewGuid(),
                    Emploee = Emploees[0],
                    Period = DateTimeOffset.Now,
                    TicketNumber = "2",
                    Type = Domain.Core.TransactionType.CashPayment,
                    Quantuty = 1,
                    Price = 1000
                },

                // Сдача
                new TransactionModel()
                {
                    Id = Guid.NewGuid(),
                    Emploee = Emploees[0],
                    Period = DateTimeOffset.Now,
                    TicketNumber = "2",
                    Type = Domain.Core.TransactionType.RefundPayment,
                    Quantuty = 1,
                    Price = 179.8
                }
            });


    }
}
