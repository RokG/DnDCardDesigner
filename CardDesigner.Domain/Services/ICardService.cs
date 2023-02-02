using CardDesigner.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface ICardService
    {
        Task<ICard> CreateCard(ICard cardModel);
        Task<ICard> UpdateCard(ICard cardModel);
        Task<bool> DeleteCard(ICard cardModel);
        public Task<IEnumerable<T>> GetAllCards<T>();
    }
}
