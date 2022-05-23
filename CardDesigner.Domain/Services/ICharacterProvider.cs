using CardDesigner.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface ICharacterProvider
    {
        public Task<IEnumerable<CharacterModel>> GetAllCharacters();
    }
}