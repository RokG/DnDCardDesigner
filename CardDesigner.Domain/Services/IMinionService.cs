using CardDesigner.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface IMinionService
    {
        Task<MinionModel> CreateMinion(MinionModel Minion);
        Task<MinionModel> UpdateMinion(MinionModel Minion);
        Task<bool> DeleteMinion(MinionModel Minion);
        public Task<IEnumerable<MinionModel>> GetAllMinions();
    }
}
