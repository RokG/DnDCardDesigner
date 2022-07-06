using CardDesigner.Domain.Models;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface ISpellCardUpdater
    {
        Task<SpellCardModel> UpdateSpellCard(SpellCardModel spellCard);
    }
}