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
    public class DatabaseCharacterService : ICharacterService
    {
        private readonly CardDesignerDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;

        public DatabaseCharacterService(CardDesignerDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<CharacterModel> CreateCharacter(CharacterModel character)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                CharacterEntity characterEntity = _mapper.Map<CharacterEntity>(character);

                CharacterEntity createdCharacterEntity = dbContext.Characters.Add(characterEntity).Entity;
                await dbContext.SaveChangesAsync();

                return _mapper.Map<CharacterModel>(createdCharacterEntity);
            }
        }

        public async Task<CharacterModel> UpdateCharacter(CharacterModel character)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                try
                {
                    // Get character from database
                    CharacterEntity characterEntity = dbContext.Characters
                        .Include(d => d.SpellDecks)
                        .Include(d => d.ItemDecks)
                        .Single(d => d.ID == character.ID);


                    // Loop over cards in source deck
                    foreach (SpellDeckModel spellDeck in character.SpellDecks)
                    {
                        // If any card is new, add it to the list
                        if (!characterEntity.SpellDecks.Where(sd => sd.ID == spellDeck.ID).Any())
                        {
                            // Get spell deck from database
                            SpellDeckEntity spellDeckEntity = dbContext.SpellDecks
                                .Include(sd => sd.SpellCards)
                                .Single(sc => sc.ID == spellDeck.ID);
                            //SpellDeckEntity spellDeckEntity = _mapper.Map<SpellDeckEntity>(spellDeck);

                            // Loop over cards in source deck
                            foreach (SpellCardModel spellCard in spellDeck.SpellCards)
                            {
                                // If any card is new, add it to the list
                                if (!spellDeckEntity.SpellCards.Where(sd => sd.ID == spellCard.ID).Any())
                                {
                                    SpellCardEntity spellCardEntity = _mapper.Map<SpellCardEntity>(spellCard);
                                    spellDeckEntity.SpellCards.Add(spellCardEntity);
                                }
                            }

                            characterEntity.SpellDecks.Add(spellDeckEntity);
                        }
                    }

                    // Loop over cards in source deck
                    foreach (ItemDeckModel itemDeck in character.ItemDecks)
                    {
                        // If any card is new, add it to the list
                        if (!characterEntity.ItemDecks.Where(sd => sd.ID == itemDeck.ID).Any())
                        {
                            // Get item deck from database
                            ItemDeckEntity itemDeckEntity = dbContext.ItemDecks
                                .Include(sd => sd.ItemCards)
                                .Single(sc => sc.ID == itemDeck.ID);
                            //ItemDeckEntity itemDeckEntity = _mapper.Map<ItemDeckEntity>(itemDeck);

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

                            characterEntity.ItemDecks.Add(itemDeckEntity);
                        }
                    }

                    // Update database
                    if (dbContext.Characters.Contains(characterEntity))
                    {
                        dbContext.Characters.Update(characterEntity);
                        await dbContext.SaveChangesAsync();
                        return _mapper.Map<CharacterModel>(characterEntity); ;
                    }
                    return null;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<bool> DeleteCharacter(CharacterModel character)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                try
                {
                    CharacterEntity characterEntity = _mapper.Map<CharacterEntity>(character);
                    if (dbContext.Characters.Contains(characterEntity))
                    {
                        dbContext.Characters.Remove(characterEntity);
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

        public async Task<IEnumerable<CharacterModel>> GetAllCharacters()
        {
            using (CardDesignerDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<CharacterEntity> characterEntities = await
                    context.Characters
                    .Include(c => c.SpellDecks)
                    .Include(c => c.ItemDecks)
                    .ToListAsync();

                //IEnumerable<Character> characterEntities = await
                //   context.Characters.ToListAsync();

                return characterEntities.Select(c => _mapper.Map<CharacterModel>(c));
            }
        }
    }
}
