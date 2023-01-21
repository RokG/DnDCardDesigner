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
    public class DatabaseItemDeckDeleter : IItemDeckDeleter
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
        public DatabaseItemDeckDeleter(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<bool> DeleteItemDeck(ItemDeckModel itemDeck)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                try
                {
                    ItemDeck itemDeckEntity = _mapper.Map<ItemDeck>(itemDeck);
                    if (dbContext.ItemDecks.Contains(itemDeckEntity))
                    {
                        dbContext.ItemDecks.Remove(itemDeckEntity);
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