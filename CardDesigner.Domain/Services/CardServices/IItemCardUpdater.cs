using CardDesigner.Domain.Models;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface IItemCardUpdater
    {
        Task<ItemCardModel> UpdateItemCard(ItemCardModel ItemCard);
    }
}