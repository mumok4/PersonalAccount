namespace PersonalAccount.Domain.Core;

/// <summary>
/// Набор типов транзакции.
/// select * from transtype where transtypeid in (387, 386, 211, 216, 101)
/// </summary>
public enum TransactionType : long
{
    /// <summary>
    /// Продажа.
    /// </summary>
    Sale = 101 ,

    /// <summary>
    /// Оплата наличными.
    /// </summary>
    CashPayment = 211,

    /// <summary>
    /// Оплата банком
    /// </summary>
    BankPayment = 216,

    /// <summary>
    /// Начала рабочей смены.
    /// </summary>
    StartShift = 386,

    /// <summary>
    /// Окончание рабочей смены.
    /// </summary>
    StopShift = 387,

    /// <summary>
    /// Сдача
    /// </summary>
    RefundPayment = 102
}
