using CardDesigner.Domain.Models;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface ICharacterDeleter
    {
        Task<bool> DeleteCharacter(CharacterModel character);
    }
}