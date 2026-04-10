using System;
using PersonalAccount.Domain.Models;
using PersonalAccount.Domain.Models.Dto;

namespace PersonalAccount.Common.Core;

/// <summary>
/// Интерфейс для сервиса построения отчета "Продажи"
/// </summary>
public interface ISalesReportService : IHandler<SellingDto>
{
    /// <summary>
    /// Сформировать отчет
    /// </summary>
    /// <param name="transactions"></param>
    /// <returns></returns>
    public IEnumerable<SellingDto> Create( IEnumerable<TransactionModel> transactions);

    /// <summary>
    /// Сформировать отчет ассинхронно
    /// </summary>
    /// <param name="transactions"></param>
    /// <returns></returns>
    public Task<IEnumerable<SellingDto>> CreateAsync( IEnumerable<TransactionModel> transactions, CancellationToken token);
}
