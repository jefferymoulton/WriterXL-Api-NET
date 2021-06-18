using Microsoft.EntityFrameworkCore;
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
    }
}