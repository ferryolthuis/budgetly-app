using Microsoft.EntityFrameworkCore;
using Shared.App.Configuration;
using Shared.Persistence;
using Shared.Persistence.Interceptors;
using Transactions.Persistence;

namespace Transactions.App.Configuration;

public class DatabaseServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        services.AddSingleton<UpdateAuditableEntitiesInterceptor>();

        services.AddDbContext<TransactionsAppDbContext>(options => 
        {
            options.UseNpgsql(configuration.GetConnectionString("Database")!);
        });

        services.AddTransient<AppDbContext, TransactionsAppDbContext>();
    }
}
