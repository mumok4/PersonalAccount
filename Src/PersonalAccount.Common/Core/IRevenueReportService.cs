using System;
using PersonalAccount.Domain.Models;
using PersonalAccount.Domain.Models.Dto;

namespace PersonalAccount.Common.Core;

/// <summary>
/// Интерфейс для сервиса построения отчета "Выручка"
/// </summary>
public interface IRevenueReportService : IHandler<RevenueDto>
{
    /// <summary>
    /// Сформировать отчет
    /// </summary>
    /// <param name="transactions"></param>
    /// <returns></returns>
    public IEnumerable<RevenueDto> Create( IEnumerable<TransactionModel> transactions);

    /// <summary>
    /// Сформировать отчет ассинхронно
    /// </summary>
    /// <param name="transactions"></param>
    /// <returns></returns>
    public Task<IEnumerable<RevenueDto>> CreateAsync( IEnumerable<TransactionModel> transactions, CancellationToken token);
}
