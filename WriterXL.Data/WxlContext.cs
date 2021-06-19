using Microsoft.EntityFrameworkCore;
using WriterXL.Domain;
using WriterXL.Domain.Groups;
using WriterXL.Domain.Members;

namespace WriterXL.Data
{
    public class WxlContext : DbContext
    {
        public WxlContext(DbContextOptions<WxlContext> options) : base(options)
        {
            
        }
        
        public DbSet<Member> Members { get; set; }
        
        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                .HasIndex(m => m.EmailAddress)
                .IsUnique();
            
            modelBuilder.Entity<Member>()
                .Property(m => m.MemberSince)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Group>()
                .HasIndex(g => g.Moniker)
                .IsUnique();

            modelBuilder.Entity<Group>()
                .Property(g => g.DateCreated)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Members)
                .WithMany(m => m.MemberOf)
                .UsingEntity<GroupMembers>
                (gm => gm.HasOne<Member>().WithMany(),
                    gm => gm.HasOne<Group>().WithMany())
                .Property(gm => gm.MemberSince)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}