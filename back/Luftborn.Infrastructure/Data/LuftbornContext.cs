using Microsoft.EntityFrameworkCore;
using Luftborn.Core.Common;
using Luftborn.Core.Entities;
using System.Reflection;

namespace Luftborn.Infrastructure.Data;

public class LuftbornContext : DbContext
{
    public LuftbornContext(DbContextOptions<LuftbornContext> options) : base(options)
    {

    }

    public DbSet<Employee> Employees { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public void BeginTransaction()
    {
        if (Database.CurrentTransaction == null)
            Database.BeginTransaction();
    }

    public async Task<int> CommitTransactionAsync()
    {
        if (Database.CurrentTransaction != null)
            Database.CommitTransaction();

        return await SaveChangesAsync();
    }

    public async Task<int> RollbackTransactionAsync()
    {
        if (Database.CurrentTransaction != null)
            Database.RollbackTransaction();

        return await SaveChangesAsync();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = "Luftborn";
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = "Luftborn";
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}