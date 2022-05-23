using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesigner.DataAccess.Services.CharacterProvider
{
    public interface ICharacterProvider
    {
        public Task<IEnumerable<CharacterModel>> GetAllCharacters();
    }
}