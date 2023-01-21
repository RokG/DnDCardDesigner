using CardDesigner.Domain.Models;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface IItemCardDeleter
    {
        Task<bool> DeleteItemCard(ItemCardModel ItemCard);
    }
}