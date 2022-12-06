using Domain.Entities;
using Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<RefreshSession> RefreshSessions { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Office> Offices { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}