using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WriterXL.Domain.Groups;
using WriterXL.Domain.Members;

namespace WriterXL.Data
{
    public class GroupRepository : IGroupRepository
    {
        private readonly WxlContext _context;
        private readonly ILogger<MemberRepository> _logger;

        public GroupRepository(WxlContext context, ILogger<MemberRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        // TODO: Refactor these general methods to a single location
        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempting to save the changes in the context");

            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> AddMemberToGroup(Member member, string moniker)
        {
            _logger.LogInformation($"Adding member {member.Id} to group {moniker}"); 
            
            var group = _context.Groups.Where(g => g.Moniker.Equals(moniker)).FirstOrDefault();
            group.Members.Add(member);

            var result = _context.SaveChanges();
            return (result > 0);
        }

        public async Task<Group[]> GetAllGroupsAsync(bool includeMembers = false)
        {
            _logger.LogInformation($"Retriving all groups");
            
            IQueryable<Group> query = _context.Groups;
            if (includeMembers) query = query.Include(g => g.Members);

            return await query.ToArrayAsync();
        }

        public async Task<Group> GetGroupByIdAsync(int id, bool includeMembers = false)
        {
            _logger.LogInformation($"Retrieving group by Id: {id}");
            
            IQueryable<Group> query = _context.Groups;
            if (includeMembers) query = query.Include(g => g.Members);
            query = query.Where(g => g.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Group> GetGroupByMonikerAsync(string moniker, bool includeMembers = false)
        {
            _logger.LogInformation($"Retrieving group by moniker {moniker}");

            IQueryable<Group> query = _context.Groups;
            if (includeMembers) query = query.Include(g => g.Members);
            query = query.Where(g => g.Moniker.Equals(moniker));

            return await query.FirstOrDefaultAsync();
        }
    }
}