using CardDesigner.Domain.Models;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface IItemDeckUpdater
    {
        Task<ItemDeckModel> UpdateItemDeck(ItemDeckModel itemDeck);
    }
}
