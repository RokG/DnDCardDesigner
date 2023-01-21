using CardDesigner.Domain.Models;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface IItemDeckDeleter
    {
        Task<bool> DeleteItemDeck(ItemDeckModel itemDeck);
    }
}
