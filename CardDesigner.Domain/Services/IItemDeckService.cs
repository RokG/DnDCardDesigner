using CardDesigner.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface IItemDeckService
    {
        Task<ItemDeckModel> CreateItemDeck(ItemDeckModel ItemDeck);
        Task<ItemDeckModel> UpdateItemDeck(ItemDeckModel ItemDeck);
        Task<bool> DeleteItemDeck(ItemDeckModel ItemDeck);
        public Task<IEnumerable<ItemDeckModel>> GetAllItemDecks();
    }
}
