using System.Threading.Tasks;
using WriterXL.Domain.Groups;
using WriterXL.Domain.Members;

namespace WriterXL.Data
{
    public interface IGroupRepository
    {
        // General 
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        Task<bool> AddMemberToGroup(Member member, string moniker);

        Task<Group[]> GetAllGroupsAsync(bool includeMembers = false);
        Task<Group> GetGroupByIdAsync(int id, bool includeMembers = false);
        Task<Group> GetGroupByMonikerAsync(string moniker, bool includeMembers = false);
    }
}