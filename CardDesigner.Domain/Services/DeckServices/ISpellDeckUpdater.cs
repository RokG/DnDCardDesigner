using CardDesigner.Domain.Models;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface ISpellDeckUpdater
    {
        Task<SpellDeckModel> UpdateSpellDeck(SpellDeckModel spellDeck);
    }
}
