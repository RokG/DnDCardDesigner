using CardDesigner.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface IDeckService
    {
        Task<IDeck> CreateDeck(IDeck deckModel);
        Task<IDeck> UpdateDeck(IDeck deckModel);
        Task<bool> DeleteDeck(IDeck deckModel);
        public Task<IEnumerable<T>> GetAllDecks<T>();
    }
}
