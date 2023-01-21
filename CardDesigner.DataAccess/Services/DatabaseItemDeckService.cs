using AutoMapper;
using CardDesigner.DataAccess.DbContexts;
using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Models;
using CardDesigner.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardDesigner.DataAccess.Services
{
    public class DatabaseItemDeckService : IItemDeckService
    {
        private readonly CardDesignerDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public DatabaseItemDeckService(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
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
                    ItemDeckEntity itemDeckEntity = dbContext.ItemDecks
                        .Include(sd => sd.ItemCards)
                        .Single(sc => sc.ID == itemDeck.ID);

                    // Loop over cards in source deck
                    foreach (ItemCardModel itemCard in itemDeck.ItemCards)
                    {
                        // If any card is new, add it to the list
                        if (!itemDeckEntity.ItemCards.Where(sd => sd.ID == itemCard.ID).Any())
                        {
                            ItemCardEntity itemCardEntity = _mapper.Map<ItemCardEntity>(itemCard);
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

        public async Task<IEnumerable<ItemDeckModel>> GetAllItemDecks()
        {
            using (CardDesignerDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ItemDeckEntity> itemDeckEntities = await
                    context.ItemDecks
                    .Include(sd => sd.Characters)
                    .Include(sd => sd.ItemCards)
                    .ToListAsync();

                return itemDeckEntities.Select(c => _mapper.Map<ItemDeckModel>(c));
            }
        }

        public async Task<bool> DeleteItemDeck(ItemDeckModel itemDeck)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                try
                {
                    ItemDeckEntity itemDeckEntity = _mapper.Map<ItemDeckEntity>(itemDeck);
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

        public async Task<ItemDeckModel> CreateItemDeck(ItemDeckModel itemDeck)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                ItemDeckEntity itemDeckEntity = _mapper.Map<ItemDeckEntity>(itemDeck);

                ItemDeckEntity createdItemDeckEntity = dbContext.ItemDecks.Add(itemDeckEntity).Entity;
                await dbContext.SaveChangesAsync();

                return _mapper.Map<ItemDeckModel>(createdItemDeckEntity);
            }
        }
    }
}
