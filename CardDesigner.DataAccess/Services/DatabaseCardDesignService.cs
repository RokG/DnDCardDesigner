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
                    case CharacterDeckDesignModel characterDeckDesignModel:
                        CharacterDeckDesignEntity characterDeckDesignEntity = _mapper.Map<CharacterDeckDesignEntity>(characterDeckDesignModel);
                        CharacterDeckDesignEntity createdCharacterDeckDesignEntity = dbContext.CharacterDeckDesigns.Add(characterDeckDesignEntity).Entity;
                        await dbContext.SaveChangesAsync();
                        return _mapper.Map<CharacterDeckDesignModel>(createdCharacterDeckDesignEntity);
                    case MinionDeckDesignModel minionDeckDesignModel:
                        MinionDeckDesignEntity minionDeckDesignEntity = _mapper.Map<MinionDeckDesignEntity>(minionDeckDesignModel);
                        MinionDeckDesignEntity createdMinionDeckDesignEntity = dbContext.MinionDeckDesigns.Add(minionDeckDesignEntity).Entity;
                        await dbContext.SaveChangesAsync();
                        return _mapper.Map<MinionDeckDesignModel>(createdMinionDeckDesignEntity);
                    case DeckBackgroundDesignModel deckBackgroundDesignModel:
                        DeckBackgroundDesignEntity deckBackgroundDesignEntity = _mapper.Map<DeckBackgroundDesignEntity>(deckBackgroundDesignModel);
                        DeckBackgroundDesignEntity createdDeckBackgroundDesignEntity = dbContext.DeckBackgroundDesigns.Add(deckBackgroundDesignEntity).Entity;
                        await dbContext.SaveChangesAsync();
                        return _mapper.Map<DeckBackgroundDesignModel>(createdDeckBackgroundDesignEntity);
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
                    case CharacterDeckDesignModel characterDeckDesignModel:
                        CharacterDeckDesignEntity characterDeckDesignEntity = _mapper.Map<CharacterDeckDesignEntity>(characterDeckDesignModel);
                        CharacterDeckDesignEntity createdCharacterDeckDesignEntity = dbContext.CharacterDeckDesigns.Update(characterDeckDesignEntity).Entity;
                        await dbContext.SaveChangesAsync();
                        return _mapper.Map<CharacterDeckDesignModel>(createdCharacterDeckDesignEntity);
                    case MinionDeckDesignModel minionDeckDesignModel:
                        MinionDeckDesignEntity minionDeckDesignEntity = _mapper.Map<MinionDeckDesignEntity>(minionDeckDesignModel);
                        MinionDeckDesignEntity createdMinionDeckDesignEntity = dbContext.MinionDeckDesigns.Update(minionDeckDesignEntity).Entity;
                        await dbContext.SaveChangesAsync();
                        return _mapper.Map<MinionDeckDesignModel>(createdMinionDeckDesignEntity);
                    case DeckBackgroundDesignModel deckBackgroundDesignModel:
                        DeckBackgroundDesignEntity deckBackgroundDesignEntity = _mapper.Map<DeckBackgroundDesignEntity>(deckBackgroundDesignModel);
                        DeckBackgroundDesignEntity createdDeckBackgroundDesignEntity = dbContext.DeckBackgroundDesigns.Update(deckBackgroundDesignEntity).Entity;
                        await dbContext.SaveChangesAsync();
                        return _mapper.Map<DeckBackgroundDesignModel>(createdDeckBackgroundDesignEntity);
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
                    case CharacterDeckDesignModel characterDeckDesignModel:
                        CharacterDeckDesignEntity characterDeckDesignEntity = _mapper.Map<CharacterDeckDesignEntity>(characterDeckDesignModel);
                        if (dbContext.CharacterDeckDesigns.Contains(characterDeckDesignEntity))
                        {
                            dbContext.CharacterDeckDesigns.Remove(characterDeckDesignEntity);
                            await dbContext.SaveChangesAsync();
                            return true;
                        }
                        return false;
                    case MinionDeckDesignModel minionDeckDesignModel:
                        MinionDeckDesignEntity minionDeckDesignEntity = _mapper.Map<MinionDeckDesignEntity>(minionDeckDesignModel);
                        if (dbContext.MinionDeckDesigns.Contains(minionDeckDesignEntity))
                        {
                            dbContext.MinionDeckDesigns.Remove(minionDeckDesignEntity);
                            await dbContext.SaveChangesAsync();
                            return true;
                        }
                        return false;
                    case DeckBackgroundDesignModel deckBackgroundDesignModel:
                        DeckBackgroundDesignEntity deckBackgroundDesignEntity = _mapper.Map<DeckBackgroundDesignEntity>(deckBackgroundDesignModel);
                        if (dbContext.DeckBackgroundDesigns.Contains(deckBackgroundDesignEntity))
                        {
                            dbContext.DeckBackgroundDesigns.Remove(deckBackgroundDesignEntity);
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

        public async Task<IEnumerable<MinionDeckDesignModel>> GetAllMinionDeckDesigns()
        {
            using (CardDesignerDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<MinionDeckDesignEntity> cardDesignEntities = await
                    context.MinionDeckDesigns
                    .Include(c => c.Characters)
                    .ToListAsync();

                return cardDesignEntities.Select(c => _mapper.Map<MinionDeckDesignModel>(c));
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
    }
}
