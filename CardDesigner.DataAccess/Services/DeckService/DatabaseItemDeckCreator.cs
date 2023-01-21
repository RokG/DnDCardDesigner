using AutoMapper;
using CardDesigner.DataAccess.DbContexts;
using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using System.Threading.Tasks;

namespace CardDesigner.DataAccess.Services
{
    public class DatabaseItemDeckCreator : IItemDeckCreator
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
        public DatabaseItemDeckCreator(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<ItemDeckModel> CreateItemDeck(ItemDeckModel itemDeck)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                ItemDeck itemDeckEntity = _mapper.Map<ItemDeck>(itemDeck);

                ItemDeck createdItemDeckEntity = dbContext.ItemDecks.Add(itemDeckEntity).Entity;
                await dbContext.SaveChangesAsync();

                return _mapper.Map<ItemDeckModel>(createdItemDeckEntity);
            }
        }

    }
}