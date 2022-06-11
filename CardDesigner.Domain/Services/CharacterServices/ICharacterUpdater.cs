using CardDesigner.Domain.Models;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface ICharacterUpdater
    {
        Task<bool> UpdateCharacter(CharacterModel character);
    }
}
