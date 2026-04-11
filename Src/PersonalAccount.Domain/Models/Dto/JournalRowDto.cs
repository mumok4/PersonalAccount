using PersonalAccount.Domain.Core;

namespace PersonalAccount.Domain.Models.Dto;

public class JournalRowDto : IDto
{
    [DataRow("transnumber")]
    public long Code {get;set;}

    [DataRow("transtype")]
    public long TypeCode {get;set;}

    [DataRow("receiptn")]
    public long ReceiptNumber {get;set;}

    [DataRow("productid")]
    public long? ProductCode {get;set;}

    [DataRow("categoryid")]
    public long? CategoryCode {get;set;}

    [DataRow("emploeeid")]
    public long? EmploeeCode {get;set;}

    [DataRow("dater")]
    public DateTime Period {get;set;}

    [DataRow("quantity")]
    public double Quantity {get;set;}

    [DataRow("price")]
    public double Price {get;set;}

    [DataRow("discountamount")]
    public double Discount {get;set;}

    [DataRow("employee_name")] 
    public string? EmploeeName { get; set; }

    [DataRow("category_name")]
    public string? CategoryName { get; set; }

    [DataRow("nomenclature_name")]
    public string? NomenclatureName { get; set; }

    public override string ToString()
        => $"{Period}:Транзакция - {Quantity * Price}";
}
