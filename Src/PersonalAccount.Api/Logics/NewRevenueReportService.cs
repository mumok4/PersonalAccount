using PersonalAccount.Domain.Core;
using PersonalAccount.Common.Core;
using PersonalAccount.Domain.Models;
using PersonalAccount.Domain.Models.Dto;

namespace PersonalAccount.Api.Logics;


/// <summary>
///  Вторая реализация <see cref="IRevenueReportService"/>
/// </summary>
public class NewRevenueReportService : IRevenueReportService
{
    public IEnumerable<RevenueDto> Create(IEnumerable<TransactionModel> transactions)
    {
        if (!transactions.Any()) return Enumerable.Empty<RevenueDto>();

        var buckets = new Dictionary<DateTime, RevenueDto>();

        foreach (var t in transactions)
        {
            var day = t.Period.Date;
            RevenueDto dto;

            if (!buckets.ContainsKey(day))
            {
                dto = new RevenueDto { Period = day, Owner = t.Owner?.Id ?? Guid.Empty };
                buckets[day] = dto;
            }
            else
            {
                dto = buckets[day];
            }

            switch (t.Type)
            {
                case TransactionType.BankPayment:   dto.BankAmount     += t.Price * t.Quantuty - t.Discount; break;
                case TransactionType.CashPayment:   dto.CashAmount     += t.Price * t.Quantuty - t.Discount; break;
                case TransactionType.RefundPayment: dto.CashAmount     -= t.Price * t.Quantuty;              break;
                case TransactionType.Sale:          dto.DiscountAmount += t.Discount;                        break;
            }
        }

        return buckets.Values;
    }

    public async Task<IEnumerable<RevenueDto>> CreateAsync(
        IEnumerable<TransactionModel> transactions, CancellationToken token)
        => await Task.Run(() => Create(transactions), token);
}