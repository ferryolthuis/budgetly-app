using Shared.App.Configuration;

namespace BankAccounts.App.Configuration;

public class PresentationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddControllers()
            .AddApplicationPart(Presentation.AssemblyReference.Assembly);
    }
}
