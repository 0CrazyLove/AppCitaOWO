using Microsoft.EntityFrameworkCore;

public class LoginDbContext : DbContext
{
    public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options) { }
    public DbSet<UserLogin> AppCitaDB { get; set; }
    public DbSet<GoogleUserData> GoogleAuthUsers { get; set; }
//estudiar esto
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Relación uno a uno entre UserLogin y GoogleUserData
        modelBuilder.Entity<UserLogin>()
            .HasOne(u => u.GoogleUserData)
            .WithOne(g => g.UserLogin)
            .HasForeignKey<UserLogin>(u => u.GoogleUserSub)
            .IsRequired(false);
    }
}