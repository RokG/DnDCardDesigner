using CardDesigner.Domain.Models;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface ISpellCardCreator
    {
        Task CreateSpellCard(SpellCardModel spellCard);
    }
}