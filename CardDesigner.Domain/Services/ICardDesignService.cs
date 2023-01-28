using CardDesigner.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface ICardDesignService
    {
        Task<CardDesignModel> CreateCardDesign(CardDesignModel cardDesignModel);
        Task<CardDesignModel> UpdateCardDesign(CardDesignModel cardDesignModel);
        Task<bool> DeleteCardDesign(CardDesignModel cardDesignModel);
        public Task<IEnumerable<CardDesignModel>> GetAllCardDesigns();
    }
}
