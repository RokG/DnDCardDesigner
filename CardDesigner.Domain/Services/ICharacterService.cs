using CardDesigner.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface ICharacterService
    {
        Task<CharacterModel> CreateCharacter(CharacterModel Character);
        Task<CharacterModel> UpdateCharacter(CharacterModel Character);
        Task<CharacterModel> UpdateCharacterDecks(CharacterModel Character);
        Task<bool> DeleteCharacter(CharacterModel Character);
        public Task<IEnumerable<CharacterModel>> GetAllCharacters();
    }
}
