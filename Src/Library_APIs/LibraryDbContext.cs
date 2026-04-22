using Microsoft.EntityFrameworkCore;

namespace Library_APIs
{
    public class LibraryDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                optionsBuilder.AddInterceptors(new AuditInterceptor());

        }
    }
}
