using PersonalAccount.Domain.Core;

namespace PersonalAccount.Domain.Models.Dto;

/// <summary>
/// Записть в журнале в клиенсткой программе.
/// </summary>
public class JournalRowDto : IDto
{
    /// <summary>
    /// Уникальный код транзакции.
    /// </summary>
    [DataRow("transnumber")]
    public long Code {get;set;}

    /// <summary>
    /// Уникальный код типа транзакции.
    /// </summary>
    [DataRow("transtype")]
    public long TypeCode {get;set;}

    /// <summary>
    /// Номер чека.
    /// </summary>
    [DataRow("receiptn")]
    public long ReceiptNumber {get;set;}

    /// <summary>
    /// Уникальный код продукта.
    /// </summary>
    [DataRow("productid")]
    public long? ProductCode {get;set;}

    /// <summary>
    /// Уникальный код категории продуктов.
    /// </summary>
    [DataRow("categoryid")]
    public long? CategoryCode {get;set;}

    /// <summary>
    /// Код сотрудника.
    /// </summary>
    [DataRow("emploeeid")]
    public long? EmploeeCode {get;set;}

    /// <summary>
    /// Дата время транзакции.
    /// </summary>
    [DataRow("dater")]
    public DateTime Period {get;set;}

    /// <summary>
    /// Количество.
    /// </summary>
    [DataRow("quantity")]
    public double Quantity {get;set;}

    /// <summary>
    /// Цена.
    /// </summary>
    [DataRow("price")]
    public double Price {get;set;}

    /// <summary>
    /// Сумма скидки.
    /// </summary>
    [DataRow("discountamount")]
    public double Discount {get;set;}

    public override string ToString()
        => $"{Period}:Транзакция - {Quantity * Price}";
}
