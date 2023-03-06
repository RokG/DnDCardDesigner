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
                // Get matching entity
                CharacterEntity characterEntity = dbContext.Characters
                    .Include(c => c.SpellDeckDescriptors)
                    .Include(c => c.ItemDeckDescriptors)
                    .Include(c => c.MinionDeckDescriptors)
                    .Include(c => c.CharacterDeckDescriptors)
                    .Include(c => c.Classes)
                    .Single(c => c.ID == characterModel.ID);

                characterEntity.Title = characterModel.Title;
                characterEntity.Weight = characterModel.Weight;
                characterEntity.Height = characterModel.Height;
                characterEntity.Age = characterModel.Age;
                characterEntity.Hitpoints = characterModel.Hitpoints;
                characterEntity.Race = characterModel.Race;
                characterEntity.Alignment = characterModel.Alignment;

                // Update one-to-one bindings
                characterEntity.Abilities = _mapper.Map<CharacterAbilitiesEntity>(characterModel.Abilities);
                characterEntity.CasterStats = _mapper.Map<CasterStatsEntity>(characterModel.CasterStats);
                characterEntity.DeckBackgroundDesign = _mapper.Map<DeckBackgroundDesignEntity>(characterModel.DeckBackgroundDesign);

                // Add Classes
                foreach (CharacterClassModel characterClassModel in characterModel.Classes)
                {
                    CharacterClassEntity matchingEntity = characterEntity.Classes.FirstOrDefault(c => c.ClassID == characterClassModel.ClassID);
                    //If any class is new, add it to the list
                    if (matchingEntity == null)
                    {
                        CharacterClassEntity characterClassEntity = _mapper.Map<CharacterClassEntity>(characterClassModel);
                        characterEntity.Classes.Add(characterClassEntity);
                    }
                    // If any class exists, update it
                    else
                    {
                        matchingEntity.Level = characterClassModel.Level;
                    }
                }

                // Remove Classes
                CharacterClassEntity removedClassEntity = characterEntity.Classes
                    .FirstOrDefault(p => characterModel.Classes.All(p2 => p2.ClassID != p.ClassID));
                if (removedClassEntity != null)
                {
                    characterEntity.Classes.Remove(removedClassEntity);
                    dbContext.CharacterClasses.Remove(removedClassEntity);
                }

                // Add Spell Decks
                foreach (SpellDeckDesignLinkerModel spellDeckDesignLinkerModel in characterModel.SpellDeckDescriptors)
                {
                    SpellDeckDesignLinkerEntity matchingEntity = characterEntity.SpellDeckDescriptors
                        .FirstOrDefault(c => c.Character.ID == characterModel.ID && c.SpellDeckID == spellDeckDesignLinkerModel.SpellDeckID);
                    //If any class is new, add it to the list
                    if (matchingEntity == null)
                    {
                        SpellDeckDesignLinkerEntity characterClassEntity = _mapper.Map<SpellDeckDesignLinkerEntity>(spellDeckDesignLinkerModel);
                        characterEntity.SpellDeckDescriptors.Add(characterClassEntity);
                    }
                    // If any class exists, update it
                    else
                    {
                        matchingEntity.DesignID = spellDeckDesignLinkerModel.DesignID;
                    }
                }

                // Remove Spell Decks
                SpellDeckDesignLinkerEntity removedSpellDeckEntity = characterEntity.SpellDeckDescriptors
                    .FirstOrDefault(p => characterModel.SpellDeckDescriptors.All(p2 => p2.SpellDeckID != p.SpellDeckID));
                if (removedSpellDeckEntity != null)
                {
                    characterEntity.SpellDeckDescriptors.Remove(removedSpellDeckEntity);
                    dbContext.SpellDeckDesignLinkers.Remove(removedSpellDeckEntity);
                }

                // Add Item Decks
                foreach (ItemDeckDesignLinkerModel itemDeckDesignLinkerModel in characterModel.ItemDeckDescriptors)
                {
                    ItemDeckDesignLinkerEntity matchingEntity = characterEntity.ItemDeckDescriptors
                        .FirstOrDefault(c => c.Character.ID == characterModel.ID && c.ItemDeckID == itemDeckDesignLinkerModel.ItemDeckID);
                    //If any class is new, add it to the list
                    if (matchingEntity == null)
                    {
                        ItemDeckDesignLinkerEntity characterClassEntity = _mapper.Map<ItemDeckDesignLinkerEntity>(itemDeckDesignLinkerModel);
                        characterEntity.ItemDeckDescriptors.Add(characterClassEntity);
                    }
                    // If any class exists, update it
                    else
                    {
                        matchingEntity.DesignID = itemDeckDesignLinkerModel.DesignID;
                    }
                }

                // Remove Item Decks
                ItemDeckDesignLinkerEntity removedItemDeckEntity = characterEntity.ItemDeckDescriptors
                    .FirstOrDefault(p => characterModel.ItemDeckDescriptors.All(p2 => p2.ItemDeckID != p.ItemDeckID));
                if (removedItemDeckEntity != null)
                {
                    characterEntity.ItemDeckDescriptors.Remove(removedItemDeckEntity);
                    dbContext.ItemDeckDesignLinkers.Remove(removedItemDeckEntity);
                }

                // Add Character Decks
                foreach (CharacterDeckDesignLinkerModel characterDeckDesignLinkerModel in characterModel.CharacterDeckDescriptors)
                {
                    CharacterDeckDesignLinkerEntity matchingEntity = characterEntity.CharacterDeckDescriptors
                        .FirstOrDefault(c => c.Character.ID == characterModel.ID && c.CharacterDeckID == characterDeckDesignLinkerModel.CharacterDeckID);
                    //If any class is new, add it to the list
                    if (matchingEntity == null)
                    {
                        CharacterDeckDesignLinkerEntity characterClassEntity = _mapper.Map<CharacterDeckDesignLinkerEntity>(characterDeckDesignLinkerModel);
                        characterEntity.CharacterDeckDescriptors.Add(characterClassEntity);
                    }
                    // If any class exists, update it
                    else
                    {
                        matchingEntity.DesignID = characterDeckDesignLinkerModel.DesignID;
                    }
                }

                // Remove Character Decks
                CharacterDeckDesignLinkerEntity removedCharacterDeckEntity = characterEntity.CharacterDeckDescriptors
                    .FirstOrDefault(p => characterModel.CharacterDeckDescriptors.All(p2 => p2.CharacterDeckID != p.CharacterDeckID));
                if (removedCharacterDeckEntity != null)
                {
                    characterEntity.CharacterDeckDescriptors.Remove(removedCharacterDeckEntity);
                    dbContext.CharacterDeckDesignLinkers.Remove(removedCharacterDeckEntity);
                }

                // Add Minion Decks
                foreach (MinionDeckDesignLinkerModel MinionDeckDesignLinkerModel in characterModel.MinionDeckDescriptors)
                {
                    MinionDeckDesignLinkerEntity matchingEntity = characterEntity.MinionDeckDescriptors
                        .FirstOrDefault(c => c.Character.ID == characterModel.ID && c.MinionDeckID == MinionDeckDesignLinkerModel.MinionDeckID);
                    //If any class is new, add it to the list
                    if (matchingEntity == null)
                    {
                        MinionDeckDesignLinkerEntity characterClassEntity = _mapper.Map<MinionDeckDesignLinkerEntity>(MinionDeckDesignLinkerModel);
                        characterEntity.MinionDeckDescriptors.Add(characterClassEntity);
                    }
                    // If any class exists, update it
                    else
                    {
                        matchingEntity.DesignID = MinionDeckDesignLinkerModel.DesignID;
                    }
                }

                // Remove Minion Decks
                MinionDeckDesignLinkerEntity removedMinionDeckEntity = characterEntity.MinionDeckDescriptors
                    .FirstOrDefault(p => characterModel.MinionDeckDescriptors.All(p2 => p2.MinionDeckID != p.MinionDeckID));
                if (removedMinionDeckEntity != null)
                {
                    characterEntity.MinionDeckDescriptors.Remove(removedMinionDeckEntity);
                    dbContext.MinionDeckDesignLinkers.Remove(removedMinionDeckEntity);
                }

                // Finaly, update character
                CharacterEntity createdCharacterCardEntity = dbContext.Characters.Update(characterEntity).Entity;

                await dbContext.SaveChangesAsync();

                return _mapper.Map<CharacterModel>(createdCharacterCardEntity);
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
                        if (dbContext.CasterStats.Contains(characterEntity.CasterStats))
                        {
                            dbContext.CasterStats.Remove(characterEntity.CasterStats);
                        }

                        if (dbContext.CharacterAbilities.Contains(characterEntity.Abilities))
                        {
                            dbContext.CharacterAbilities.Remove(characterEntity.Abilities);
                        }

                        if (dbContext.DeckBackgroundDesigns.Contains(characterEntity.DeckBackgroundDesign))
                        {
                            dbContext.DeckBackgroundDesigns.Remove(characterEntity.DeckBackgroundDesign);
                        }

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
                    .Include(c => c.CharacterDeckDescriptors)
                    .Include(c => c.MinionDeckDescriptors)
                    .Include(c => c.DeckBackgroundDesign)
                    .Include(c => c.Classes)
                    .Include(c => c.CasterStats)
                    .Include(c => c.Abilities)
                    .ToListAsync();

                return characterEntities.Select(c => _mapper.Map<CharacterModel>(c));
            }
        }
    }
}
