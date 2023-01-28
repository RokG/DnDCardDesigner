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
    public class DatabaseCardDesignService : ICardDesignService
    {
        private readonly CardDesignerDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public DatabaseCardDesignService(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<CardDesignModel> CreateCardDesign(CardDesignModel cardDesign)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                CardDesignEntity cardDesignEntity = _mapper.Map<CardDesignEntity>(cardDesign);

                CardDesignEntity createdCardDesignEntity = dbContext.CardDesigns.Add(cardDesignEntity).Entity;
                await dbContext.SaveChangesAsync();

                return _mapper.Map<CardDesignModel>(createdCardDesignEntity);
            }
        }

        public async Task<CardDesignModel> UpdateCardDesign(CardDesignModel cardDesignModel)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                try
                {
                    CardDesignEntity itemCardEntity = _mapper.Map<CardDesignEntity>(cardDesignModel);

                    CardDesignEntity createdItemCardEntity = dbContext.CardDesigns.Update(itemCardEntity).Entity;
                    await dbContext.SaveChangesAsync();

                    return _mapper.Map<CardDesignModel>(itemCardEntity);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<bool> DeleteCardDesign(CardDesignModel cardDesign)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                try
                {
                    CardDesignEntity cardDesignEntity = _mapper.Map<CardDesignEntity>(cardDesign);
                    if (dbContext.CardDesigns.Contains(cardDesignEntity))
                    {
                        dbContext.CardDesigns.Remove(cardDesignEntity);
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

        public async Task<IEnumerable<CardDesignModel>> GetAllCardDesigns()
        {
            using (CardDesignerDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<CardDesignEntity> cardDesignEntities = await
                    context.CardDesigns
                    .Include(c => c.Characters)
                    .ToListAsync();

                return cardDesignEntities.Select(c => _mapper.Map<CardDesignModel>(c));
            }
        }
    }
}
