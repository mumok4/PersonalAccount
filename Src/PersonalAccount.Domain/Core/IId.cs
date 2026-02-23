using System;

namespace PersonalAccount.Domain.Core;

/// <summary>
/// Общий интерфейс для работы с моделями.
/// </summary>
public interface IId : IModel
{
    /// <summary>
    /// Уникальный код.
    /// </summary>
    public Guid Id {get;set;}
}
