using CardDesigner.Domain.Models;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface ICharacterCreator
    {
        Task CreateCharacter(CharacterModel character);
    }
}