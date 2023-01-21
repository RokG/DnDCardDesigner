using CardDesigner.Domain.Models;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface IItemCardCreator
    {
        Task<ItemCardModel> CreateItemCard(ItemCardModel ItemCard);
    }
}