using CardDesigner.Domain.Models;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface ISpellCardCreator
    {
        Task<SpellCardModel> CreateSpellCard(SpellCardModel spellCard);
    }
}