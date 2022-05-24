using CardDesigner.Domain.Models;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface ICharacterCreator
    {
        Task<CharacterModel> CreateCharacter(CharacterModel character);
    }
}