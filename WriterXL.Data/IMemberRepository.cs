using System.Threading.Tasks;
using WriterXL.Domain.Groups;
using WriterXL.Domain.Members;

namespace WriterXL.Data
{
    public interface IMemberRepository
    {
        // General 
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        
        Task<Member[]> GetAllMembersAsync();
        Task<Member> GetMemberByIdAsync(int id, bool includeGroups = false);
        Task<Member> GetMemberByEmailAsync(string emailAddress, bool includeGroups = false);
    }
}