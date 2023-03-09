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
                            .Include(sd => sd.Cards)
                            .Single(sc => sc.ID == spellDeckModel.ID);

                        spellDeckEntity.Name = spellDeckModel.Name;

                        // Loop over cards in source deck - ADD
                        foreach (SpellCardModel spellCard in spellDeckModel.Cards)
                        {
                            // If any card is new, add it to the list
                            if (!spellDeckEntity.Cards.Where(sd => sd.ID == spellCard.ID).Any())
                            {
                                SpellCardEntity spellCardEntity = _mapper.Map<SpellCardEntity>(spellCard);
                                spellDeckEntity.Cards.Add(spellCardEntity);
                            }
                        }

                        // Loop over cards in source deck - REMOVE
                        foreach (SpellCardEntity spellCard in spellDeckEntity.Cards)
                        {
                            // If any card is missing, remove it from the list
                            if (!spellDeckModel.Cards.Any(id => id.ID == spellCard.ID))
                            {
                                spellDeckEntity.Cards.Remove(spellCard);
                            }
                        }

                        SpellDeckEntity createdSpellDeckEntity = dbContext.SpellDecks.Update(spellDeckEntity).Entity;

                        await dbContext.SaveChangesAsync();

                        return _mapper.Map<SpellDeckModel>(createdSpellDeckEntity);

                    case ItemDeckModel itemDeckModel:
                        // Get item deck from database
                        ItemDeckEntity itemDeckEntity = dbContext.ItemDecks
                            .Include(sd => sd.Cards)
                            .Single(sc => sc.ID == itemDeckModel.ID);

                        itemDeckEntity.Name = itemDeckModel.Name;

                        // Loop over cards in source deck - ADD
                        foreach (ItemCardModel itemCard in itemDeckModel.Cards)
                        {
                            // If any card is new, add it to the list
                            if (!itemDeckEntity.Cards.Where(sd => sd.ID == itemCard.ID).Any())
                            {
                                ItemCardEntity itemCardEntity = _mapper.Map<ItemCardEntity>(itemCard);
                                itemDeckEntity.Cards.Add(itemCardEntity);
                            }
                        }

                        // Loop over cards in source deck - REMOVE
                        foreach (ItemCardEntity itemCard in itemDeckEntity.Cards)
                        {
                            // If any card is missing, remove it from the list
                            if (!itemDeckModel.Cards.Any(id => id.ID == itemCard.ID))
                            {
                                itemDeckEntity.Cards.Remove(itemCard);
                            }
                        }

                        ItemDeckEntity createdItemDeckEntity = dbContext.ItemDecks.Update(itemDeckEntity).Entity;

                        await dbContext.SaveChangesAsync();

                        return _mapper.Map<ItemDeckModel>(createdItemDeckEntity);

                    case CharacterDeckModel characterDeckModel:
                        // Get character deck from database
                        CharacterDeckEntity characterDeckEntity = dbContext.CharacterDecks
                            .Include(sd => sd.Cards)
                            .Single(sc => sc.ID == characterDeckModel.ID);

                        characterDeckEntity.Name = characterDeckModel.Name;

                        // Loop over cards in source deck - ADD
                        foreach (CharacterCardModel characterCard in characterDeckModel.Cards)
                        {
                            // If any card is new, add it to the list
                            if (!characterDeckEntity.Cards.Where(sd => sd.ID == characterCard.ID).Any())
                            {
                                CharacterCardEntity characterCardEntity = _mapper.Map<CharacterCardEntity>(characterCard);
                                characterDeckEntity.Cards.Add(characterCardEntity);
                            }
                        }

                        // Loop over cards in source deck - REMOVE
                        foreach (CharacterCardEntity characterCard in characterDeckEntity.Cards)
                        {
                            // If any card is missing, remove it from the list
                            if (!characterDeckModel.Cards.Any(id => id.ID == characterCard.ID))
                            {
                                characterDeckEntity.Cards.Remove(characterCard);
                            }
                        }

                        CharacterDeckEntity createdCharacterDeckEntity = dbContext.CharacterDecks.Update(characterDeckEntity).Entity;

                        await dbContext.SaveChangesAsync();

                        return _mapper.Map<CharacterDeckModel>(createdCharacterDeckEntity);

                    case MinionDeckModel minionDeckModel:
                        // Get minion deck from database
                        MinionDeckEntity minionDeckEntity = dbContext.MinionDecks
                            .Include(sd => sd.Cards)
                            .Single(sc => sc.ID == minionDeckModel.ID);

                        minionDeckEntity.Name = minionDeckModel.Name;

                        // Loop over cards in source deck - ADD
                        foreach (MinionCardModel minionCard in minionDeckModel.Cards)
                        {
                            // If any card is new, add it to the list
                            if (!minionDeckEntity.Cards.Where(sd => sd.ID == minionCard.ID).Any())
                            {
                                MinionCardEntity minionCardEntity = _mapper.Map<MinionCardEntity>(minionCard);
                                minionDeckEntity.Cards.Add(minionCardEntity);
                            }
                        }
                        // Loop over cards in source deck - REMOVE
                        foreach (MinionCardEntity minionCard in minionDeckEntity.Cards)
                        {
                            // If any card is missing, remove it from the list
                            if (!minionDeckModel.Cards.Any(id => id.ID == minionCard.ID))
                            {
                                minionDeckEntity.Cards.Remove(minionCard);
                            }
                        }

                        MinionDeckEntity createdMinionDeckEntity = dbContext.MinionDecks.Update(minionDeckEntity).Entity;

                        await dbContext.SaveChangesAsync();

                        return _mapper.Map<MinionDeckModel>(createdMinionDeckEntity);

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
                        .Include(sd => sd.Cards)
                        .ToListAsync();

                    return (IEnumerable<T>)spellDeckEntities.Select(c => _mapper.Map<SpellDeckModel>(c));
                }
                else if (typeof(T) == typeof(ItemDeckModel))
                {
                    IEnumerable<ItemDeckEntity> itemDeckEntities = await
                        context.ItemDecks
                        .Include(sd => sd.Cards)
                        .ToListAsync();

                    return (IEnumerable<T>)itemDeckEntities.Select(c => _mapper.Map<ItemDeckModel>(c));
                }
                else if (typeof(T) == typeof(CharacterDeckModel))
                {
                    IEnumerable<CharacterDeckEntity> characterDeckEntities = await
                        context.CharacterDecks
                        .Include(sd => sd.Cards)
                        .ToListAsync();

                    return (IEnumerable<T>)characterDeckEntities.Select(c => _mapper.Map<CharacterDeckModel>(c));
                }
                else if (typeof(T) == typeof(MinionDeckModel))
                {
                    IEnumerable<MinionDeckEntity> minionDeckEntities = await
                        context.MinionDecks
                        .Include(sd => sd.Cards)
                        .ThenInclude(sd => sd.Minion)
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
