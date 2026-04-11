using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalAccount.Common.Core;
using PersonalAccount.Data.Logics;

namespace PersonalAccount.Data.Extensions;

public static class RegistryExtension
{
    public static IServiceCollection RegistryPersonalAccountData(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<Domain.Models.Options.ApiOptions>(configuration.GetSection("ApiOptions"));

        services.AddScoped<ICompanySettingsRepository, CompanySettingsRepository>();
        services.AddScoped<IJournalRowRepository, JournalRowRepository>(); 

        var connectionString = configuration.GetValue<string>("ApiOptions:PostgreConnection");
        
        services.AddDbContext<PersonalAccountContext>(
            x => x.UseNpgsql(connectionString)
        );

        return services;
    }
}
