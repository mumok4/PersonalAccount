using PersonalAccount.Domain.Core;

namespace PersonalAccount.Common.Core;

/// <summary>
/// Абстрактный интерфейс репозиторий для наследования.
/// </summary>
public interface IHandler<T> where T: IDto;

