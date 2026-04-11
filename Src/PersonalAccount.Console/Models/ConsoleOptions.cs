namespace PersonalAccount.Console.Models;

public class ConsoleOptions
{
    public string MsSqlConnection { get; set; } = string.Empty;
    public string PostgreConnection { get; set; } = string.Empty;
    public string ApiUrl { get; set; } = string.Empty;
    public Guid CompanyId { get; set; }
}