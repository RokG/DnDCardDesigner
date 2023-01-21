using CardDesigner.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface IItemDeckProvider
    {
        public Task<IEnumerable<ItemDeckModel>> GetAllItemDecks();
    }
}
