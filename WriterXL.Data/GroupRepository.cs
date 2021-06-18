using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WriterXL.Domain.Groups;

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
            _logger.LogInformation($"Attempitng to save the changes in the context");

            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Group[]> GetAllGroupsAsync()
        {
            _logger.LogInformation($"Retriving all groups");
            return await _context.Groups.ToArrayAsync();
        }

        public async Task<Group> GetGroupByIdAsync(int id)
        {
            _logger.LogInformation($"Retrieving group by Id: {id}");

            return await _context.Groups
                .Where(g => g.Id == id)
                .SingleOrDefaultAsync();
        }

        public async Task<Group> GetGroupByMonikerAsync(string moniker)
        {
            _logger.LogInformation($"Retrieving group by moniker {moniker}");

            return await _context.Groups
                .Where(g => g.Moniker.Equals(moniker))
                .SingleOrDefaultAsync();
        }
    }
}