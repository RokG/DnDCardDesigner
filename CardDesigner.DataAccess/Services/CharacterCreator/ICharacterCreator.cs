using CardDesigner.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDesigner.DataAccess.Services.CharacterCreator
{
    public interface ICharacterCreator
    {
        Task<CharacterModel> CreateCharacter(CharacterModel character);
    }
}