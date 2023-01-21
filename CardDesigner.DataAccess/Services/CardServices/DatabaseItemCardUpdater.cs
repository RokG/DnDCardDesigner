using AutoMapper;
using CardDesigner.DataAccess.DbContexts;
using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using System.Threading.Tasks;

namespace CardDesigner.DataAccess.Services
{
    public class DatabaseItemCardUpdater : IItemCardUpdater
    {
        #region Private fields

        private readonly CardDesignerDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        #endregion

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="dbContextFactory">Database context factory</param>
        /// <param name="mapper">Mapper object</param>
        public DatabaseItemCardUpdater(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all characters from database
        /// </summary>
        /// <returns></returns>
        public async Task<ItemCardModel> UpdateItemCard(ItemCardModel itemCard)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                ItemCard itemCardEntity = _mapper.Map<ItemCard>(itemCard);

                ItemCard createdItemCardEntity = dbContext.ItemCards.Update(itemCardEntity).Entity;
                await dbContext.SaveChangesAsync();

                return _mapper.Map<ItemCardModel>(itemCardEntity);
            }
        }
    }
}