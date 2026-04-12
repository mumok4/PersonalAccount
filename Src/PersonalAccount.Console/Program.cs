using System.Data.Common;
using System.Net.Http.Json;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PersonalAccount.Common.Core;
using PersonalAccount.Console.Logics;
using PersonalAccount.Console.Models;
using PersonalAccount.Domain;
using PersonalAccount.Domain.Models;
using PersonalAccount.Domain.Models.Dto;

CurrentApplication.ShowLogo();

var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
var configuration = builder.Build();

var services = new ServiceCollection();
services.Configure<ConsoleOptions>(configuration.GetSection("ConsoleOptions"));
services.AddTransient<IClientRepository<JournalRowDto>, JournalRepository>();

var provider = services.BuildServiceProvider();

var options = provider.GetRequiredService<IOptions<ConsoleOptions>>().Value;
var repository = provider.GetRequiredService<IClientRepository<JournalRowDto>>();

long currentStartPosition = 1;

try
{
    using var initClient = new HttpClient();
    var positionUrl = $"{options.ApiUrl}/api/transactions/position/{options.CompanyId}";
    currentStartPosition = await initClient.GetFromJsonAsync<long>(positionUrl);
    Console.WriteLine($"Возобновление с позиции: {currentStartPosition}");
}
catch
{
    Console.WriteLine("Не удалось получить позицию, начинаем с 1");
}

while (true)
{
    try 
    {
        using (var client = new HttpClient())
        {
            var url = $"{options.ApiUrl}/api/transactions/push/{options.CompanyId}";
            
            using (DbConnection connection = new SqlConnection(options.MsSqlConnection))
            {
                var settings = new LoadingSettingsModel 
                { 
                    BatchSize = 3000, 
                    StartPosition = currentStartPosition 
                };
                
                var rows = await repository.GetRows(connection, settings);
                
                if (rows.Count() > 0) 
                {
                    Console.WriteLine($"Отправка записей: {rows.Count()}");
                    var response = await client.PostAsJsonAsync(url, rows);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        currentStartPosition = rows.Max(x => x.Code) + 1;
                        Console.WriteLine("Данные отправлены успешно.");
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка API: {response.StatusCode}");
                    }
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Произошла ошибка: {ex.Message}");
    }

    await Task.Delay(500); 
}