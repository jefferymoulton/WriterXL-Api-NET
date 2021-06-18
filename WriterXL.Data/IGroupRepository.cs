using System.Threading.Tasks;
using WriterXL.Domain.Groups;

namespace WriterXL.Data
{
    public interface IGroupRepository
    {
        // General 
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        Task<Group[]> GetAllGroupsAsync();
        Task<Group> GetGroupByIdAsync(int id);
        Task<Group> GetGroupByMonikerAsync(string moniker);
    }
}