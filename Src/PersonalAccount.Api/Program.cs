using System.Reflection;
using DbUp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalAccount.Data.Extensions;
using PersonalAccount.Common.Core;
using PersonalAccount.Api.Logics;

// Настройки и построитель Web приложения
var builder = WebApplication.CreateBuilder();

builder.Configuration.AddJsonFile("appsettings.json");

var connectionString = builder.Configuration.GetValue<string>("ApiOptions:PostgreConnection")!;

// Миграции
var upgrader = DeployChanges.To
            .PostgresqlDatabase(connectionString)
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
        .RegistryPersonalAccountData(builder.Configuration);

builder.Services.AddScoped<ILoadingService, LoadingService>();

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