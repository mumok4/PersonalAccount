using System;
using PersonalAccount.Domain.Models;
using PersonalAccount.Domain.Models.Dto;

namespace PersonalAccount.Common.Core;

/// <summary>
/// Интерфейс для сервиса построения отчета "График работы"
/// </summary>
public interface IWorkScheduleReportService : IHandler<WorkScheduleDto>
{
    /// <summary>
    /// Сформировать отчет
    /// </summary>
    /// <param name="transactions"></param>
    /// <returns></returns>
    public IEnumerable<WorkScheduleDto> Create( IEnumerable<TransactionModel> transactions);

    /// <summary>
    /// Сформировать отчет ассинхронно
    /// </summary>
    /// <param name="transactions"></param>
    /// <returns></returns>
    public Task<IEnumerable<WorkScheduleDto>> CreateAsync( IEnumerable<TransactionModel> transactions, CancellationToken token);
}
