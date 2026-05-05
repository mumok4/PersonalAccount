using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalAccount.Api.Logics;
using PersonalAccount.Common.Core;
using PersonalAccount.Data.Logics;
using PersonalAccount.Domain.Models.Dto;

namespace PersonalAccount.Api.Extensions;

/// <summary>
/// Регистрация сервисов модуля в DI
/// </summary>
public static class RegistryExtension
{
    /// <summary>
    /// Зарегистрировать в контейнере сервисы модуля PersonalAccount.Api
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection RegistryPersonalAccountApi
    (
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped< IRevenueReportService, RevenueReportService>();
        services.AddScoped< ISalesReportService, SalesReportService>();
        services.AddScoped< IWorkScheduleReportService, WorkScheduleReportService>();
        services.AddScoped< IServerRepository<JournalRowDto> , JournalWriteRepository >();
        services.AddScoped< ILoadingService, LoadingService>();
        services.AddScoped< IJournalDataService, JournalDataService >();
        services.AddScoped< IEntityExtractService, EntityExtractService >();
        services.AddScoped< IBusinessDataRepository, BusinessDataRepository >();
        services.AddHostedService< JournalProcessingBackgroundService >();
        return services;
    }
}
