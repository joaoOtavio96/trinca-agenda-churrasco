using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Trinca.AgendaChurrasco.Domain.Shared.Entities;

namespace Trinca.AgendaChurrasco.Data.Interceptors;

public class DataCriacaoInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var entries = eventData.Context.ChangeTracker
            .Entries()
            .Where(x => x.Entity is Entity && x.State == EntityState.Added);

        foreach (var entry in entries)
        {
            (entry.Entity as Entity).DataCriacao = DateTime.UtcNow;
        }
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}