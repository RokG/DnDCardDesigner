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
    public class DatabaseItemCardService : IItemCardService
    {
        private readonly CardDesignerDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public DatabaseItemCardService(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }
        
        public async Task<ItemCardModel> CreateItemCard(ItemCardModel itemCard)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                ItemCardEntity itemCardEntity = _mapper.Map<ItemCardEntity>(itemCard);
                ItemCardEntity createdItemCardEntity = dbContext.ItemCards.Add(itemCardEntity).Entity;
                await dbContext.SaveChangesAsync();

                return _mapper.Map<ItemCardModel>(createdItemCardEntity);
            }
        }

        public async Task<ItemCardModel> UpdateItemCard(ItemCardModel itemCard)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                ItemCardEntity itemCardEntity = _mapper.Map<ItemCardEntity>(itemCard);

                ItemCardEntity createdItemCardEntity = dbContext.ItemCards.Update(itemCardEntity).Entity;
                await dbContext.SaveChangesAsync();

                return _mapper.Map<ItemCardModel>(itemCardEntity);
            }
        }
        
        public async Task<bool> DeleteItemCard(ItemCardModel itemCard)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                try
                {
                    ItemCardEntity itemCardEntity = _mapper.Map<ItemCardEntity>(itemCard);
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

        public async Task<IEnumerable<ItemCardModel>> GetAllItemCards()
        {
            using (CardDesignerDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ItemCardEntity> characterEntities = await
                    context.ItemCards
                    .Include(sc => sc.ItemDecks)
                    .ToListAsync();

                return characterEntities.Select(c => _mapper.Map<ItemCardModel>(c));
            }
        }

    }
}
