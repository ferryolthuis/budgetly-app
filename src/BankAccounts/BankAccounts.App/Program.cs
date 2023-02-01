using BankAccounts.App.Initialization;
using Shared.App.Configuration;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.InstallServices(builder.Configuration, BankAccounts.App.AssemblyReference.Assembly);

WebApplication app = builder.Build();

app.PrepDatabase(app.Environment.IsDevelopment());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
