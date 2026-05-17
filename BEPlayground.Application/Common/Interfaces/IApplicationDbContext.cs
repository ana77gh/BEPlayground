using Microsoft.EntityFrameworkCore;
using BEPlayground.Domain.Entities;

namespace BEPlayground.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
