using Shared.App.Configuration;

namespace BankAccounts.App.Configuration;

public class SwaggerServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen();
    }
}
