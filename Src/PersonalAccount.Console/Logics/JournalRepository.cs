using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using PersonalAccount.Common.Core;
using PersonalAccount.Console.Models;
using PersonalAccount.Domain.Extensions;
using PersonalAccount.Domain.Models;
using PersonalAccount.Domain.Models.Dto;

namespace PersonalAccount.Console.Logics;

/// <summary>
/// Репозиторий для чтения транзакций из источника
/// </summary>
public class JournalRepository : IClientRepository<JournalRowDto>
{
    private readonly ConsoleOptions _options;

    /// <summary>
    /// Создать экземпляр репозитория
    /// </summary>
    /// <param name="options"> Настройки консольного приложения </param>
    public JournalRepository(IOptions<ConsoleOptions> options)
    {
        ArgumentNullException.ThrowIfNull(options);
        _options = options.Value;
    }

    private const string _sql = @"
    select top {0}
            j.transnumber,
            j.transtype,
            j.receiptn,
            j.dater,
            j.quantity,
            j.price,
            j.discountamount,
            LTRIM(RTRIM(ISNULL(p.lastname,'') + ' ' + ISNULL(p.firstname,'') + ' ' + ISNULL(p.middlename,''))) as employee_name,
            c.description as category_name,
            n.description as nomenclature_name
        from journal j
        left join personnel p on j.loginid = p.cardid
        left join category c on j.categoryid = c.categoryid
        left join product n on j.id = n.productid
        where j.transtype in (387, 386, 211, 216, 101, 102)
        and j.transnumber >= {1}";

    /// <summary>
    /// Получить пакет записей журнала согласно настройкам загрузки
    /// </summary>
    /// <param name="connection"> Подключение к базе данных </param>
    /// <param name="options"> Настройки загрузки </param>
    public async Task<IEnumerable<JournalRowDto>> GetRows(DbConnection connection, LoadingSettingsModel options)
    {
        ArgumentNullException.ThrowIfNull(connection);
        ArgumentNullException.ThrowIfNull(options);

        var sql = string.Format(_sql, options.BatchSize, options.StartPosition);

        try
        {
            if (connection.State == ConnectionState.Closed)
                await connection.OpenAsync();

            var command = new SqlCommand(sql, (SqlConnection)connection);
            var dataset = new DataSet();
            var adapter = new SqlDataAdapter(command);
            adapter.Fill(dataset);

            var rows = from s in dataset.Tables[0].Rows.Cast<DataRow>()
                       select s.MapRow<JournalRowDto>();

            return rows;
        }
        catch (Exception ex)
        {
            throw new InvalidDataException(
                $"Невозможно выполнить SQL запрос {sql}\n{ex.Message}{ex.InnerException?.Message}");
        }
        finally
        {
            await connection.CloseAsync();
        }
    }
}