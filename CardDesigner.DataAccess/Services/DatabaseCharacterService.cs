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

        public async Task<CharacterModel> UpdateCharacter(CharacterModel characterModel)
        {
            using CardDesignerDbContext dbContext = _dbContextFactory.CreateDbContext();
            {
                try
                {
                    // Get spell deck from database
                    CharacterEntity characterEntity = dbContext.Characters
                        .Include(sd => sd.SpellDeckDescriptors)
                        .Include(sd => sd.ItemDeckDescriptors)
                        .Include(sd => sd.DeckBackgroundDesign)
                        .Single(sc => sc.ID == characterModel.ID);

                    // Add/Update spell decks
                    foreach (SpellDeckDesignLinkerModel spellDeckDesignModel in characterModel.SpellDeckDescriptors)
                    {
                        // If any descriptor is new, add it to the list
                        if (!characterEntity.SpellDeckDescriptors.Where(sd => sd.SpellDeckID == spellDeckDesignModel.SpellDeckID).Any())
                        {
                            SpellDeckDesignLinkerEntity spellDeckDesignEntity = _mapper.Map<SpellDeckDesignLinkerEntity>(spellDeckDesignModel);
                            characterEntity.SpellDeckDescriptors.Add(spellDeckDesignEntity);
                        }
                        // If any descriptor exists, update it
                        else
                        {
                            characterEntity.SpellDeckDescriptors
                                .First(d => d.SpellDeckID == spellDeckDesignModel.SpellDeckID)
                                .DesignID = spellDeckDesignModel.DesignID;
                        }
                    }

                    // Add/Update item decks
                    foreach (ItemDeckDesignLinkerModel itemDeckDesignModel in characterModel.ItemDeckDescriptors)
                    {
                        // If any descriptor is new, add it to the list
                        if (!characterEntity.ItemDeckDescriptors.Where(sd => sd.ItemDeckID == itemDeckDesignModel.ItemDeckID).Any())
                        {
                            ItemDeckDesignLinkerEntity itemDeckDesignEntity = _mapper.Map<ItemDeckDesignLinkerEntity>(itemDeckDesignModel);
                            characterEntity.ItemDeckDescriptors.Add(itemDeckDesignEntity);
                        }
                        // If any descriptor exists, update it
                        else
                        {
                            characterEntity.ItemDeckDescriptors
                                .First(d => d.ItemDeckID == itemDeckDesignModel.ItemDeckID)
                                .DesignID = itemDeckDesignModel.DesignID;
                        }
                    }

                    // Add/Update back decks
                    // If any descriptor is new, add it to the list
                    if (characterModel.DeckBackgroundDesign != null)
                    {
                        if (characterEntity.DeckBackgroundDesign == null)
                        {
                            CharacterDeckDesignEntity characterDeckDesignEntity = _mapper.Map<CharacterDeckDesignEntity>(characterModel.DeckBackgroundDesign);
                            characterEntity.DeckBackgroundDesign = characterDeckDesignEntity;
                        }
                        else
                        {
                            CharacterDeckDesignEntity characterDeckDesignEntity = dbContext.CharacterDeckDesigns
                                .Single(sc => sc.ID == characterModel.DeckBackgroundDesign.ID);

                            characterEntity.DeckBackgroundDesign = null;
                            characterEntity.DeckBackgroundDesign = characterDeckDesignEntity;
                        }
                    }

                    // Remove spell decks
                    foreach (SpellDeckDesignLinkerEntity spellDeckDesignEntity in characterEntity.SpellDeckDescriptors)
                    {
                        // If any descriptor is missing, remove it from the list
                        if (!characterModel.SpellDeckDescriptors.Any(id => id.SpellDeckID == spellDeckDesignEntity.SpellDeckID))
                        {
                            SpellDeckDesignLinkerEntity toRemove = dbContext.SpellDeckDesignLinkers.Single(
                                sc => (
                                sc.SpellDeckID == spellDeckDesignEntity.SpellDeckID
                                && sc.Character.ID == characterModel.ID
                                ));
                            dbContext.SpellDeckDesignLinkers.Remove(toRemove);
                        }
                    }

                    // Remove item decks
                    foreach (ItemDeckDesignLinkerEntity itemDeckDesignEntity in characterEntity.ItemDeckDescriptors)
                    {
                        // If any descriptor is missing, remove it from the list
                        if (!characterModel.ItemDeckDescriptors.Any(id => id.ItemDeckID == itemDeckDesignEntity.ItemDeckID))
                        {
                            ItemDeckDesignLinkerEntity toRemove = dbContext.ItemDeckDesignLinkers.Single(
                                sc => (
                                sc.ItemDeckID == itemDeckDesignEntity.ItemDeckID
                                && sc.Character.ID == characterModel.ID
                                ));
                            dbContext.ItemDeckDesignLinkers.Remove(toRemove);
                        }
                    }

                    // Update and return
                    await dbContext.SaveChangesAsync();

                    return _mapper.Map<CharacterModel>(characterEntity);
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
                    .Include(c => c.SpellDeckDescriptors)
                    .Include(c => c.ItemDeckDescriptors)
                    .Include(c => c.DeckBackgroundDesign)
                    .Include(c => c.Classes)
                    .ToListAsync();

                return characterEntities.Select(c => _mapper.Map<CharacterModel>(c));
            }
        }
    }
}
