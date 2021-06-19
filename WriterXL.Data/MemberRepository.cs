using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WriterXL.Domain.Groups;
using WriterXL.Domain.Members;

namespace WriterXL.Data
{
    public class MemberRepository : IMemberRepository
    {
        private readonly WxlContext _context;
        private readonly ILogger<MemberRepository> _logger;

        public MemberRepository(WxlContext context, ILogger<MemberRepository> logger)
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

        public async Task<Member[]> GetAllMembersAsync()
        {
            _logger.LogInformation($"Retrieving all members.");
            return await _context.Members.ToArrayAsync();
        }

        public async Task<Member> GetMemberByIdAsync(int id, bool includeGroups = false)
        {
            _logger.LogInformation($"Retrieving a Member by Id: {id}");

            IQueryable<Member> query = _context.Members;
            if (includeGroups) query = query.Include(m => m.MemberOf);
            query = query.Where(m => m.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Member> GetMemberByEmailAsync(string emailAddress, bool includeGroups = false)
        {
            _logger.LogInformation($"Retrieving a Member by {emailAddress}");

            IQueryable<Member> query = _context.Members;
            if (includeGroups) query = query.Include(m => m.MemberOf);
            query = query.Where(m => m.EmailAddress.Equals(emailAddress));
            
            return await query.FirstOrDefaultAsync();
        }
    }
}