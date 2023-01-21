using CardDesigner.Domain.Models;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface IItemDeckCreator
    {
        Task<ItemDeckModel> CreateItemDeck(ItemDeckModel itemDeck);
    }
}
