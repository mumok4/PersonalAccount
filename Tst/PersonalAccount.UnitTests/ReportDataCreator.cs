using System.Text.Json;
using System.Text.Json.Serialization;
using PersonalAccount.Domain.Models;

namespace PersonalAccount.UnitTests;

public class ReportDataCreator
{
    // Наименование исходного файла (типовые продажи)
    private const string _typicalPaymentTransactions = "TypicalPaymentTransactions.json";

    // Наименование исходного файла (график работы)
    private const string _typicalWorkScheduleTransactions = "TypicalWorkScheduleTransactions.json";

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
    /// Загрузить данные из источника и сформировать тестовый набор данных.
    /// </summary>
    /// <param name="fileName"> Исходный Json файл. </param>
    private void Build(string fileName)
    {
        if (!File.Exists(fileName))
            throw new FileNotFoundException($"Файл данных не найден: {fileName}");

        var json = File.ReadAllText(fileName);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        var data = JsonSerializer.Deserialize<ReportDataCreator>(json, options)
                    ?? throw new InvalidOperationException("Не удалось десериализовать данные из JSON.");

        Categories = data.Categories;
        Companies = data.Companies;
        Emploees = data.Emploees;
        Nomenclatures = data.Nomenclatures;
        Transactions = data.Transactions;
    }

    /// <summary>
    /// Сформировать фейковый набор данных для модульного тестирования (простой сценарий, продажи)
    /// </summary>
    public void BuildTypicalPaymentScenario()
        => Build(_typicalPaymentTransactions);

    /// <summary>
    /// Сформировать фейковый набор данных для модульного тестирования (простой пример, график работы)
    /// </summary>
    public void BuildTypicalWorkSheduleScenario()
        => Build(_typicalWorkScheduleTransactions)  ;  
}
