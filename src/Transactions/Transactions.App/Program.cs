using Shared.App.Configuration;
using Transactions.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.InstallServices(builder.Configuration, Transactions.App.AssemblyReference.Assembly);

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
