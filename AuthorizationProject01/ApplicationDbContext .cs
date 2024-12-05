using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<ClaimList> ClaimLists { get; set; }
    public DbSet<PolicyDefinition> PolicyDefinition { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Ensure correct table name mapping (if needed)
        modelBuilder.Entity<PolicyDefinition>().ToTable("PolicyDefinition");
    }

    //protected override void OnModelCreating(ModelBuilder builder)
    //{
    //    base.OnModelCreating(builder);

    //    // Configure ClaimCategory entity
    //    builder.Entity<ClaimCategory>()
    //        .HasKey(c => c.Id);

    //    builder.Entity<ClaimCategory>()
    //        .Property(c => c.Name)
    //        .IsRequired()
    //        .HasMaxLength(100);

    //    // Configure ApplicationUserClaim entity
    //    builder.Entity<ApplicationUserClaim>()
    //        .HasOne(c => c.ClaimCategory)
    //        .WithMany(cc => cc.UserClaims)
    //        .HasForeignKey(c => c.ClaimCategoryId)
    //        .OnDelete(DeleteBehavior.SetNull); // Optional: Set to null if category is deleted
    //}
}
