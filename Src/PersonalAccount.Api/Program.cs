using System.Reflection;
using DbUp;


var connectionString = "User ID=admin;Password=123456;Host=localhost;Port=5433;Database=personal_account;";
var upgrader =  DeployChanges.To
            .PostgresqlDatabase(  connectionString )
            .WithScriptsEmbeddedInAssembly(Assembly.GetAssembly(typeof(PersonalAccount.Data.PersonalAccountDataMarker)))
            .LogToConsole()
            .Build();

var result = upgrader.PerformUpgrade();
if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result.Error);
    Console.ResetColor();
}

