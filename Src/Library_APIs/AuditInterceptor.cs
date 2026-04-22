using Library_APIs.Entities;
using Library_APIs.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
namespace Library_APIs
{
    public class AuditInterceptor: SaveChangesInterceptor
    {

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
       DbContextEventData eventData,
       InterceptionResult<int> result,
       CancellationToken cancellationToken = default)
        {

            var context=eventData.Context;

            if (context == null)
                return await base.SavingChangesAsync(eventData, result, cancellationToken);

            foreach (var item in context.ChangeTracker.Entries().Where (e=>e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted).ToList())
            {
                context.Set<AuditLog>().Add(

                       new AuditLog
                       {

                           EntityName = item.Entity.GetType().Name,
                           Action = item.State.ToString(),
                           OccurredAt = DateTime.UtcNow,


                       });
            }



            return await  base.SavingChangesAsync(eventData, result, cancellationToken);
        }

    }
}
