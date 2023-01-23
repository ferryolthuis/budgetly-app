using Shared.App.Configuration;

namespace Transactions.App.Configuration;

public class SwaggerServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen();
    }
}
