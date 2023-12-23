using DesafioHyperativa.Domain.Contracts.Common;
using DesafioHyperativa.Domain.Entities;
using DesafioHyperativa.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;

namespace DesafioHyperativa.Repository.Contexts;
public class HyperativaDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public HyperativaDbContext(DbContextOptions<HyperativaDbContext> options,
        IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public HyperativaDbContext(DbContextOptions<HyperativaDbContext> options) : base(options) { }
    #region Override
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.Parse("bc63d576-0408-4270-b31e-b4c1541ea03d"),
                DtRegister = DateTime.Now,
                DtUpdate = DateTime.Now,
                Email = "01@user.com",
                Password = "Abcd1234"
            },
            new User
            {
                Id = Guid.Parse("ce07edbb-721d-4693-a0f2-cd01fe469acf"),
                DtRegister = DateTime.Now,
                DtUpdate = DateTime.Now,
                Email = "02@user.com",
                Password = "Efgh5678"
            }
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning));
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        SetDefaultValues();
        return base.SaveChangesAsync(cancellationToken);
    }
    public override int SaveChanges()
    {
        SetDefaultValues();
        return base.SaveChanges();
    }
    #endregion

    #region Private Methods
    private void SetDefaultValues()
    {
        foreach (var entity in ChangeTracker
            .Entries()
            .Where(p => p.State == EntityState.Added))
        {
            if (entity.Entity is IEntity entityCreated)
            {
                entityCreated.Id = Guid.NewGuid();
                entityCreated.DtRegister = DateTime.Now;
                entityCreated.DtUpdate = DateTime.Now;
            }

            if (entity.Entity is IUser entityUser)
            {
                entityUser.UserId = GetUserLogged();
            }
        }

        foreach (var entity in ChangeTracker
            .Entries()
            .Where(p => p.State == EntityState.Modified))
        {
            if (entity.Entity is IEntity entityUpdated)
            {
                entity.Property("DtRegister").IsModified = false;
                entityUpdated.DtUpdate = DateTime.Now;
            }

            if (entity.Entity is IUser entityUser)
            {
                entityUser.UserId = GetUserLogged();
            }
        }
    }

    private Guid GetUserLogged()
    {
        Guid? userId = null;
        var claims = _httpContextAccessor.HttpContext?.User;
        var userIdClaim = claims?.FindFirst("UserId");

        if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid _userId))
            userId = _userId;

        return userId ?? throw new BusinessException("Error retrieving logged in user");
    }
    #endregion
}
