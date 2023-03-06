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
    public class DatabaseDeckService : IDeckService
    {
        private readonly CardDesignerDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public DatabaseDeckService(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<IDeck> CreateDeck(IDeck deckModel)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                switch (deckModel)
                {
                    case SpellDeckModel spellDeckModel:
                        SpellDeckEntity spellDeckEntity = _mapper.Map<SpellDeckEntity>(spellDeckModel);
                        SpellDeckEntity createdSpellDeckEntity = dbContext.SpellDecks.Add(spellDeckEntity).Entity;

                        await dbContext.SaveChangesAsync();

                        return _mapper.Map<SpellDeckModel>(createdSpellDeckEntity);
                    case ItemDeckModel itemDeckModel:
                        ItemDeckEntity itemDeckEntity = _mapper.Map<ItemDeckEntity>(itemDeckModel);
                        ItemDeckEntity createdItemDeckEntity = dbContext.ItemDecks.Add(itemDeckEntity).Entity;

                        await dbContext.SaveChangesAsync();

                        return _mapper.Map<ItemDeckModel>(createdItemDeckEntity);
                    case CharacterDeckModel characterDeckModel:
                        CharacterDeckEntity characterDeckEntity = _mapper.Map<CharacterDeckEntity>(characterDeckModel);
                        CharacterDeckEntity createdCharacterDeckEntity = dbContext.CharacterDecks.Add(characterDeckEntity).Entity;

                        await dbContext.SaveChangesAsync();

                        return _mapper.Map<CharacterDeckModel>(createdCharacterDeckEntity);
                    case MinionDeckModel minionDeckModel:
                        MinionDeckEntity minionDeckEntity = _mapper.Map<MinionDeckEntity>(minionDeckModel);
                        MinionDeckEntity createdMinionDeckEntity = dbContext.MinionDecks.Add(minionDeckEntity).Entity;

                        await dbContext.SaveChangesAsync();

                        return _mapper.Map<MinionDeckModel>(createdMinionDeckEntity);
                    default:
                        return null;
                }
            }
        }

        public async Task<IDeck> UpdateDeck(IDeck deckModel)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                switch (deckModel)
                {
                    case SpellDeckModel spellDeckModel:
                        // Get spell deck from database
                        SpellDeckEntity spellDeckEntity = dbContext.SpellDecks
                            .Include(sd => sd.SpellCards)
                            .Single(sc => sc.ID == spellDeckModel.ID);

                        // Loop over cards in source deck - ADD
                        foreach (SpellCardModel spellCard in spellDeckModel.SpellCards)
                        {
                            // If any card is new, add it to the list
                            if (!spellDeckEntity.SpellCards.Where(sd => sd.ID == spellCard.ID).Any())
                            {
                                SpellCardEntity spellCardEntity = _mapper.Map<SpellCardEntity>(spellCard);
                                spellDeckEntity.SpellCards.Add(spellCardEntity);
                            }
                        }

                        // Loop over cards in source deck - REMOVE
                        foreach (SpellCardEntity spellCard in spellDeckEntity.SpellCards)
                        {
                            // If any card is missing, remove it from the list
                            if (!spellDeckModel.SpellCards.Any(id => id.ID == spellCard.ID))
                            {
                                SpellCardEntity spellCardEntity = _mapper.Map<SpellCardEntity>(spellCard);
                                spellDeckEntity.SpellCards.Remove(spellCardEntity);
                            }
                        }

                        await dbContext.SaveChangesAsync();

                        return _mapper.Map<SpellDeckModel>(spellDeckEntity);
                    case ItemDeckModel itemDeckModel:
                        // Get item deck from database
                        ItemDeckEntity itemDeckEntity = dbContext.ItemDecks
                            .Include(sd => sd.ItemCards)
                            .Single(sc => sc.ID == itemDeckModel.ID);

                        // Loop over cards in source deck - ADD
                        foreach (ItemCardModel itemCardModel in itemDeckModel.ItemCards)
                        {
                            // If any card is new, add it to the list
                            if (!itemDeckEntity.ItemCards.Where(sd => sd.ID == itemCardModel.ID).Any())
                            {
                                ItemCardEntity itemCardEntity = _mapper.Map<ItemCardEntity>(itemCardModel);
                                itemDeckEntity.ItemCards.Add(itemCardEntity);
                            }
                        }
                        // Loop over cards in source deck - REMOVE
                        foreach (ItemCardEntity itemCardEntity in itemDeckEntity.ItemCards)
                        {
                            // If any card is missing, remove it from the list
                            if (!itemDeckModel.ItemCards.Any(id => id.ID == itemCardEntity.ID))
                            {
                                itemDeckEntity.ItemCards.Remove(itemCardEntity);
                            }
                        }

                        await dbContext.SaveChangesAsync();

                        return _mapper.Map<ItemDeckModel>(itemDeckEntity);
                    case CharacterDeckModel characterDeckModel:
                        // Get character deck from database
                        CharacterDeckEntity characterDeckEntity = dbContext.CharacterDecks
                            .Include(sd => sd.CharacterCards)
                            .Single(sc => sc.ID == characterDeckModel.ID);

                        // Loop over cards in source deck - ADD
                        foreach (CharacterCardModel characterCardModel in characterDeckModel.CharacterCards)
                        {
                            // If any card is new, add it to the list
                            if (!characterDeckEntity.CharacterCards.Where(sd => sd.ID == characterCardModel.ID).Any())
                            {
                                CharacterCardEntity characterCardEntity = _mapper.Map<CharacterCardEntity>(characterCardModel);
                                characterDeckEntity.CharacterCards.Add(characterCardEntity);
                            }
                        }
                        // Loop over cards in source deck - REMOVE
                        foreach (CharacterCardEntity characterCardEntity in characterDeckEntity.CharacterCards)
                        {
                            // If any card is missing, remove it from the list
                            if (!characterDeckModel.CharacterCards.Any(id => id.ID == characterCardEntity.ID))
                            {
                                characterDeckEntity.CharacterCards.Remove(characterCardEntity);
                            }
                        }

                        await dbContext.SaveChangesAsync();

                        return _mapper.Map<CharacterDeckModel>(characterDeckEntity);
                    case MinionDeckModel minionDeckModel:
                        // Get minion deck from database
                        MinionDeckEntity minionDeckEntity = dbContext.MinionDecks
                            .Include(sd => sd.MinionCards)
                            .Single(sc => sc.ID == minionDeckModel.ID);

                        // Loop over cards in source deck - ADD
                        foreach (MinionCardModel minionCardModel in minionDeckModel.MinionCards)
                        {
                            // If any card is new, add it to the list
                            if (!minionDeckEntity.MinionCards.Where(sd => sd.ID == minionCardModel.ID).Any())
                            {
                                MinionCardEntity minionCardEntity = _mapper.Map<MinionCardEntity>(minionCardModel);
                                minionDeckEntity.MinionCards.Add(minionCardEntity);
                            }
                        }
                        // Loop over cards in source deck - REMOVE
                        foreach (MinionCardEntity minionCardEntity in minionDeckEntity.MinionCards)
                        {
                            // If any card is missing, remove it from the list
                            if (!minionDeckModel.MinionCards.Any(id => id.ID == minionCardEntity.ID))
                            {
                                minionDeckEntity.MinionCards.Remove(minionCardEntity);
                            }
                        }

                        await dbContext.SaveChangesAsync();

                        return _mapper.Map<MinionDeckModel>(minionDeckEntity);
                    default:
                        return null;
                }
            }
        }

        public async Task<bool> DeleteDeck(IDeck deckModel)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                switch (deckModel)
                {
                    case SpellDeckModel spellDeckModel:
                        SpellDeckEntity spellDeckEntity = _mapper.Map<SpellDeckEntity>(spellDeckModel);
                        if (dbContext.SpellDecks.Contains(spellDeckEntity))
                        {
                            dbContext.SpellDecks.Remove(spellDeckEntity);
                            await dbContext.SaveChangesAsync();
                            return true;
                        }
                        return false;
                    case ItemDeckModel itemDeckModel:
                        ItemDeckEntity itemDeckEntity = _mapper.Map<ItemDeckEntity>(itemDeckModel);
                        if (dbContext.ItemDecks.Contains(itemDeckEntity))
                        {
                            dbContext.ItemDecks.Remove(itemDeckEntity);
                            await dbContext.SaveChangesAsync();
                            return true;
                        }
                        return false;
                    case CharacterDeckModel characterDeckModel:
                        CharacterDeckEntity characterDeckEntity = _mapper.Map<CharacterDeckEntity>(characterDeckModel);
                        if (dbContext.CharacterDecks.Contains(characterDeckEntity))
                        {
                            dbContext.CharacterDecks.Remove(characterDeckEntity);
                            await dbContext.SaveChangesAsync();
                            return true;
                        }
                        return false;
                    case MinionDeckModel minionDeckModel:
                        MinionDeckEntity minionDeckEntity = _mapper.Map<MinionDeckEntity>(minionDeckModel);
                        if (dbContext.MinionDecks.Contains(minionDeckEntity))
                        {
                            dbContext.MinionDecks.Remove(minionDeckEntity);
                            await dbContext.SaveChangesAsync();
                            return true;
                        }
                        return false;
                    default:
                        return false; ;
                }
            }
        }

        public async Task<IEnumerable<T>> GetAllDecks<T>()
        {
            using (CardDesignerDbContext context = _dbContextFactory.CreateDbContext())
            {
                if (typeof(T) == typeof(SpellDeckModel))
                {
                    IEnumerable<SpellDeckEntity> spellDeckEntities = await
                        context.SpellDecks
                        .Include(sd => sd.SpellCards)
                        .ToListAsync();

                    return (IEnumerable<T>)spellDeckEntities.Select(c => _mapper.Map<SpellDeckModel>(c));
                }
                else if (typeof(T) == typeof(ItemDeckModel))
                {
                    IEnumerable<ItemDeckEntity> itemDeckEntities = await
                        context.ItemDecks
                        .Include(sd => sd.ItemCards)
                        .ToListAsync();

                    return (IEnumerable<T>)itemDeckEntities.Select(c => _mapper.Map<ItemDeckModel>(c));
                }
                else if (typeof(T) == typeof(CharacterDeckModel))
                {
                    IEnumerable<CharacterDeckEntity> characterDeckEntities = await
                        context.CharacterDecks
                        .Include(sd => sd.CharacterCards)
                        .ToListAsync();

                    return (IEnumerable<T>)characterDeckEntities.Select(c => _mapper.Map<CharacterDeckModel>(c));
                }
                else if (typeof(T) == typeof(MinionDeckModel))
                {
                    IEnumerable<MinionDeckEntity> minionDeckEntities = await
                        context.MinionDecks
                        .Include(sd => sd.MinionCards)
                        .ThenInclude(sd=>sd.Minion)
                        .ToListAsync();

                    return (IEnumerable<T>)minionDeckEntities.Select(c => _mapper.Map<MinionDeckModel>(c));
                }
                else
                {
                    return null;
                }

            }
        }
    }

}
