using Azure.Core.GeoJson;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PersonalAccount.Console.Models;
using PersonalAccount.Domain;
using PersonalAccount.Domain.Core;
using PersonalAccount.Domain.Models;
using System.Data;
using System.Data.Common;
using PersonalAccount.Domain.Models;
using PersonalAccount.Domain.Models.Dto;

CurrentApplication.ShowLogo();

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json");

var configuration = builder.Build();
var options = configuration.Get<ApplicationOptions>()
                ?? throw new InvalidOperationException("Invalid loading app configuration");

using var connect = new SqlConnection(options.ConnectionString);
connect.Open();

// // variant 1
// var sql = "select Top 10 * from journal";
// var command = new SqlCommand(sql, connect);
// var reader = command.ExecuteReader();
// var position = 1;

// while (reader.Read())
// {
//     Console.WriteLine($"{position} - {reader[0]}, {reader[4]}");
// }

// variant 2
var sql = "select Top 10 * from journal";
var command = new SqlCommand(sql, connect);
var adapter = new SqlDataAdapter(command);
var dataset = new DataSet();

adapter.Fill(dataset);

var table = dataset.Tables[0];
for (int position = 0; position < table.Rows.Count; position++)
{
    var dto = new JournalRowDto();
    dto.Period = Convert.ToDateTime( table.Rows[position]["dater"]);
    dto.Quantity = Convert.ToDouble( table.Rows[position]["quantity"]);
    dto.Price = Convert.ToDouble( table.Rows[position]["price"]); 

    Console.WriteLine(dto);
}

while (true)
{
    await Task.Delay(TimeSpan.FromHours(1));
}

