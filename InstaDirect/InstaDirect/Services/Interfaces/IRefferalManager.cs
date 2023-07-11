using InstaDirect.Models;
using InstaDirect.Models.ViewModels;

namespace InstaDirect.Services.Interfaces
{
    public interface IRefferalManager
    {
        public Task AddRefferalAsync(RefferalsInfo refferal);
        public Task<IEnumerable<RefferalsInfo>> GetGoodRefferalsAsync(long tid);
        public Task<IEnumerable<TopRefferals>> GetTopRefferalsAsync(int days);
        public Task<IEnumerable<FullRefferalInfo>> GetFullRefferalsInfoAsync(long tid);
        public Task DeleteRefferalAsync(int id);
        public Task DeleteAllRefferalsAsync();
        public Task<bool> CheckForАvailabilityRefferalAsync(long refferalTId, long inviterTId);
    }
}
