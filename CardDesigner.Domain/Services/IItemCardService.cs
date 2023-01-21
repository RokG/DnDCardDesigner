using CardDesigner.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface IItemCardService
    {
        Task<ItemCardModel> CreateItemCard(ItemCardModel ItemCard);
        Task<ItemCardModel> UpdateItemCard(ItemCardModel ItemCard);
        Task<bool> DeleteItemCard(ItemCardModel ItemCard);
        public Task<IEnumerable<ItemCardModel>> GetAllItemCards();
    }
}
