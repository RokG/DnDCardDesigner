using CardDesigner.Domain.Models;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface ICharacterEditor
    {
        Task<bool> UpdateCharacter(CharacterModel character);
    }
}
