using CardDesigner.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface ISpellCardService
    {
        Task<SpellCardModel> CreateSpellCard(SpellCardModel SpellCard);
        Task<SpellCardModel> UpdateSpellCard(SpellCardModel SpellCard);
        Task<bool> DeleteSpellCard(SpellCardModel SpellCard);
        public Task<IEnumerable<SpellCardModel>> GetAllSpellCards();
    }
}
