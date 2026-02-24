using System;

namespace PersonalAccount.Domain.Core;

/// <summary>
/// Атрибут для маппинга.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class DbColumnAttribute : Attribute
{
    /// <summary>
    /// Наименование колонки в таблице.
    /// </summary>
    public string ColumnName { get; }

    /// <summary>
    /// Тип данных колонки.
    /// </summary>
    public Type ColumnType { get; }

    public DbColumnAttribute(string columnName, Type columnType)
    {
        ColumnName = columnName;
        ColumnType = columnType;
    }
}