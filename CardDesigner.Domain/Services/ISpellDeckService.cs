using CardDesigner.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface ISpellDeckService
    {
        Task<SpellDeckModel> CreateSpellDeck(SpellDeckModel SpellDeck);
        Task<SpellDeckModel> UpdateSpellDeck(SpellDeckModel SpellDeck);
        Task<bool> DeleteSpellDeck(SpellDeckModel SpellDeck);
        public Task<IEnumerable<SpellDeckModel>> GetAllSpellDecks();
    }
}
