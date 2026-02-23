namespace PersonalAccount.Domain.Core;

/// <summary>
/// Набор типов транзакции.
/// </summary>
public enum TransactionType : long
{
    /// <summary>
    /// Продажа
    /// </summary>
    Sale = 101 ,

    /// <summary>
    /// Списание по себестоимости
    /// </summary>
    Writeoff = 111,

    /// <summary>
    /// Оплата наличкой
    /// </summary>
    Cash = 211,

    /// <summary>
    /// Оплата картой
    /// </summary>
    Visa = 216,

    /// <summary>
    /// ВХод в систему
    /// </summary>
    Login = 301,
    
    /// <summary>
    /// Начало смены
    /// </summary>
    StartShift = 386,

    /// <summary>
    /// Конец смены
    /// </summary>
    StopShift = 387,

    /// <summary>
    /// Сумма
    /// </summary>
    Total = 501,
}
