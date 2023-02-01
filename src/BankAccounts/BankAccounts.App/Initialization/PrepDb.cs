using BankAccounts.Domain.Entities;
using BankAccounts.Domain.ValueObjects;
using BankAccounts.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Result;

namespace BankAccounts.App.Initialization;

public static class PrepDb
{
    public static void PrepDatabase(this IApplicationBuilder app, bool isDev)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        using BankAccountsAppDbContext context = scope.ServiceProvider.GetService<BankAccountsAppDbContext>()!;
        MigrateDatabase(context);
        
        if (isDev)
        {
            SeedData(context);
        }
    }

    private static void MigrateDatabase(DbContext context)
    {
        Console.WriteLine("--> Migrating database to latest version");
        try
        {
            context.Database.Migrate();
        }
        catch (Exception e)
        {
            Console.WriteLine($"--> An error occured when applying migrations: {e.Message}");
        }
    }

    private static void SeedData(BankAccountsAppDbContext context)
    {
        if (!context.BankAccounts.Any())
        {
            Console.WriteLine("--> Seeding Database...");

            var checkingAccount = BankAccount.Create(Guid.NewGuid(), "Checking Account");
            var savingAccount = BankAccount.Create(Guid.NewGuid(), "Saving Account");
            
            Result<Transaction> transaction1 = checkingAccount.AddTransaction(
                new DateOnly(2022, 1, 1), "Bank", "Mortgage", "January Mortgage", Money.FromAmount(500m).Value, Money.Zero());
            Result<Transaction> transaction2 = checkingAccount.AddTransaction(
                new DateOnly(2022, 1, 2), "Saving Account", "Emergencies", "Monthly Savings", Money.FromAmount(100m).Value, Money.Zero());
            Result<Transaction> transaction3 = savingAccount.AddTransaction(
                new DateOnly(2022, 1, 2), "Checking Account", "Emergencies", "Monthly Savings", Money.Zero(), Money.FromAmount(100m).Value);
            
            context.BankAccounts.AddRange(
                checkingAccount,
                savingAccount
            );
            
            context.Transactions.AddRange(
                transaction1.Value,
                transaction2.Value,
                transaction3.Value
            );

            context.SaveChanges();
        }
    }
}
