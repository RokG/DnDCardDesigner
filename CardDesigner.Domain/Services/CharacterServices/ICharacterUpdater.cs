using CardDesigner.Domain.Models;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface ICharacterUpdater
    {
        Task<CharacterModel> UpdateCharacter(CharacterModel character);
    }
}
