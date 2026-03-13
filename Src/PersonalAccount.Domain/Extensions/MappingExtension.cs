using System.Data;
using System.Reflection;
using PersonalAccount.Domain.Core;

namespace PersonalAccount.Domain.Extensions;

public static class MappingExtension
{
    /// <summary>
    /// Выполнить маппинг одной строки выборки ADO.net в указанную структуру Dto.
    /// </summary>
    /// <typeparam name="T"> Связанная струкура. </typeparam>
    /// <param name="source"> Исходный набор данных. </param>
    /// <returns></returns>
    public static T MapRow<T>(this DataRow source) where T: IDto
    {
        ArgumentNullException.ThrowIfNull(source);
        
        var result = Activator.CreateInstance<T>();
        var properties = typeof(T)
                         .GetProperties()
                         .Where(x => x.GetCustomAttribute<DataRowAttribute>(true) is not null)
                         .Select(x => new
                         {
                            Property = x,
                            Attribute = x.GetCustomAttribute<DataRowAttribute>(true)
                         })
                         .Where(x => x.Attribute is not null)
                         .ToList();

        var items =  properties.Select(x => new
        {
            Property = x.Property,
            ColumnName = x.Attribute?.FieldName
        })
        // Указано наименование поля в выборке
        .Where(x => !string.IsNullOrWhiteSpace(x.ColumnName))
        // Указанное наименование есть в выборке
        .Where(x => source.Table.Columns.Cast<DataColumn>().Any( y => y.ColumnName == x.ColumnName ))
        .ToList();

        try
        {
            foreach(var item in items)
            {
                var value = source[item.ColumnName!] as object;
                if (!item.Property.CanWrite) continue;
                var convertedValue = ConvertValue(item.Property.PropertyType, value, item.ColumnName!);
                
                item.Property.SetValue(result, convertedValue);
            }
        }
        catch(Exception ex)
        {
            throw new InvalidDataException($"Невозможно сконвертировать структуру {source.GetType().Name} в {typeof(T).Name}!\n{ex.Message}{ex.InnerException?.Message}");
        }

        return result;
    }

    /// <summary>
    /// Вспомогательный метод для конвертации значения в требуемый тип
    /// </summary>
    private static object? ConvertValue(Type targetType, object? value, string propertyName)
    {
        var isNullable = targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>);
        Type underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

        if (value is null)
        {
            if (isNullable || !underlyingType.IsValueType)
                return null;

            throw new InvalidCastException(
                $"Ошибка маппинга данных! Невозможно присвоить null значение non-nullable свойству {propertyName} типа {targetType}.");
        }

        if (underlyingType == value.GetType())
            return value;

        try
        {
            return Convert.ChangeType(value, underlyingType);
        }
        catch (Exception ex) when (ex is FormatException or InvalidCastException or ArgumentNullException or OverflowException or InvalidOperationException)
        {
            throw new InvalidCastException(
                $"Ошибка маппинга данных! Невозможно сконвертировать значение '{value}' в тип {underlyingType}. Поле в выборке {propertyName}.", ex);
        }
    }

}
