using AutoMapper;
using CardDesigner.DataAccess.DbContexts;
using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CardDesigner.DataAccess.Services
{
    public class DatabaseItemDeckUpdater : IItemDeckUpdater
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
        public DatabaseItemDeckUpdater(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<ItemDeckModel> UpdateItemDeck(ItemDeckModel itemDeck)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                try
                {
                    // Get item deck from database
                    ItemDeck itemDeckEntity = dbContext.ItemDecks
                        .Include(sd => sd.ItemCards)
                        .Single(sc => sc.ID == itemDeck.ID);

                    // Loop over cards in source deck
                    foreach (ItemCardModel itemCard in itemDeck.ItemCards)
                    {
                        // If any card is new, add it to the list
                        if (!itemDeckEntity.ItemCards.Where(sd => sd.ID == itemCard.ID).Any())
                        {
                            ItemCard itemCardEntity = _mapper.Map<ItemCard>(itemCard);
                            itemDeckEntity.ItemCards.Add(itemCardEntity);
                        }
                    }

                    await dbContext.SaveChangesAsync();

                    return _mapper.Map<ItemDeckModel>(itemDeckEntity); ;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}