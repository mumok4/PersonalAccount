using System.Data.Common;
using PersonalAccount.Domain.Core;
using PersonalAccount.Domain.Models;

namespace PersonalAccount.Common.Core;

/// <summary>
/// Интерфейс для работы с клиентскими данными.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IClientRepository<T> : IHandler<T> where T: IDto
{
    /// <summary>
    /// Получить пакет данных согласно настройкам.
    /// </summary>
    /// <param name="connection"> Абстрактный коннект. </param>
    /// <param name="options"> Настройки. </param>
    /// <returns></returns>
    public Task<IEnumerable<T>> GetRows(DbConnection connection, LoadingSettingsModel options);
}
