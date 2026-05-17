using BEPlayground.Application.Common.Interfaces;
using BEPlayground.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BEPlayground.Infrastructure.Data
{
    public class AppDbContext : DbContext, IApplicationDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products => Set<Product>();
    }
}

