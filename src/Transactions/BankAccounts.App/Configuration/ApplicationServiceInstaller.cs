using FluentValidation;
using MediatR;
using Shared.App.Configuration;
using Shared.Application.Behaviors;

namespace BankAccounts.App.Configuration;

public class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Transactions.Application.AssemblyReference.Assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        //services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));

        services.AddValidatorsFromAssembly(
            Transactions.Application.AssemblyReference.Assembly,
            includeInternalTypes: true);
    }
}
