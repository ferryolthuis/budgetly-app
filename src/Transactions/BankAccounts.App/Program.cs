using BankAccounts.Persistence;
using Shared.App.Configuration;
using AssemblyReference = BankAccounts.App.AssemblyReference;

var builder = WebApplication.CreateBuilder(args);
builder.Services.InstallServices(builder.Configuration, AssemblyReference.Assembly);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TransactionsAppDbContext>();
    await context.Database.EnsureCreatedAsync();
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
