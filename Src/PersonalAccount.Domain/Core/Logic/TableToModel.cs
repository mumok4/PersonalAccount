using PersonalAccount.Domain.Core;
using PersonalAccount.Domain.Models.Dto;
using System.Data;
using System.Reflection;

namespace PersonalAccount.Domain.Logics;

/// <summary>
/// Класс для маппинга строк таблицы в DTO модели.
/// </summary>
public static class TableToModel
{
    /// <summary>
    /// Преобразует DataTable в JournalRowDto.
    /// </summary>
    public static List<JournalRowDto> ConvertToJournalRows(DataTable table)
    {
        var result = new List<JournalRowDto>();

        for (int i = 0; i < table.Rows.Count; i++)
        {
            var row = table.Rows[i];
            var dto = new JournalRowDto();

            var properties = typeof(JournalRowDto).GetProperties();

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<DbColumnAttribute>();

                if (attribute == null)
                    continue;

                if (!table.Columns.Contains(attribute.ColumnName))
                    continue;

                var value = row[attribute.ColumnName];

                if (value is DBNull)
                    continue;

                if (property.PropertyType == typeof(DateTimeOffset) && value is DateTime dateTime)
                {
                    property.SetValue(dto, new DateTimeOffset(dateTime));
                    continue;
                }

                property.SetValue(dto, Convert.ChangeType(value, attribute.ColumnType));
            }

            result.Add(dto);
        }

        return result;
    }
}