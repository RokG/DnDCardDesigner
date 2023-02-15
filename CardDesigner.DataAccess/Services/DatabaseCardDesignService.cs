using AutoMapper;
using CardDesigner.DataAccess.DbContexts;
using CardDesigner.Domain.Entities;
using CardDesigner.Domain.Interfaces;
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

        public async Task<ICardDesign> CreateCardDesign(ICardDesign cardDesign)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                switch (cardDesign)
                {
                    case SpellDeckDesignModel spellDeckDesignModel:
                        SpellDeckDesignEntity spellDeckDesignEntity = _mapper.Map<SpellDeckDesignEntity>(spellDeckDesignModel);
                        SpellDeckDesignEntity createdSpellDeckDesignEntity = dbContext.SpellDeckDesigns.Add(spellDeckDesignEntity).Entity;
                        await dbContext.SaveChangesAsync();
                        return _mapper.Map<SpellDeckDesignModel>(createdSpellDeckDesignEntity);
                    case ItemDeckDesignModel itemDeckDesignModel:
                        ItemDeckDesignEntity itemDeckDesignEntity = _mapper.Map<ItemDeckDesignEntity>(itemDeckDesignModel);
                        ItemDeckDesignEntity createdItemDeckDesignEntity = dbContext.ItemDeckDesigns.Add(itemDeckDesignEntity).Entity;
                        await dbContext.SaveChangesAsync();
                        return _mapper.Map<ItemDeckDesignModel>(createdItemDeckDesignEntity);
                    case DeckBackgroundDesignModel characterDeckDesignModel:
                        DeckBackgroundDesignEntity characterDeckDesignEntity = _mapper.Map<DeckBackgroundDesignEntity>(characterDeckDesignModel);
                        DeckBackgroundDesignEntity createdCharacterDeckDesignEntity = dbContext.DeckBackgroundDesigns.Add(characterDeckDesignEntity).Entity;
                        await dbContext.SaveChangesAsync();
                        return _mapper.Map<DeckBackgroundDesignModel>(createdCharacterDeckDesignEntity);
                    default:
                        return null;
                }
            }
        }

        public async Task<ICardDesign> UpdateCardDesign(ICardDesign cardDesignModel)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                switch (cardDesignModel)
                {
                    case SpellDeckDesignModel spellDeckDesignModel:
                        SpellDeckDesignEntity spellDeckDesignEntity = _mapper.Map<SpellDeckDesignEntity>(spellDeckDesignModel);
                        SpellDeckDesignEntity createdSpellDeckDesignEntity = dbContext.SpellDeckDesigns.Update(spellDeckDesignEntity).Entity;
                        await dbContext.SaveChangesAsync();
                        return _mapper.Map<SpellDeckDesignModel>(createdSpellDeckDesignEntity);
                    case ItemDeckDesignModel itemDeckDesignModel:
                        ItemDeckDesignEntity itemDeckDesignEntity = _mapper.Map<ItemDeckDesignEntity>(itemDeckDesignModel);
                        ItemDeckDesignEntity createdItemDeckDesignEntity = dbContext.ItemDeckDesigns.Update(itemDeckDesignEntity).Entity;
                        await dbContext.SaveChangesAsync();
                        return _mapper.Map<ItemDeckDesignModel>(createdItemDeckDesignEntity);
                    case DeckBackgroundDesignModel characterDeckDesignModel:
                        DeckBackgroundDesignEntity characterDeckDesignEntity = _mapper.Map<DeckBackgroundDesignEntity>(characterDeckDesignModel);
                        DeckBackgroundDesignEntity createdCharacterDeckDesignEntity = dbContext.DeckBackgroundDesigns.Update(characterDeckDesignEntity).Entity;
                        await dbContext.SaveChangesAsync();
                        return _mapper.Map<DeckBackgroundDesignModel>(createdCharacterDeckDesignEntity);
                    default:
                        return null;
                }
            }
        }

        public async Task<bool> DeleteCardDesign(ICardDesign cardDesign)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                switch (cardDesign)
                {
                    case SpellDeckDesignModel spellDeckDesignModel:
                        SpellDeckDesignEntity spellDeckDesignEntity = _mapper.Map<SpellDeckDesignEntity>(spellDeckDesignModel);
                        if (dbContext.SpellDeckDesigns.Contains(spellDeckDesignEntity))
                        {
                            dbContext.SpellDeckDesigns.Remove(spellDeckDesignEntity);
                            await dbContext.SaveChangesAsync();
                            return true;
                        }
                        return false;
                    case ItemDeckDesignModel itemDeckDesignModel:
                        ItemDeckDesignEntity itemDeckDesignEntity = _mapper.Map<ItemDeckDesignEntity>(itemDeckDesignModel);
                        if (dbContext.ItemDeckDesigns.Contains(itemDeckDesignEntity))
                        {
                            dbContext.ItemDeckDesigns.Remove(itemDeckDesignEntity);
                            await dbContext.SaveChangesAsync();
                            return true;
                        }
                        return false;
                    case DeckBackgroundDesignModel characterDeckDesignModel:
                        DeckBackgroundDesignEntity characterDeckDesignEntity = _mapper.Map<DeckBackgroundDesignEntity>(characterDeckDesignModel);
                        if (dbContext.DeckBackgroundDesigns.Contains(characterDeckDesignEntity))
                        {
                            dbContext.DeckBackgroundDesigns.Remove(characterDeckDesignEntity);
                            await dbContext.SaveChangesAsync();
                            return true;
                        }
                        return false;
                    default:
                        return false;
                }
            }
        }

        public async Task<IEnumerable<SpellDeckDesignModel>> GetAllSpellDeckDesigns()
        {
            using (CardDesignerDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<SpellDeckDesignEntity> cardDesignEntities = await
                    context.SpellDeckDesigns
                    .Include(c => c.Characters)
                    .ToListAsync();

                return cardDesignEntities.Select(c => _mapper.Map<SpellDeckDesignModel>(c));
            }
        }

        public async Task<IEnumerable<ItemDeckDesignModel>> GetAllItemDeckDesigns()
        {
            using (CardDesignerDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ItemDeckDesignEntity> cardDesignEntities = await
                    context.ItemDeckDesigns
                    .Include(c => c.Characters)
                    .ToListAsync();

                return cardDesignEntities.Select(c => _mapper.Map<ItemDeckDesignModel>(c));
            }
        }

        public async Task<IEnumerable<DeckBackgroundDesignModel>> GetAllBackgroundDeckDesigns()
        {
            using (CardDesignerDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<DeckBackgroundDesignEntity> cardDesignEntities = await
                    context.DeckBackgroundDesigns
                    .Include(c => c.Characters)
                    .ToListAsync();

                return cardDesignEntities.Select(c => _mapper.Map<DeckBackgroundDesignModel>(c));
            }
        }

        public async Task<IEnumerable<CharacterDeckDesignModel>> GetAllCharacterDeckDesigns()
        {
            using (CardDesignerDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<CharacterDeckDesignEntity> cardDesignEntities = await
                    context.CharacterDeckDesigns
                    .Include(c => c.Characters)
                    .ToListAsync();

                return cardDesignEntities.Select(c => _mapper.Map<CharacterDeckDesignModel>(c));
            }
        }
    }
}
