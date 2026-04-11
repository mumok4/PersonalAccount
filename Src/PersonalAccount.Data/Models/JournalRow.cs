using System;

namespace PersonalAccount.Data.Models;

public partial class JournalRow
{
    public Guid Id { get; set; } 
    public long Code { get; set; }
    public long TypeCode { get; set; }
    public long ReceiptNumber { get; set; }
    public DateTime Period { get; set; }
    public double Quantity { get; set; }
    public double Price { get; set; }
    public double Discount { get; set; }
    
    public string? EmploeeName { get; set; }
    public string? CategoryName { get; set; }
    public string? NomenclatureName { get; set; }
}
