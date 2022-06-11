using CardDesigner.Domain.Models;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface ISpellDeckDeleter
    {
        Task<bool> DeleteSpellDeck(SpellDeckModel spellDeck);
    }
}
