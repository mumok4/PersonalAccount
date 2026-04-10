using System.Reflection;
using DbUp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalAccount.Data.Extensions;


var connectionString = "User ID=admin;Password=123456;Host=localhost;Port=5433;Database=personal_account;";

// Настройки и построитель Web приложения
var builder = WebApplication.CreateBuilder();
var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

// Миграции
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


// Подключение сервисов
builder.Services
        .RegistryPersonalAccountData( configuration );

// Настройки Web
builder.Services.AddControllers();
builder.WebHost.UseUrls("http://0.0.0.0:8000");

// Web приложение
var application = builder.Build();
application.UseDeveloperExceptionPage();
application.UseRouting();
application.MapControllers();

// Запуск
application.Run();