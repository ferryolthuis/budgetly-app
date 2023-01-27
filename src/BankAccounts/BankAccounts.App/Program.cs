using BankAccounts.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.App.Configuration;
using AssemblyReference = BankAccounts.App.AssemblyReference;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.InstallServices(builder.Configuration, AssemblyReference.Assembly);

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    BankAccountsAppDbContext context = scope.ServiceProvider.GetRequiredService<BankAccountsAppDbContext>();
    await context.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
