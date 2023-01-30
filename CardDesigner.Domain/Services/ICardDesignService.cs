using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface ICardDesignService
    {
        Task<ICardDesign> CreateCardDesign(ICardDesign cardDesignModel);
        Task<ICardDesign> UpdateCardDesign(ICardDesign cardDesignModel);
        Task<bool> DeleteCardDesign(ICardDesign cardDesignModel);
        public Task<IEnumerable<SpellDeckDesignModel>> GetAllSpellDeckDesigns();
        public Task<IEnumerable<ItemDeckDesignModel>> GetAllItemDeckDesigns();
        public Task<IEnumerable<CharacterDeckDesignModel>> GetAllCharacterCardDesigns();
    }
}
