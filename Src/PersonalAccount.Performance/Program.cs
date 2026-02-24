using System.Data;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PersonalAccount.Console.Models;
using PersonalAccount.Domain.Logics;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json");

var configuration = builder.Build();
var options = configuration.Get<ApplicationOptions>()
              ?? throw new InvalidOperationException("Invalid loading app configuration");

using var connect = new SqlConnection(options.ConnectionString);
connect.Open();

var stopwatch = new Stopwatch();

// Day
stopwatch.Start();

var sqlDay = "SELECT * FROM journal WHERE dater >= '2023-01-01' AND dater < '2023-01-02'";
var commandDay = new SqlCommand(sqlDay, connect);
var adapterDay = new SqlDataAdapter(commandDay);
var datasetDay = new DataSet();
adapterDay.Fill(datasetDay);
var dayRows = TableToModel.ConvertToJournalRows(datasetDay.Tables[0]);

stopwatch.Stop();
Console.WriteLine($"День:    {dayRows.Count} записей за {stopwatch.Elapsed.TotalMilliseconds} мс");

// Month
stopwatch.Restart();

var sqlMonth = "SELECT * FROM journal WHERE dater >= '2023-01-01' AND dater < '2023-02-01'";
var commandMonth = new SqlCommand(sqlMonth, connect);
var adapterMonth = new SqlDataAdapter(commandMonth);
var datasetMonth = new DataSet();
adapterMonth.Fill(datasetMonth);
var monthRows = TableToModel.ConvertToJournalRows(datasetMonth.Tables[0]);

stopwatch.Stop();
Console.WriteLine($"Месяц:   {monthRows.Count} записей за {stopwatch.Elapsed.TotalMilliseconds} мс");

// Quart
stopwatch.Restart();

var sqlQuarter = "SELECT * FROM journal WHERE dater >= '2023-01-01' AND dater < '2023-04-01'";
var commandQuarter = new SqlCommand(sqlQuarter, connect);
var adapterQuarter = new SqlDataAdapter(commandQuarter);
var datasetQuarter = new DataSet();
adapterQuarter.Fill(datasetQuarter);
var quarterRows = TableToModel.ConvertToJournalRows(datasetQuarter.Tables[0]);

stopwatch.Stop();
Console.WriteLine($"Квартал: {quarterRows.Count} записей за {stopwatch.Elapsed.TotalMilliseconds} мс");