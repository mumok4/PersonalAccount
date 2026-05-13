using System;
using PersonalAccount.Common.Core;
using PersonalAccount.Web.Logics;

namespace PersonalAccount.Web.Extensions;

public static class RegistryExtension
{
    /// <summary>
    /// Зарегистрировать в контейнере сервисы модуля PersonalAccount.Web
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection RegistryPersonalAccountWeb
    (
        this IServiceCollection services,
          IConfiguration configuration
    )
    {
        services.AddScoped<IBranchRepository, BranchRepository>();
        services.AddScoped<ISettingsService, SettingsService>();
        return services;
    }
}
