using System;

namespace PersonalAccount.Domain.Core;

/// <summary>
/// Отдельный атрибут для настройки маппинга DataRow с Dto структурой.
/// </summary>
public class DataRowAttribute : Attribute
{
    /// <summary>
    /// Наименование поля.
    /// </summary>
    public string FieldName {get;set;} = null!;

    /// <summary>
    /// Создать атрибут с указанными параметрами.
    /// </summary>
    /// <param name="fieldName"> Наименование поля для выборки. </param>
    public DataRowAttribute(string fieldName) => FieldName = fieldName.Trim();
}
