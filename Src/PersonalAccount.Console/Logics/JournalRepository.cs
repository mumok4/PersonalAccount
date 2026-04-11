using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using PersonalAccount.Common.Core;
using PersonalAccount.Console.Models;
using PersonalAccount.Domain.Extensions;
using PersonalAccount.Domain.Models;
using PersonalAccount.Domain.Models.Dto;
using System.Data;
using System.Data.Common;

namespace PersonalAccount.Console.Logics;

public class JournalRepository : IClientRepository<JournalRowDto>
{
    private readonly ConsoleOptions _options;

    public JournalRepository(IOptions<ConsoleOptions> options)
    {
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
    public async Task<IEnumerable<JournalRowDto>> GetRows(DbConnection connection, LoadingSettingsModel options)
    {
        var sql = string.Format(_sql, options.BatchSize, options.StartPosition);    
        
        if(connection.State == ConnectionState.Closed) 
        {
            await connection.OpenAsync();
        }
        
        var command = new SqlCommand(sql, (SqlConnection)connection);
        var dataset = new DataSet();
        var adapter = new SqlDataAdapter(command);
        adapter.Fill(dataset);

        await connection.CloseAsync(); 

        var result = from s in dataset.Tables[0].Rows.Cast<DataRow>()
                     select s.MapRow<JournalRowDto>();

        return result;
    }
}
