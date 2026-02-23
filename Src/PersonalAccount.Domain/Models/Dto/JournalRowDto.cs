using PersonalAccount.Domain.Core;

namespace PersonalAccount.Domain.Models.Dto;

/// <summary>
/// Записть в журнале в клиентской программе.
/// </summary>
public class JournalRowDto : IDto
{
    /// <summary>
    /// Уникальный код транзакции.
    /// </summary>
    [DbColumn("transnumber", typeof(long))]
    public long Code {get;set;}

    /// <summary>
    /// Уникальный код типа транзакции.
    /// </summary>
    [DbColumn("transtype", typeof(int))]
    public int TypeCode {get;set;}

    /// <summary>
    /// Номер чека.
    /// </summary>
    [DbColumn("receiptn", typeof(decimal))]
    public decimal ReceiptNumber {get;set;}

    /// <summary>
    /// Уникальный код продукта.
    /// </summary>
    [DbColumn("id", typeof(string))]
    public string? ProductCode {get;set;}

    /// <summary>
    /// Уникальный код категории продуктов.
    /// </summary>
    [DbColumn("categoryid", typeof(string))]
    public string? CategoryCode {get;set;}

    /// <summary>
    /// Код сотрудника.
    /// </summary>
    [DbColumn("loginid", typeof(string))]
    public string? EmploeeCode {get;set;}

    /// <summary>
    /// Дата время транзакции.
    /// </summary>
    [DbColumn("dater", typeof(DateTime))]
    public DateTimeOffset Period {get;set;}

    /// <summary>
    /// Количество.
    /// </summary>
    [DbColumn("quantity", typeof(double))]
    public double Quantity {get;set;}

    /// <summary>
    /// Цена.
    /// </summary>
    [DbColumn("price", typeof(double))]
    public double Price {get;set;}

    /// <summary>
    /// Сумма скидки.
    /// </summary>
    [DbColumn("discount", typeof(double))]
    public double Discount {get;set;}

    /// <summary>
    /// Сумма скидки в деньгах.
    /// </summary>
    [DbColumn("discountamount", typeof(double))]
    public double DiscountAmount {get;set;}

    public override string ToString()
    {
        return $"{Period}: транзакция - {Quantity * Price}";
    }
}