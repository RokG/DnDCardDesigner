using CardDesigner.Domain.Models;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface ISpellCardDeleter
    {
        Task<bool> DeleteSpellCard(SpellCardModel ItemCard);
    }
}