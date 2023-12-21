using Microsoft.EntityFrameworkCore;

namespace FurryFriends.Persistence;

public interface IDatabaseContext
{
    int SaveChanges();
    Task<int> SaveChangesAsync();
    DbSet<T> Set<T>() where T : class;
}