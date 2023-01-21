using CardDesigner.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardDesigner.Domain.Services
{
    public interface IItemCardProvider
    {
        public Task<IEnumerable<ItemCardModel>> GetAllItemCards();
    }
}