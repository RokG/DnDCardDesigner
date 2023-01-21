using AutoMapper;
using CardDesigner.DataAccess.DbContexts;
using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CardDesigner.DataAccess.Services
{
    public class DatabaseItemCardDeleter : IItemCardDeleter
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
        public DatabaseItemCardDeleter(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all characters from database
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DeleteItemCard(ItemCardModel itemCard)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                try
                {
                    ItemCard itemCardEntity = _mapper.Map<ItemCard>(itemCard);
                    if (dbContext.ItemCards.Contains(itemCardEntity))
                    {
                        dbContext.ItemCards.Remove(itemCardEntity);
                        await dbContext.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}