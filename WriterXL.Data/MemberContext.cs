using Microsoft.EntityFrameworkCore;
using WriterXL.Domain.Members;

namespace WriterXL.Data
{
    public class MemberContext : DbContext
    {
        public MemberContext(DbContextOptions<MemberContext> options) : base(options)
        {
            
        }
        
        public DbSet<Member> Members { get; set; }
    }
}