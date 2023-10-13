using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Trinca.AgendaChurrasco.Domain.Shared.Entities;

namespace Trinca.AgendaChurrasco.Data.Interceptors;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var entries = eventData.Context.ChangeTracker
            .Entries()
            .Where(x => x.Entity is ISoftDeletable && x.State == EntityState.Deleted);
        
        foreach (var entry in entries)
        {
            entry.State = EntityState.Modified;
            (entry.Entity as ISoftDeletable).IsDeleted = true;
        }
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}