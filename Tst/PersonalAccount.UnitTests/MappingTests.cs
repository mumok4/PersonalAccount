using System;
using System.Data;
using NUnit.Framework;
using PersonalAccount.Domain.Extensions;
using PersonalAccount.Domain.Models.Dto;

namespace PersonalAccount.UnitTests;

/*
Имя проверяемого метода
Сценарий, в котором тестируется метод
Ожидаемое поведение при вызове сценария
*/


/// <summary>
/// Набор тестов для проверки маппинга данных.
/// </summary>
public class MappingTests
{
    /// <summary>
    /// Проверить маппинг DataRow в Dto. Положительный сценарий.
    /// </summary>
    [Test]
    public void MapRow_Mapping_NotThrow()
    {
        // Подготовка
        var table = new DataTable("test");
        var columns = new List<DataColumn>()
        {
            new DataColumn()
            { 
                ColumnName = "dater", 
                DataType= typeof(DateTime)},

            new DataColumn()
            {
                ColumnName = "transtype",
                DataType = typeof(int)
            }
        };
        table.Columns.AddRange(columns.ToArray());
        var row = table.NewRow();
        var period = DateTime.Now;
        row["dater"] = period;
        row["transtype"] = 1;

        // Действие
        var result = row.MapRow<JournalRowDto>();

        // Проверки
        Assert.That(result is not null);
        Assert.That(result!.TypeCode == 1);
        Assert.That(result.Period == period);
    }
}
